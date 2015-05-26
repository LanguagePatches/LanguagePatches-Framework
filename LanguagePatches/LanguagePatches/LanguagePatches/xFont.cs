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
using System.Xml;
using UnityEngine;

namespace LanguagePatches
{
    public static class xFont
    {
        private static AssetBundle asset;

        private static Dictionary<string, Font> fonts = null;
        private static Dictionary<string, KeyValuePair<TextAsset, Material>> spriteFonts = null;

        public static void FontIfy(TextMesh mesh, float size = -1)
        {
            if (fonts == null)
                GetConfig();

            if (fonts != null && fonts.ContainsKey(mesh.font.name)) 
            {
                Font fnt = fonts[mesh.font.name];
                Color o = mesh.renderer.sharedMaterial.color;
                mesh.renderer.sharedMaterial = fnt.material;
                mesh.renderer.sharedMaterial.color = o;
                mesh.font = fnt;
                mesh.richText = true;
                if (size != -1)
                {
                    mesh.text = "<size=" + size.ToString() + ">" + mesh.text + "</size>";
                }
            }
        }



        public static void FontIfy(ScreenSafeGUIText text, float size = -1)
        {
            if (fonts == null)
                GetConfig();

            if (fonts != null && fonts.ContainsKey(text.textStyle.font.name))
            {
                Font fnt = fonts[text.textStyle.font.name];
                text.textStyle.font = fnt;
                text.textStyle.richText = true;
                if (size != -1)
                {
                    text.text = "<size=" + size.ToString() + ">" + text.text + "</size>";
                }
            }

        }

        public static void FontIfy(SpriteText text)
        {
            if (spriteFonts.ContainsKey(text.font.name))
            {
                KeyValuePair<TextAsset, Material> kv = spriteFonts[text.font.name];
                SpriteFont f = new SpriteFont(kv.Key);
                text.SetFont(f, kv.Value);
            }
        }

        public static void FontIfy(SpriteTextRich text)
        {
            for (int i = 0; i < text.font.fonts.Length; i++)
            {
                Debug.Log("SFNT: " + text.font.fonts[i].fontText.name + " - " + text.text);
            }
        }

        private static void GetConfig()
        {
            // Does the config exists?
            if (File.Exists(Loader.path + "Font.xml"))
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(Loader.path + "Font.xml");
                asset = new WWW("file:///" + Directory.GetCurrentDirectory() + "/" + Loader.rawPath + "/" + xmlDocument.DocumentElement.GetAttribute("asset") + ".unity3d").assetBundle;
                fonts = new Dictionary<string, Font>();
                spriteFonts = new Dictionary<string, KeyValuePair<TextAsset, Material>>();
                foreach (XmlElement childNode in xmlDocument.DocumentElement.ChildNodes)
                {
                    if (childNode.GetAttribute("type") == "font")
                    {
                        fonts.Add(childNode.GetAttribute("name"), asset.Load(childNode.InnerText, typeof(Font)) as Font);
                    }
                    else if (childNode.GetAttribute("type") == "spriteFont")
                    {
                        TextAsset key = asset.Load(childNode.InnerText, typeof(TextAsset)) as TextAsset;
                        Material value = new Material(Shader.Find("Sprite/Vertex Colored"));
                        Texture2D tex = asset.Load(childNode.InnerText, typeof(Texture2D)) as Texture2D;
                        value.SetTexture("_MainTex", tex);
                        Material.DontDestroyOnLoad(value);

                        KeyValuePair<TextAsset, Material> kv = new KeyValuePair<TextAsset, Material>(key, value);
                        spriteFonts.Add(childNode.GetAttribute("name"), kv);
                    }
                }
            }
        }
    }
}
