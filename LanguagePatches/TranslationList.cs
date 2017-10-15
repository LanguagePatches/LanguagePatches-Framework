/**
 * Language Patches Framework
 * Translates the game into different Languages
 * Copyright (c) 2017 Thomas P.
 * Licensed under the terms of the MIT License
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Regex;

namespace LanguagePatches
{
    /// <summary>
    /// A List that contains translations and that can translate texts
    /// </summary>
    public class TranslationList : List<Translation>
    {
        // Some caching for strings
        private List<Object> cache = new List<Object>();

        /// <summary>
        /// Translates a string based on the translations stored in this list
        /// </summary>
        /// <param name="text">The string that should get translated</param>
        /// <returns>The translated representation of the input string</returns>
        public String this[String text]
        {
            get { return Translate(text, null); }
        }

        public String Translate(String text, String context)
        {
            if (String.IsNullOrEmpty(context))
                context = typeof(TranslationList).Assembly.GetName().Name;

            // Check translations
            for (Int32 j = 0; j < Count; j++)
            {
                // Scene does not match
                Translation translation = base[j];
                if (translation.scene != null && (!translation.scene.Contains(HighLogic.LoadedScene)) && (!MapView.MapIsEnabled || !translation.scene.Contains(GameScenes.TRACKSTATION)))
                    continue;
                if (translation.context != context)
                    continue;

                // Get matches
                Match match = translation.expression.Match(text);

                // Check the match
                if (!match.Success)
                    continue;

                // Get the regex matches and create the return string
                GroupCollection groups = match.Groups;
                cache.Clear();
                for (Int32 i = 0; i < groups.Count; i++)
                    cache.Add(groups[i].Value);
                return groups.Count == 1 ? Prepare(translation.translation) : String.Format(Prepare(translation.translation), cache.ToArray());
            }

            // Nothing found
            return text;
        }

        public Int32 ChangeSize(String text, String context)
        {
            if (String.IsNullOrEmpty(context))
                context = typeof(TranslationList).Assembly.GetName().Name;

            // Check translations
            for (Int32 j = 0; j < Count; j++)
            {
                // Scene does not match
                Translation translation = base[j];
                if (translation.scene != null && (!translation.scene.Contains(HighLogic.LoadedScene)) && (!MapView.MapIsEnabled || !translation.scene.Contains(GameScenes.TRACKSTATION)))
                    continue;
                if (translation.context != context)
                    continue;

                // Get matches
                Match match = translation.expression.Match(text);

                // Check the match
                if (!match.Success)
                    continue;

                return translation.size;
            }

            // Nothing found
            return -1;
        }

        /// <summary>
        /// Creates a new list and fills it with values from a new config node
        /// </summary>
        public TranslationList(ConfigNode config)
        {
            ConfigNode[] tNodes = config.GetNodes("TRANSLATION");
            for (Int32 i = 0; i < tNodes.Length; i++)
            {
                Add(new Translation(tNodes[i]));
            }
        }

        /// <summary>
        /// Modifies a string to be useable
        /// </summary>
        public static String Prepare(String input)
        {
            return input.Replace(@"\n", "\n").Replace(@"\r", "\r");
        }
    }
}
