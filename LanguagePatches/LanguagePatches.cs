/** 
 * Language Patches Framework
 * Translates the game into different Languages
 * Copyright (c) 2016 Thomas P.
 * Licensed under the terms of the MIT License
 */

using System;
using System.Collections.Generic;
using System.Linq;
using KSP.UI;
using UnityEngine;
using UnityEngine.UI;

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
        /// Logged UI elements
        /// </summary>
        private Dictionary<Text, String> logged { get; set; }
        private Dictionary<TextMesh, String> logged2 { get; set; }
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

            // Prevent this class from getting destroyed
            DontDestroyOnLoad(this);

            // Override loading hints
            if (config.HasNode("HINTS"))
            {
                String[] hints = config.GetNode("HINTS").GetValues("hint");
                LoadingScreen.Instance.Screens.ForEach(s => s.tips = hints);                
            }

            // Get debug mode
            Boolean.TryParse(config.GetValue("debug"), out debug);
            
            // Logger
            if (debug)
            {
                loggers = new Dictionary<GameScenes, Logger>();
                logged = new Dictionary<Text, String>();
                logged2 = new Dictionary<TextMesh, String>();
                GameEvents.onGameSceneLoadRequested.Add((scene) => { if (!loggers.ContainsKey(scene)) loggers.Add(scene, new Logger(scene.ToString())); loggers[scene].SetAsActive(); });
                loggers.Add(HighLogic.LoadedScene, new Logger(HighLogic.LoadedScene.ToString()));
                loggers[HighLogic.LoadedScene].SetAsActive();
            }

        }

        /// <summary>
        /// Applies the translations to all ui elements it can find
        /// </summary>
        void Update()
        {
            // Text
            foreach (Text text in Resources.FindObjectsOfTypeAll<Text>())
            {
                // Debug mode
                if (debug)
                {
                    if (!logged.ContainsKey(text) || translations[logged[text]] != text.text)
                    {
                        Logger.Active.Log(text.text);
                        logged.Add(text, text.text);
                    }
                }

                // Replace text
                text.text = translations[text.text];
            }

            // TextMesh (wtf is this)
            foreach (TextMesh text in Resources.FindObjectsOfTypeAll<TextMesh>())
            {
                // Debug mode
                if (debug)
                {
                    if (!logged2.ContainsKey(text) || translations[logged2[text]] != text.text)
                    {
                        Logger.Active.Log(text.text);
                        logged2.Add(text, text.text);
                    }
                }

                // Replace text
                text.text = translations[text.text];
            }
        }
    }
}
