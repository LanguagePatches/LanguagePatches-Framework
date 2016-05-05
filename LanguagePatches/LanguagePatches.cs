/** 
 * Language Patches Framework
 * Translates the game into different Languages
 * Copyright (c) 2016 Thomas P.
 * Licensed under the terms of the MIT License
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KSP.UI;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection.Emit;

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
        /// Patched UI elements
        /// </summary>
        private Dictionary<Text, String> patched_Text { get; set; }

        private Dictionary<TextMesh, String> patched_Mesh { get; set; }
        private Dictionary<DialogGUIBase, String> patched_Base { get; set; }
        private Dictionary<GameScenes, Logger> loggers { get; set; }

        /// <summary>
        /// Load the configs when the game has started
        /// </summary>
        protected override void Awake()
        {
            // Call base
            base.Awake();

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

            // Prevent this class from getting destroyed
            DontDestroyOnLoad(this);

            // Override loading hints
            if (config.HasNode("HINTS"))
            {
                String[] hints = config.GetNode("HINTS").GetValues("hint");
                LoadingScreen.Instance.Screens.ForEach(s => s.tips = hints);
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
        /// Updates the ingame texts
        /// </summary>
        protected override void AsyncUpdate()
        {
            // Text
            foreach (Text text in Resources.FindObjectsOfTypeAll<Text>())
            {
                if (!patched_Text.ContainsKey(text))
                {
                    // Log
                    if (debug) Logger.Active.Log(text.text);

                    // Replace text
                    text.text = translations[text.text];
                    patched_Text.Add(text, text.text);
                }
                else if (patched_Text.ContainsKey(text) && patched_Text[text] != text.text)
                {
                    // Log
                    if (debug) Logger.Active.Log(text.text);

                    // Replace text
                    text.text = translations[text.text];
                    patched_Text[text] = text.text;
                }
            }

            // TextMesh (wtf is this)
            foreach (TextMesh text in Resources.FindObjectsOfTypeAll<TextMesh>())
            {
                if (!patched_Mesh.ContainsKey(text))
                {
                    // Log
                    if (debug) Logger.Active.Log(text.text);

                    // Replace text
                    text.text = translations[text.text];
                    patched_Mesh.Add(text, text.text);
                }
                else if (patched_Mesh.ContainsKey(text) && patched_Mesh[text] != text.text)
                {
                    // Log
                    if (debug) Logger.Active.Log(text.text);

                    // Replace text
                    text.text = translations[text.text];
                    patched_Mesh[text] = text.text;
                }
            }

            // PopupDialog (Squaaaad)
            foreach (PopupDialog dialog in Resources.FindObjectsOfTypeAll<PopupDialog>())
            {
                foreach (DialogGUIBase guiBase in dialog.dialogToDisplay.Options)
                {
                    Utility.DoRecursive(guiBase, childBase => childBase.children, text =>
                    {
                        if (!patched_Base.ContainsKey(text))
                        {
                            // Log
                            if (debug) Logger.Active.Log(text.OptionText);

                            // Replace text
                            text.OptionText = translations[text.OptionText];
                            patched_Base.Add(text, text.OptionText);
                        }
                        else if (patched_Base.ContainsKey(text) && patched_Base[text] != text.OptionText)
                        {
                            // Log
                            if (debug) Logger.Active.Log(text.OptionText);

                            // Replace text
                            text.OptionText = translations[text.OptionText];
                            patched_Base[text] = text.OptionText;
                        }
                    });
                }
            }
        }
    }
}
