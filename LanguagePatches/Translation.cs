/** 
 * Language Patches Framework
 * Translates the game into different Languages
 * Copyright (c) 2016 Thomas P.
 * Licensed under the terms of the MIT License
 */
 
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace LanguagePatches
{
    /// <summary>
    /// A class that represents a text translation
    /// </summary>
    public class Translation
    {
        /// <summary>
        /// The original message, uses Regex syntax
        /// </summary>
        public String text { get; set; }

        /// <summary>
        /// The replacement message, uses String.Format syntax
        /// </summary>
        public String translation { get; set; }

        /// <summary>
        /// The scene where the translation gets applied
        /// </summary>
        public GameScenes? scene { get; set; }

        /// <summary>
        /// Creates a new Translation component from a config node
        /// </summary>
        /// <param name="node">The config node where the </param>
        public Translation(ConfigNode node)
        {
            // Check for original text
            if (!node.HasValue("text"))
                throw new Exception("The config node is missing the text value!");

            // Check for translation
            if (!node.HasValue("translation"))
                throw new Exception("The config node is missing the translation value!");

            // Assign the new texts
            text = node.GetValue("text");
            translation = node.GetValue("translation");

            // Loads scene value
            if (node.HasValue("scene"))
                scene = (GameScenes)Enum.Parse(typeof(GameScenes), node.GetValue("scene"));
            else
                scene = null;

            // Replace variable placeholders
            translation = Regex.Replace(translation, @"@(\d*)", "{$1}");
        }
    }
}
