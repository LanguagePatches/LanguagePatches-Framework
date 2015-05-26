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
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text.RegularExpressions;
using System.Xml;
using UnityEngine;

namespace LanguagePatches
{
    [KSPAddon(KSPAddon.Startup.EveryScene, false)]
    public class xText : MonoBehaviour
    {
        private string directory = AssemblyLoader.GetPathByType(typeof(xText)).Replace("PluginData/LanguagePatches", "Log");

        private static Dictionary<string, string> dict_Field;
        private Type[] types = new Type[] { typeof(SpriteText), typeof(SpriteTextRich), typeof(TextMesh), typeof(GUIContent) };
        private Dictionary<Type, IList> patchedText = new Dictionary<Type, IList>();
        TextWriter logger;

        public void Awake()
        {
            if (Loader.loadCache)
            {
                // Load SpriteText from .xml
                LoadDict();
                Directory.CreateDirectory(directory);
                logger = new StreamWriter(Path.Combine(directory, HighLogic.LoadedScene.ToString() + ".log"));

                // Create storing lists
                foreach (Type type in types)
                {
                    patchedText.Add(type, createList(type));
                }
            }
        }

        public void Update()
        {
            if (Loader.loadCache)
            {
                // Patch all SpriteTexts
                if (patchedText[typeof(SpriteText)].Count < Resources.FindObjectsOfTypeAll(typeof(SpriteText)).Length)
                {
                    // Go through all objects
                    foreach (SpriteText txt in Resources.FindObjectsOfTypeAll(typeof(SpriteText)))
                    {
                        if (!patchedText[typeof(SpriteText)].Contains(txt))
                        {
                            logger.WriteLine("[SpriteText] " + Escape(txt.Text));
                            txt.Text = xText.Trans(txt.Text);
                            xFont.FontIfy(txt);
                            patchedText[typeof(SpriteText)].Add(txt);
                            txt.UpdateMesh();

                        } 
                    }
                }

                // Patch all SpriteTextRichs
                if (patchedText[typeof(SpriteTextRich)].Count < Resources.FindObjectsOfTypeAll(typeof(SpriteTextRich)).Length)
                {
                    // Go through all objects
                    foreach (SpriteTextRich txt in Resources.FindObjectsOfTypeAll(typeof(SpriteTextRich)))
                    {
                        if (!patchedText[typeof(SpriteTextRich)].Contains(txt))
                        {
                            logger.WriteLine("[SpriteTextRich] " + Escape(txt.Text));
                            txt.Text = xText.Trans(txt.text);
                            xFont.FontIfy(txt);
                            patchedText[typeof(SpriteTextRich)].Add(txt);
                            txt.UpdateMesh();
                        }
                    }
                }

                // Patch all SpriteTextRichs
                if (patchedText[typeof(TextMesh)].Count < Resources.FindObjectsOfTypeAll(typeof(TextMesh)).Length)
                {
                    // Go through all objects
                    foreach (TextMesh txt in Resources.FindObjectsOfTypeAll(typeof(TextMesh)))
                    {
                        if (!patchedText[typeof(TextMesh)].Contains(txt))
                        {
                            logger.WriteLine("[TextMesh] " + Escape(txt.text));
                            txt.text = xText.Trans(txt.text);
                            xFont.FontIfy(txt);
                            patchedText[typeof(TextMesh)].Add(txt);
                        }
                    }
                }
                
                logger.Flush();
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
                    xText.dict_Field[((XmlCDataSection)childNode.FirstChild.FirstChild).InnerText] = ((XmlCDataSection)childNode.LastChild.FirstChild).InnerText;
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
            if (dict_Field == null)
            {
                return value;
            }
            else
            {
                str = (!xText.dict_Field.ContainsKey(Escape(value))) ? value : Unescape(xText.dict_Field[Escape(value)]);
                Debug.Log(str + " - " + xText.dict_Field.ContainsKey(Escape(value)));
                return str;
            }
        }

        public IList createList(Type myType)
        {
            Type genericListType = typeof(List<>).MakeGenericType(myType);
            return (IList)Activator.CreateInstance(genericListType);
        }

        public static string Escape(string s)
        {
            s = s.Replace("\n", "\\n").Replace("\r", "\\r").Replace("\t", "\\t");
            return s;
        }

        public static string Unescape(string s)
        {
            s = s.Replace(@"\n", "\n").Replace(@"\r", "\r").Replace(@"\t", "\t");
            return s;
        }
    }
}
