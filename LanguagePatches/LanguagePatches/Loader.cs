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

namespace LanguagePatches
{
    [KSPAddon(KSPAddon.Startup.Instantly, false)]
    public class Loader : MonoBehaviour
    {
        public static void writeCache(bool toggle)
        {
            if (File.Exists(cachePath)) { File.Delete(cachePath); }
            TextWriter cache = new StreamWriter(cachePath);
            cache.Write(toggle);
            cache.Close();
        }
        public static bool loadCache
        {
            get
            {
                bool status = false;
                StreamReader cacheReader = new StreamReader(cachePath);
                string line = cacheReader.ReadLine();
                cacheReader.Close();
                if (bool.Parse(line) != status) status = bool.Parse(line);
                return status;
            }
        }
        // Multiple paths
        private static string Ipath = "";
        private static string Iimages = "";
        private static string Iraw = "";
        private static string ImustRestart = "";
        private static string IfullLang = "";
        private static string IfullLangEN = "";
        private static string Iversion = "";
        private static string Icredits = "";
        private static string cachePath = "";
        public void Awake()
        {
            // There can only be one config node
            if (GameDatabase.Instance.GetConfigNodes("LanguagePatches").Count() == 1) {
                ConfigNode language = GameDatabase.Instance.GetConfigNodes("LanguagePatches")[0];
                Ipath = language.GetValue("path") + "/" + language.GetNode("Root").GetValue("script") + "/" + language.GetValue("lang");
                Iimages = language.GetValue("path") + "/" + language.GetNode("Root").GetValue("images");
                Iraw = language.GetValue("path");
                ImustRestart = language.GetNode("Settings").GetValue("mustRestart");
                IfullLang = language.GetNode("Settings").GetValue("fullLang");
                IfullLangEN = language.GetNode("Settings").GetValue("fullLangEnglish");
                Iversion = language.GetNode("Settings").GetValue("version");
                Icredits = language.GetNode("Settings").GetValue("credits");

                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/" + Iraw + "/PluginData");
                cachePath = Iraw + "/PluginData/CACHE";
                if (!File.Exists(cachePath))
                {
                    writeCache(true);
                }
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

        public static string mustRestart
        {
            get { return ImustRestart; }
            set { ImustRestart = value; }
        }

        public static string fullLang
        {
            get { return IfullLang; }
            set { IfullLang = value; }
        }
        
        public static string credits
        {
            get { return Icredits; }
            set { Icredits = value; }
        }
        public static string version
        {
            get { return Iversion; }
            set { Iversion = value; }
        }
        public static string fullLangEnglish
        {
            get { return IfullLangEN; }
            set { IfullLangEN = value; }
        }
    }
}
