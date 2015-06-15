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
    [KSPAddon(KSPAddon.Startup.Instantly, true)]
    public class xLoading : MonoBehaviour
    {
        private List<string> mytips = new List<string>();
        private string nowText = "";
        private float size = 0;
        private TextMesh loadText;
        private System.Random random = new System.Random();
        private bool fontIfy = true;

        private void Start()
        {
            if (Loader.loadCache)
            {
            // Does our config exists?
                if (File.Exists(Loader.path + "LoadingTips.xml"))
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.Load(Loader.path + "LoadingTips.xml");
                    foreach (XmlElement childNode in xmlDocument.DocumentElement.ChildNodes)
                    {
                        this.mytips.Add(childNode.InnerText ?? "");
                    }

                    if (xmlDocument.DocumentElement.HasAttribute("size"))
                    {
                        size = float.Parse(xmlDocument.DocumentElement.GetAttribute("size"));
                    }

                    if (xmlDocument.DocumentElement.HasAttribute("replaceFont"))
                    {
                        fontIfy = bool.Parse(xmlDocument.DocumentElement.GetAttribute("replaceFont"));
                    }
                }
            }
        }

        private void Update()
        {
            if (Loader.loadCache)
            {
                // Get all TextMeshes and replace their text / font.
                if (this.loadText == null)
                {
                    GameObject gameObject = GameObject.Find("Text");
                    if (gameObject == null)
                    {
                        return;
                    }
                    this.loadText = gameObject.GetComponent<TextMesh>();
                    if (size == 0) size = this.loadText.characterSize;
                }
                if (this.nowText != this.loadText.text)
                {
                    int num = this.random.Next(this.mytips.Count);
                    this.loadText.text = string.Concat("", this.mytips[num]);
                    this.nowText = this.loadText.text;
                    if (this.mytips.Count > 1)
                    {
                        this.mytips.RemoveAt(num);
                    }
                    if (fontIfy)
                        xFont.FontIfy(this.loadText, size);
                }
            }
        }
    }
    [KSPAddon(KSPAddon.Startup.EveryScene, false)]
    public class LoadingText : MonoBehaviour
    {
        void Update()
        {
            TextMesh[] textMeshes = GameObject.FindObjectsOfType<TextMesh>();
            foreach (TextMesh TM in textMeshes)
            {
                if (TM.text.Contains("Loading..."))
                {
                    TM.text = "<size=25>" + Loader.Loading + "</size>";
                    // Changing font (both this following way or using xFont.fontIfy(TextMesh mesh, float size)) makes the text not to load! :
                    // TM.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
                }
            }
        }
    }
}
