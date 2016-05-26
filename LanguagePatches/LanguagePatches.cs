/** 
 * Language Patches Framework
 * Translates the game into different Languages
 * Copyright (c) 2016 Thomas P.
 * Licensed under the terms of the MIT License
 */

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using KSP.UI;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection.Emit;
using UnityEngine.Assertions.Must;

namespace LanguagePatches
{
    /// <summary>
    /// The main Language Controller. Here we start all services that are needed
    /// to translate the Games UI.
    /// </summary>
    [KSPAddon(KSPAddon.Startup.Instantly, true)]
    public class LanguagePatches : MonoBehaviour
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
        public Dictionary<String, KeyValuePair<Int32, Int32>> fontSize { get; set; } 

        /// <summary>
        /// Patched UI elements
        /// </summary>
        private Dictionary<Text, String> patched_Text { get; set; }

        private Dictionary<TextMesh, String> patched_Mesh { get; set; }
        private Dictionary<DialogGUIBase, String> patched_Base { get; set; }
        private Dictionary<PopupDialog, String> patched_Dialog { get; set; }
        private Dictionary<GameScenes, Logger> loggers { get; set; }

        /// <summary>
        /// Load the configs when the game has started
        /// </summary>
        void Awake()
        {
            // Instance
            Instance = this;

            // Get the ConfigNodes
            ConfigNode[] nodes = GameDatabase.Instance.GetConfigs("LANGUAGEPATCHES").Select(c => c.config).ToArray();

            // Merge them into a single one
            if (nodes.Length > 1)
            {
                for (Int32 i = 1; i < nodes.Length; i++)
                    nodes[0].AddData(nodes[i]);
            }
            config = nodes[0];

            // Create a new Translation list from the node
            translations = new TranslationList(config);

            // Create fonts
            fonts = new Dictionary<String, Font>();
            fontSize = new Dictionary<String, KeyValuePair<Int32, Int32>>();

            // Prevent this class from getting destroyed
            DontDestroyOnLoad(this);

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
                Int32 meshSize; Int32.TryParse(node.GetValue("meshSize"), out meshSize);
                Int32 textSize; Int32.TryParse(node.GetValue("textSize"), out textSize);

                String[] split = file.Split(':');
                if (split[0].ToLowerInvariant() == "os")
                {
                    String[] moreSplit = split[1].Split('@');
                    fonts.Add(name, Font.CreateDynamicFontFromOSFont(moreSplit[0], Int32.Parse(moreSplit[1])));
                }
                else
                {
                    AssetBundle bundle = AssetBundle.CreateFromMemoryImmediate(File.ReadAllBytes(KSPUtil.ApplicationRootPath + "GameData/" + split[0]));
                    fonts.Add(name, bundle.LoadAsset<Font>(split[1]));
                    bundle.Unload(false);
                }
                fontSize.Add(name, new KeyValuePair<Int32, Int32>(meshSize, textSize));
            }

            // Load case sensivity
            Boolean.TryParse(config.GetValue("caseSensitive"), out caseSensitive);

            // Get debug mode
            Boolean.TryParse(config.GetValue("debug"), out debug);

            // Internals
            patched_Text = new Dictionary<Text, String>();
            patched_Mesh = new Dictionary<TextMesh, String>();
            patched_Base = new Dictionary<DialogGUIBase, String>();

            // Logger
            if (debug)
            {
                loggers = new Dictionary<GameScenes, Logger>();
                GameEvents.onGameSceneLoadRequested.Add((scene) =>
                {
                    if (!loggers.ContainsKey(scene)) loggers.Add(scene, new Logger(scene.ToString()));
                    loggers[scene].SetAsActive();
                });
                loggers.Add(HighLogic.LoadedScene, new Logger(HighLogic.LoadedScene.ToString()));
                loggers[HighLogic.LoadedScene].SetAsActive();
            }

        }

        /// <summary>
        /// The current frame
        /// </summary>
        private Byte frame;

        /// <summary>
        /// Updates the ingame texts
        /// </summary>
        void Update()
        {
            // Update
            frame++;

            // First frame
            if (frame == 1)
            {
                // Patch all Unity UI Texts
                foreach (Text text in Resources.FindObjectsOfTypeAll<Text>())
                {
                    if (!patched_Text.ContainsKey(text) || (patched_Text.ContainsKey(text) && patched_Text[text] != text.text))
                    {
                        // Log
                        if (debug) Logger.Active.Log(text.text);

                        // Replace text
                        text.text = translations[text.text];
                        if (fonts.ContainsKey(text.font.name))
                        {
                            text.font = fonts[text.font.name];
                            text.fontSize = text.fontSize != 0 ? fontSize[text.font.name].Value : 0;
                        }
                        if (patched_Text.ContainsKey(text))
                            patched_Text[text] = text.text;
                        else
                            patched_Text.Add(text, text.text);
                    }
                }
                return;
            }

            // Second Frame
            if (frame == 2)
            {
                // Patch all TextMeshs
                foreach (TextMesh text in Resources.FindObjectsOfTypeAll<TextMesh>())
                {
                    if (!patched_Mesh.ContainsKey(text) || (patched_Mesh.ContainsKey(text) && patched_Mesh[text] != text.text))
                    {
                        // Log
                        if (debug) Logger.Active.Log(text.text);

                        // Replace text and font
                        text.text = translations[text.text];
                        if (fonts.ContainsKey(text.font.name))
                        {
                            text.font = fonts[text.font.name];
                            text.fontSize = text.fontSize != 0 ? fontSize[text.font.name].Key : 0;
                            MeshRenderer rend = text.GetComponentInChildren<MeshRenderer>();
                            rend.material.mainTexture = text.font.material.mainTexture;
                        }
                        if (patched_Mesh.ContainsKey(text))
                            patched_Mesh[text] = text.text;
                        else
                            patched_Mesh.Add(text, text.text);
                    }
                }
                return;
            }

            // Third Frame
            if (frame == 3)
            {
                // Patch all PopupDialogs
                foreach (PopupDialog dialog in Resources.FindObjectsOfTypeAll<PopupDialog>())
                {
                    // Null check
                    if (dialog?.dialogToDisplay == null)
                        continue;

                    // Translate the texts
                    dialog.dialogToDisplay.title = translations[dialog.dialogToDisplay.title];
                    Debug.Log(dialog.dialogToDisplay.title);
                    dialog.dialogToDisplay.message = translations[dialog.dialogToDisplay.message];

                    // Patch the Dialog Options
                    foreach (DialogGUIBase guiBase in dialog.dialogToDisplay.Options)
                    {
                        Utility.DoRecursive(guiBase, childBase => childBase.children, text =>
                        {
                            if (!patched_Base.ContainsKey(text) || (patched_Base.ContainsKey(text) && patched_Base[text] != text.OptionText))
                            {
                                // Log
                                if (debug) Logger.Active.Log(text.OptionText);

                                // Replace text
                                text.OptionText = translations[text.OptionText];
                                if (patched_Base.ContainsKey(text))
                                    patched_Base[text] = text.OptionText;
                                else
                                    patched_Base.Add(text, text.OptionText);
                            }
                        });
                    }
                }
                frame = 0;
            }
        }
    }
}
