/**
 * Language Patches Framework
 * Translates the game into different Languages
 * Copyright (c) 2016 Thomas P.
 * Licensed under the terms of the MIT License
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

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
            get
            {
                // Check translations
                for (Int32 j = 0; j < Count; j++)
                {
                    // Scene doesnt match
                    Translation translation = base[j];
                    if (translation.scene.HasValue && translation.scene != HighLogic.LoadedScene)
                        continue;

                    // Get matches
                    String prepared = Prepare(translation.text);
                    Match match = Regex.Match(text, "^" + prepared + "$", !LanguagePatches.Instance.caseSensitive ? RegexOptions.IgnoreCase : RegexOptions.None);

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
        }

        /// <summary>
        /// Creates a new list and fills it with values from a new config node
        /// </summary>
        public TranslationList(ConfigNode node)
        {
            foreach (ConfigNode translation in node.GetNodes("TRANSLATION"))
                Add(new Translation(translation));
        }

        /// <summary>
        /// Modifies a string to be useable
        /// </summary>
        public String Prepare(String input)
        {
            return input.Replace(@"\n", "\n").Replace(@"\r", "\r");
        }
    }
}
