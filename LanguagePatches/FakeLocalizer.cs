/**
 * Language Patches Framework
 * Translates the game into different Languages
 * Copyright (c) 2016 Thomas P.
 * Licensed under the terms of the MIT License
 */

using System;
using System.Reflection;
using KSP.Localization;

namespace LanguagePatches
{
    public static class FakeLocalizer
    {
        private static MethodInfo _Format = typeof(Localizer).GetMethod("_Format", BindingFlags.NonPublic | BindingFlags.Instance);

        /// <summary>
        /// A static empty array, so we don't have to create a new one every time Format gets called
        /// </summary>
        private static String[] empty = new String[0];

        /// <summary>
        /// Integrates the regex based translations into the official system
        /// </summary>
        public static String Format(String template)
        {
            String output = (String)_Format.Invoke(Localizer.Instance, new Object[] { template, empty });
            return LanguagePatches.Translate(output, null);
        }

        /// <summary>
        /// Integrates the regex based translations into the official system
        /// </summary>
        public static String Format(String template, params String[] list)
        {
            String output = (String)_Format.Invoke(Localizer.Instance, new Object[] { template, list });
            return LanguagePatches.Translate(output, null);
        }

        /// <summary>
        /// Integrates the regex based translations into the official system
        /// </summary>
        public static String Format(String template, params Object[] list)
        {
            String[] array = new String[list.Length];
			for (Int32 i = 0; i < list.Length; i++)
			{
				array[i] = list[i].ToString();
			}
            String output = (String)_Format.Invoke(Localizer.Instance, new Object[] { template, array });
            return LanguagePatches.Translate(output, null);
        }        
    }
}