/** 
 * Language Patches Framework
 * Translates the game into different Languages
 * Copyright (c) 2016 Thomas P.
 * Licensed under the terms of the MIT License
 */
 
using System;
using System.Collections.Generic;
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
                Translation translation = this.FirstOrDefault(t => Regex.IsMatch(text, t.text));

                // Null check
                if (translation == null)
                    return text;

                // Get the regex matches and create the return string
                GroupCollection groups = Regex.Match(text, translation.text).Groups;
                if (groups.Count == 1)
                    return translation.translation;
                else
                    return String.Format(translation.translation, groups.OfType<Group>().Select(g => g.Success ? g.Value : "").ToArray());
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
    }
}
