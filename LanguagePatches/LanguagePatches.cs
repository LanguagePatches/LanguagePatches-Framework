/** 
 * Language Patches Framework
 * Translates the game into different Languages
 * Copyright (c) 2016 Thomas P.
 * Licensed under the terms of the MIT License
 */

/// System
using System;
using System.Collections.Generic;
using System.Linq;

/// KSP
using KSP.UI;

/// Unity
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Our Namespace
/// </summary>
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
        /// Load the configs when the game has started
        /// </summary>
        void Awake()
        {
            /// Get the ConfigNodes
            ConfigNode[] nodes = GameDatabase.Instance.GetConfigs("LANGUAGEPATCHES").Select(c => c.config).ToArray();

            /// Merge them into a single one
            for (Int32 i = 1; i < nodes.Length; i++)
                nodes[0].AddData(nodes[i]);
            config = nodes[0];

            /// Create a new Translation list from the node
            translations = new TranslationList(config);

            /// Prevent this class from getting destroyed
            DontDestroyOnLoad(this);

            /// Override loading hints
            if (config.HasNode("HINTS"))
            {
                String[] hints = config.GetNode("HINTS").GetValues("hint");
                LoadingScreen.Instance.Screens.ForEach(s => s.tips = hints);                
            }
         }
    }
}
