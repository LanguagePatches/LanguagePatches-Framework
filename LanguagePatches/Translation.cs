/** 
 * Language Patches Framework
 * Translates the game into different Languages
 * Copyright (c) 2017 Thomas P.
 * Licensed under the terms of the MIT License
 */

using System;
using System.CodeDom;
using System.IO;
using System.Regex;

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
        /// The replacement size, uses integer
        /// </summary>
        public Int32 size { get; set; }

        /// <summary>
        /// The scene where the translation gets applied
        /// </summary>
        public GameScenes? scene { get; set; }

        /// <summary>
        /// The expression to check for this translation
        /// </summary>
        public Regex expression { get; set; }

        /// <summary>
        /// The context for the translations
        /// </summary>
        public String context { get; set; }

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
            String prepared = TranslationList.Prepare(text);
            if (!LanguagePatches.Instance.caseSensitive)
                expression = new Regex("^(?i)" + prepared + "$", RegexOptions.Compiled);
            else
                expression = new Regex("^" + prepared + "$", RegexOptions.Compiled | RegexOptions.None);

            // Loads scene value
            if (node.HasValue("scene"))
                scene = (GameScenes)Enum.Parse(typeof(GameScenes), node.GetValue("scene"));
            else
                scene = null;

            // Loads size value
            if (node.HasValue("size"))
                size = Int32.Parse(node.GetValue("size"));
            else
                size = -1;

            // Context
            if (node.HasValue("context"))
                context = node.GetValue("context");
            else
                context = typeof(Translation).Assembly.GetName().Name;

            // Replace variable placeholders
            translation = Regex.Replace(translation, @"@(\d*)", "{$1}");
        }
    }
}
