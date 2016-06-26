/** 
 * Language Patches Framework
 * Translates the game into different Languages
 * Copyright (c) 2016 Thomas P.
 * Licensed under the terms of the MIT License
 */

using System;
using System.Collections.Generic;

namespace LanguagePatches
{
    /// <summary>
    /// A class to store common utility functions
    /// </summary>
    public class Utility
    {
        // Runs a function recursively
        public static TOut DoRecursive<TIn, TOut>(TIn start, Func<TIn, IEnumerable<TIn>> selector, Func<TOut, bool> check, Func<TIn, TOut> action)
        {
            TOut tout = action(start);
            if (check(tout))
                return tout;
            foreach (TIn tin in selector(start))
            {
                tout = DoRecursive(tin, selector, check, action);
                if (check(tout))
                    return tout;
            }
            return default(TOut);
        }

        // Runs a function recursively
        public static void DoRecursive<T>(T start, Func<T, IEnumerable<T>> selector, Action<T> action)
        {
            DoRecursive<T, Object>(start, selector, tout => false, tin => { action(tin); return null; });
        }

        /// <summary>
        /// Compares the return of a dialog button with the cache
        /// </summary>
        public static Boolean CompareDialog(DialogGUIBase text, String cache)
        {
            // Replace text
            if (text is DialogGUIButton)
            {
                DialogGUIButton gui = ((DialogGUIButton) text);
                return gui.GetString?.Invoke() == cache;
            }
            if (text is DialogGUIToggle)
            {
                DialogGUIToggle gui = ((DialogGUIToggle) text);
                return gui.setLabel?.Invoke() == cache;
            }
            if (text is DialogGUILabel)
            {
                DialogGUILabel gui = ((DialogGUILabel) text);
                return gui.GetString?.Invoke() == cache;
            }
            return false;
        }
    }
}