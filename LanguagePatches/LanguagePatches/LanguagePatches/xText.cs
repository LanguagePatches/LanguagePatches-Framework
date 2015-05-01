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

using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

namespace LanguagePatches
{
    [KSPAddon(KSPAddon.Startup.EveryScene, false)]
    public class xText : MonoBehaviour
    {
        private string directory = AssemblyLoader.GetPathByType(typeof(xText)).Replace("PluginData/LanguagePatches", "Log");

        private static Dictionary<string, string> dict_Field;
        private List<SpriteText> patched = new List<SpriteText>();
        private List<SpriteTextRich> patchedRich = new List<SpriteTextRich>();
        TextWriter logger;
        bool[] finish = new bool[2];

        public void Awake()
        {
            if (Loader.loadCache)
            {
                // Load SpriteText from .xml
                LoadDict();
                Directory.CreateDirectory(directory);
                logger = new StreamWriter(Path.Combine(directory, HighLogic.LoadedScene.ToString() + ".log"));
            }
        }

        public void Update()
        {
            if (Loader.loadCache)
            {
                // Patch all SpriteTexts
                if (patched.Count < Resources.FindObjectsOfTypeAll(typeof(SpriteText)).Length)
                {
                    // Go through all objects
                    foreach (SpriteText txt in Resources.FindObjectsOfTypeAll(typeof(SpriteText)))
                    {
                        if (!patched.Contains(txt))
                        {
                            logger.WriteLine("[SpriteText] " + txt.Text);
                            txt.Text = xText.Trans(txt.Text);
                            xFont.FontIfy(txt);
                            patched.Add(txt);
                            txt.UpdateMesh();

                        }
                    }
                }
                else
                {
                    finish[0] = true;
                }

                // Patch all SpriteTextRichs
                if (patchedRich.Count < Resources.FindObjectsOfTypeAll(typeof(SpriteTextRich)).Length)
                {
                    // Go through all objects
                    foreach (SpriteTextRich txt in Resources.FindObjectsOfTypeAll(typeof(SpriteTextRich)))
                    {
                        if (!patchedRich.Contains(txt))
                        {
                            logger.WriteLine("[SpriteTextRich] " + txt.Text);
                            txt.Text = xText.Trans(txt.text);
                            xFont.FontIfy(txt);
                            patchedRich.Add(txt);
                            txt.UpdateMesh();
                        }
                    }
                }
                else
                {
                    finish[1] = true;
                }

                if (finish[0] && finish[1])
                {
                    logger.Flush();
                }
            }
        }


        public static void LoadDict()
        {
            // Does the config exists?
            if (File.Exists(Loader.path + "Text.xml"))
            {
                XmlDocument xmlDocument = new XmlDocument();
                xText.dict_Field = new Dictionary<string, string>();
                xmlDocument.Load(Loader.path + "Text.xml");
                foreach (XmlElement childNode in xmlDocument.DocumentElement.ChildNodes)
                {
                    xText.dict_Field[childNode.GetAttribute("name")] = ((XmlCDataSection)childNode.FirstChild).InnerText;
                }
            }
        }

        // Translate a SpriteText
        public static string Trans(string value)
        {
            string str;
            if (xText.dict_Field == null)
            {
                xText.LoadDict();
            }
            str = (!xText.dict_Field.ContainsKey(value) ? value : xText.dict_Field[value]);
            return str;
        }
    }
}
