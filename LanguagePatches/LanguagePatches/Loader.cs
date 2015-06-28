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
using System.IO;
using UnityEngine;
using System.Reflection;

namespace LanguagePatches
{
    [KSPAddon(KSPAddon.Startup.Instantly, false)]
    public class Loader : MonoBehaviour
    {
        // Multiple paths
        public static string path = "";
        public static string images = "";
        public static string rawPath = "";
        public static string mustRestart = "";
        public static string fullLang = "";
        public static string fullLangEN = "";
        public static string version = "";
        public static string credits = "";
        public static string LangPrefix = "";
        public static string assetPath = "";
        public static bool watchdogActive = false;

        public void Awake()
        {
            // There can only be one config node
            if (GameDatabase.Instance.GetConfigNodes("LanguagePatches").Count() == 1)
            {
                // Get all the variables
                ConfigNode language = GameDatabase.Instance.GetConfigNodes("LanguagePatches")[0];
                path = language.GetValue("path") + "/" + language.GetNode("Root").GetValue("script") + "/" + language.GetValue("lang");
                images = language.GetValue("path") + "/" + language.GetNode("Root").GetValue("images");
                rawPath = language.GetValue("path");
                LangPrefix = language.GetValue("lang");
                mustRestart = language.GetNode("Settings").GetValue("mustRestart");
                fullLang = language.GetNode("Settings").GetValue("fullLang");
                fullLangEN = language.GetNode("Settings").GetValue("fullLangEnglish");
                version = language.GetNode("Settings").GetValue("version");
                credits = language.GetNode("Settings").GetValue("credits");

                // Create the CACHE file
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/" + rawPath + "/PluginData");
                Storage.cachePath = rawPath + "/PluginData/CACHE";
                if (!File.Exists(Storage.cachePath))
                {
                    Storage.Load = true;
                }

                // Get the path for the AssetFile
                assetPath = language.GetNode("Root").GetValue("fontAsset");

                // Get the Lock-File for the Cecil-Patcher
                watchdogActive = File.Exists(Assembly.GetAssembly(typeof(Game)).Location.Replace(".dll", ".old"));

                // Load the configuration
                Storage.LoadConfiguration();
            }
            else
            {
                // If we have multiple nodes, kill us
                Destroy(this);
            }
            
        }
    }
}
