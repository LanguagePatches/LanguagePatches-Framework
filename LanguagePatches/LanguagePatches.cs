﻿/**
 * Language Patches Framework
 * Translates the game into different Languages
 * Copyright (c) 2017 Thomas P.
 * Licensed under the terms of the MIT License
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using KSP.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LanguagePatches
{
    /// <summary>
    /// The main Language Controller. Here we start all services that are needed
    /// to translate the Games UI.
    /// </summary>
    [KSPAddon(KSPAddon.Startup.Instantly, true)]
    public class LanguagePatches : AsyncMonoBehaviour
    {
        /// <summary>
        /// The currently active Language Controller
        /// </summary>
        public static LanguagePatches Instance { get; set; }

        /// <summary>
        /// The translations loaded from the config
        /// </summary>
        public TranslationList translations { get; set; }

        /// <summary>
        /// Images that contain translated texts
        /// </summary>
        public Dictionary<String, Texture2D> images { get; set; }

        /// <summary>
        /// The configuration for the framework
        /// </summary>
        public ConfigNode config { get; set; }

        /// <summary>
        /// Whether the plugin should run in debug mode.
        /// </summary>
        public Boolean debug;

        /// <summary>
        /// Whether the framework is case insensitive
        /// </summary>
        public Boolean caseSensitive = true;

        /// <summary>
        /// Fonts, either loaded from Unity Asset Bundles or from the OS
        /// </summary>
        public Dictionary<String, Font> fonts { get; set; }

        /// <summary>
        /// TextMeshPro Fonts, loaded from Unity Asset Bundles
        /// </summary>
        public Dictionary<String, TMP_FontAsset> TMP_fonts { get; set; }

        /// <summary>
        /// Patched UI elements
        /// </summary>
        private CombinedDictionary patched { get; set; }
        private List<String> urls { get; set; }
        private Dictionary<GameScenes, Logger> loggers { get; set; }

        /// <summary>
        /// Load the configs when the game has started
        /// </summary>
        void Awake()
        {
            // Instance
            Instance = this;

            // Redirect the Localization Methods to new methods
            RedirectionHelper.RedirectCalls(new Func<String, String>(Localizer.Format).Method, new Func<String, String>(FakeLocalizer.Format).Method);
            RedirectionHelper.RedirectCalls(new Func<String, String[], String>(Localizer.Format).Method, new Func<String, String[], String>(FakeLocalizer.Format).Method);
            RedirectionHelper.RedirectCalls(new Func<String, System.Object[], String>(Localizer.Format).Method, new Func<String, System.Object[], String>(FakeLocalizer.Format).Method);

            // Get the ConfigNodes
            ConfigNode[] nodes = GameDatabase.Instance.GetConfigs("LANGUAGEPATCHES").Select(c => c.config).ToArray();

            // Merge them into a single one
            if (nodes.Length > 1)
            {
                for (Int32 i = 1; i < nodes.Length; i++)
                {
                    foreach (ConfigNode n in nodes[i].nodes)
                        nodes[0].AddNode(n);
                    foreach (ConfigNode.Value v in nodes[i].values)
                        nodes[0].AddValue(v.name, v.value, v.comment);
                }
            }
            if (nodes.Length != 0)
                config = nodes[0];

            // Create a new Translation list from the node
            translations = new TranslationList(config);

            // Create fonts
            fonts = new Dictionary<String, Font>();

            // Create TMP_FontAssets
            TMP_fonts = new Dictionary<String, TMP_FontAsset>();

            // Create images
            images = new Dictionary<String, Texture2D>();

            // Prevent this class from getting destroyed
            DontDestroyOnLoad(this);

            // Internals
            patched = new CombinedDictionary();

            // Register Updates
            RegisterTask(Method.UPDATE, UpdateText);
            RegisterTask(Method.UPDATE, UpdateTextMesh);
            RegisterTask(Method.UPDATE, UpdateTextMeshPro);
            RegisterTask(Method.UPDATE, PopupDialogUpdate);
            RegisterTask(Method.UPDATE, SceneUpdate);
            RegisterTask(Method.UPDATE, UpdateImages);

            // Nullcheck 
            if (config == null)
                return;

            // Override loading hints
            if (config.HasNode("HINTS"))
            {
                String[] hints = config.GetNode("HINTS").GetValues("hint");
                LoadingScreen.Instance.Screens.ForEach(s => s.tips = hints);
            }

            // Load the fonts
            foreach (ConfigNode node in config.GetNodes("FONT"))
            {
                // Vars
                String name = node.GetValue("name");
                String file = node.GetValue("file");

                String[] split = file.Split(':');
                if (split[0].ToLowerInvariant() == "os")
                {
                    String[] moreSplit = split[1].Split('@');
                    fonts.Add(name, Font.CreateDynamicFontFromOSFont(moreSplit[0], Int32.Parse(moreSplit[1])));
                }
                else
                {
                    AssetBundle bundle = AssetBundle.LoadFromMemory(File.ReadAllBytes(KSPUtil.ApplicationRootPath + "GameData/" + split[0]));
                    fonts.Add(name, bundle.LoadAsset<Font>(split[1]));
                    bundle.Unload(false);
                }
            }

            // Load the TMP_FontAssets
            foreach (ConfigNode node in config.GetNodes("TMPFONT"))
            {
                // Vars
                String name = node.GetValue("name");
                String file = node.GetValue("file");

                String[] split = file.Split(':');

                AssetBundle bundle = AssetBundle.LoadFromMemory(File.ReadAllBytes(KSPUtil.ApplicationRootPath + "GameData/" + split[0]));
                TMP_fonts.Add(name, bundle.LoadAsset<TMP_FontAsset>(split[1]));
                bundle.Unload(false);

            }

            // Load images
            GameEvents.OnGameDatabaseLoaded.Add(() =>
            {
                foreach (ConfigNode node in config.GetNodes("IMAGE"))
                {
                    String original = node.GetValue("name");
                    String file = node.GetValue("file");
                    Texture2D texture = GameDatabase.Instance.GetTexture(file, false);
                    images.Add(original, texture);
                }
            });

            // Load URLS
            if (config.HasNode("URLS"))
            {
                ConfigNode uNode = config.GetNode("URLS");
                urls = new List<String>
                {
                    uNode.HasValue("KSPsiteURL") ? "http://" + uNode.GetValue("KSPsiteURL") : null,
                    uNode.HasValue("SpaceportURL") ? "http://" + uNode.GetValue("SpaceportURL") : null,
                    uNode.HasValue("DefaultFlagURL") ? uNode.GetValue("DefaultFlagURL") : null
                };
            }

            // Load case sensivity
            Boolean.TryParse(config.GetValue("caseSensitive"), out caseSensitive);

            // Get debug mode
            Boolean.TryParse(config.GetValue("debug"), out debug);

            // Logger
            if (debug)
            {
                loggers = new Dictionary<GameScenes, Logger>();
                GameEvents.onGameSceneLoadRequested.Add((scene) =>
                {
                    if (!loggers.ContainsKey(scene)) loggers.Add(scene, new Logger(scene.ToString()));
                    loggers[scene].SetAsActive();
                    mainMenuPatched = false;
                });
                loggers.Add(HighLogic.LoadedScene, new Logger(HighLogic.LoadedScene.ToString()));
                loggers[HighLogic.LoadedScene].SetAsActive();
            }
        }

        /// <summary>
        /// Updates Unity UI components
        /// </summary>
        public void UpdateText()
        {
            // Patch all Unity UI Texts
            foreach (Text text in Resources.FindObjectsOfTypeAll<Text>())
            {
                // Already edited
                if (patched.Contains(text, text.text))
                    continue;

                // Log
                if (debug) Logger.Active.Log("[TXT] " + text.text);

                // Replace text
                text.text = translations[text.text];
                if (fonts.ContainsKey(text.font.name))
                    text.font = fonts[text.font.name];
                if (patched.Contains<Text, String>(text))
                    patched.Set(text, text.text);
                else
                    patched.Add(text, text.text);
            }
        }

        /// <summary>
        /// Updates Textures
        /// </summary>
        public void UpdateImages()
        {
            // Patch all Unity UI Images
            foreach (Material mat in Resources.FindObjectsOfTypeAll<Material>())
            {
                // Already edited
                if (images.Any(s => s.Value?.name == mat.mainTexture?.name))
                    continue;
                if (mat.mainTexture == null)
                    continue;

                // Replace image
                if (images.ContainsKey(mat.mainTexture.name))
                {
                    mat.mainTexture = images[mat.mainTexture.name];
                    DontDestroyOnLoad(mat);
                }
            }
        }

        /// <summary>
        /// Updates Unity textmeshes
        /// </summary>
        public void UpdateTextMesh()
        {
            // Patch all TextMeshs
            foreach (TextMesh text in Resources.FindObjectsOfTypeAll<TextMesh>())
            {
                // Already edited
                if (patched.Contains(text, text.text))
                    continue;

                // Log
                if (debug) Logger.Active.Log("[TMS] " + text.text);

                // Change size
                Int32 NewSize = translations.ChangeSize(text.text, null);
                if (NewSize != -1)
                {
                    text.fontSize = NewSize;
                }

                // Replace text and font
                text.text = translations[text.text];
                if (fonts.ContainsKey(text.font.name))
                {
                    text.font = fonts[text.font.name];
                    MeshRenderer rend = text.GetComponentInChildren<MeshRenderer>();
                    if (text.font?.material?.mainTexture != null)
                        rend.material.mainTexture = text.font.material.mainTexture;
                }

                if (patched.Contains<TextMesh, String>(text))
                    patched.Set(text, text.text);
                else
                    patched.Add(text, text.text);
            }
        }

        /// <summary>
        /// Updates TextMeshPro
        /// </summary>
        public void UpdateTextMeshPro()
        {
            // Patch all TextMeshs
            foreach (TMP_Text text in Resources.FindObjectsOfTypeAll<TMP_Text>())
            {
                // Already edited
                if (patched.Contains(text, text.text))
                    continue;

                // Log
                if (debug) Logger.Active.Log("[TMP] " + text.text);

                // Change size
                Int32 NewSize = translations.ChangeSize(text.text, null);
                if (NewSize != -1)
                {
                    text.fontSize = NewSize;
                }

                // Replace text and font
                text.text = translations[text.text];
                if (TMP_fonts.ContainsKey(text.font.name))
                {
                    text.font = TMP_fonts[text.font.name];
                }


                if (patched.Contains<TMP_Text, String>(text))
                    patched.Set(text, text.text);
                else
                    patched.Add(text, text.text);
            }
        }

        /// <summary>
        /// Updates the Popup Dialoges
        /// </summary>
        public void PopupDialogUpdate()
        {
            // Patch all PopupDialogs
            foreach (PopupDialog dialog in Resources.FindObjectsOfTypeAll<PopupDialog>())
            {
                // Dialog is null
                if (dialog?.dialogToDisplay == null)
                    continue;

                // Translate the texts
                dialog.dialogToDisplay.title = translations[dialog.dialogToDisplay.title];
                dialog.dialogToDisplay.message = translations[dialog.dialogToDisplay.message];

                // Patch the Dialog Options
                foreach (DialogGUIBase guiBase in dialog.dialogToDisplay.Options)
                {
                    Utility.DoRecursive(guiBase, childBase => childBase.children, text =>
                    {
                        if (patched.Contains<DialogGUIBase, String>(text) &&
                            (!patched.Contains<DialogGUIBase, String>(text) ||
                              patched.Get<DialogGUIBase, String>(text) == text.OptionText ||
                              Utility.CompareDialog(text, patched.Get<DialogGUIBase, String>(text)))) return;

                        // Log
                        if (debug) Logger.Active.Log("[PUD]" + text.OptionText);

                        // Replace text
                        if (text is DialogGUIButton)
                        {
                            DialogGUIButton gui = ((DialogGUIButton)text);
                            if (gui.GetString != null)
                            {
                                Func<String> value = (Func<String>)Delegate.CreateDelegate(typeof(Func<String>), gui.GetString.Target, gui.GetString.Method);
                                gui.GetString = () => translations[value()];
                            }
                            else
                            {
                                gui.OptionText = translations[gui.OptionText];
                            }
                        }
                        else if (text is DialogGUIToggle)
                        {
                            DialogGUIToggle gui = ((DialogGUIToggle)text);
                            if (gui.setLabel != null)
                            {
                                Func<String> value = (Func<String>)Delegate.CreateDelegate(typeof(Func<String>), gui.setLabel.Target, gui.setLabel.Method);
                                gui.setLabel = () => translations[value()];
                            }
                            else
                            {
                                gui.label = gui.OptionText = translations[gui.OptionText];
                            }
                        }
                        else if (text is DialogGUILabel)
                        {
                            DialogGUILabel gui = ((DialogGUILabel)text);
                            if (gui.GetString != null)
                            {
                                Func<String> value = (Func<String>)Delegate.CreateDelegate(typeof(Func<String>), gui.GetString.Target, gui.GetString.Method);
                                gui.GetString = () => translations[value()];
                            }
                            else
                            {
                                gui.OptionText = translations[gui.OptionText];
                            }
                        }
                        else if (!String.IsNullOrEmpty(text.OptionText))
                            text.OptionText = translations[text.OptionText];
                        if (patched.Contains<DialogGUIBase, String>(text))
                            patched.Set(text, text.OptionText);
                        else
                            patched.Add(text, text.OptionText);
                    });
                }
            }
        }

        /// <summary>
        /// Does Scene Maintainance
        /// </summary>
        public void SceneUpdate()
        {
            if (HighLogic.LoadedScene == GameScenes.MAINMENU && !mainMenuPatched && urls != null)
            {
                MainMenu menu = Resources.FindObjectsOfTypeAll<MainMenu>()[0];
                menu.KSPsiteURL = urls[0] ?? menu.KSPsiteURL;
                menu.SpaceportURL = urls[1] ?? menu.SpaceportURL;
                menu.DefaultFlagURL = urls[2] ?? menu.DefaultFlagURL;

                // Update callbacks
                typeof(MainMenu).GetProperties(BindingFlags.NonPublic | BindingFlags.Static).FirstOrDefault()?.SetValue(null, menu.DefaultFlagURL, null);
                typeof(MainMenu).GetMethod("Start", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(menu, null);
                mainMenuPatched = true;
            }
        }

        /// <summary>
        /// Whether the main menu was already patched
        /// </summary>
        private Boolean mainMenuPatched;

        /// <summary>
        /// Mod API
        /// </summary>
        public static String Translate(String input, String context)
        {
            if (Instance == null)
                return input;
            if (Instance.translations == null)
                return input;
            return Instance.translations.Translate(input, context);
        }
    }
}
