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

using System.Linq;
using UnityEngine;

namespace LanguagePatches
{
    [KSPAddon(KSPAddon.Startup.Instantly, false)]
    public class Loader : MonoBehaviour
    {
        // Multiple paths
        private static string Ipath = "";
        private static string Iimages = "";
        private static string Iraw = ""; 

        public void Awake()
        {
            // There can only be one config node
            if (GameDatabase.Instance.GetConfigNodes("LanguagePatches").Count() == 1) {
                ConfigNode language = GameDatabase.Instance.GetConfigNodes("LanguagePatches")[0];
                Ipath = language.GetValue("path") + "/" + language.GetNode("Root").GetValue("script") + "/" + language.GetValue("lang");
                Iimages = language.GetValue("path") + "/" + language.GetNode("Root").GetValue("images");
                Iraw = language.GetValue("path");
            }
            else
            {
                // If we have multiple nodes, kill us
                Destroy(this);
            }
            
        }

        public static string path
        {
            get { return Ipath; }
            set { Ipath = value; }
        }

        public static string images
        {
            get { return Iimages; }
            set { Iimages = value; }
        }

        public static string rawPath
        {
            get { return Iraw; }
            set { Iraw = value; }
        }
    }
}
