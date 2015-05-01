/**
 * Language Patches
 * Copyright (C) 2015 Thomas P. (http://kerbalspaceprogram.de), simon56modder
 * 
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 3 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston,
 * MA 02110-1301  USA
 * 
 * This library is intended to be used as a plugin for Kerbal Space Program
 * which is copyright 2011-2015 Squad. Your usage of Kerbal Space Program
 * itself is governed by the terms of its EULA, not the license above.
 * 
 * https://kerbalspaceprogram.com
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace LanguagePatches
{
    [KSPAddon(KSPAddon.Startup.Settings, false)]
    public class Setting : MonoBehaviour
    {
        public static bool toggle = true;

        void Awake()
        {
            toggle = Loader.loadCache;
        }
        void OnGUI()
        {
            // Settings window
                GUI.skin = HighLogic.Skin;
                if (toggle)
                {
                    toggle = GUI.Toggle(new Rect(Screen.width - 250, 0, 200, 50), toggle, " " + Loader.fullLang + " (" + Loader.mustRestart + ")");
                }
                else
                {
                    toggle = GUI.Toggle(new Rect(Screen.width - 250, 0, 200, 50), toggle, " English (Needs restart)");
                }
                GUI.Label(new Rect(10,10,200,200), "Translated in " + Loader.fullLangEnglish + " by " + Loader.credits);
                Loader.writeCache(toggle);
        }
    }
}
