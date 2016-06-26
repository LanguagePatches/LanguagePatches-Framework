﻿/** 
 * Language Patches Framework
 * Translates the game into different Languages
 * Copyright (c) 2016 Thomas P.
 * Licensed under the terms of the MIT License
 */
 
using System;
using System.Collections.Generic;
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
        /// <summary>
        /// Translates a string based on the translations stored in this list
        /// </summary>
        /// <param name="text">The string that should get translated</param>
        /// <returns>The translated representation of the input string</returns>
        public String this[String text]
        {
            get
            {
                // Get the matching translation
                Translation translation = this.FirstOrDefault(t => Regex.IsMatch(text, "^" + Prepare(t.text) + "$", !LanguagePatches.Instance.caseSensitive ? RegexOptions.IgnoreCase : RegexOptions.None));

                // Null check
                if (translation == null || (translation.scene.HasValue && translation.scene != HighLogic.LoadedScene))
                    return text;

                // Get the regex matches and create the return string
                GroupCollection groups = Regex.Match(text, "^" + Prepare(translation.text) + "$", !LanguagePatches.Instance.caseSensitive ? RegexOptions.IgnoreCase : RegexOptions.None).Groups;
                return groups.Count == 1 ? Prepare(translation.translation) : String.Format(Prepare(translation.translation), groups.OfType<Group>().Select(g => g.Success ? g.Value : ")").ToArray());
            }
        }

        /// <summary>
        /// Creates a new list and fills it with values from a new config node
        /// </summary>
        public TranslationList(ConfigNode node) : base()
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
