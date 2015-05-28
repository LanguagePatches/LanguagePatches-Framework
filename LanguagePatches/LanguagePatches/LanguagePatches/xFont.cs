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
        private static Dictionary<string, string> fonts = null;

        public static void FontIfy(TextMesh mesh, float size = -1)
        {
                Font Arial = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font; 
                Color o = mesh.renderer.sharedMaterial.color;
                mesh.renderer.sharedMaterial = Arial.material;
                mesh.renderer.sharedMaterial.color = o;
                mesh.font = Arial;
                mesh.richText = true;
                if (size != -1)
                {
                    mesh.text = "<size=" + size.ToString() + ">" + mesh.text + "</size>";
                }
        }

        public static void FontIfy(ScreenSafeGUIText text, float size = -1)
        {
            Font Arial = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            text.textStyle.font = Arial;
            text.textStyle.richText = true;
            if (size != -1)
            {
                text.text = "<size=" + size.ToString() + ">" + text.text + "</size>";
            }
        }

        public static void FontIfy(SpriteText text)
        {

        }

        public static void FontIfy(SpriteTextRich text)
        {

        }

        private static void GetConfig()
        {
            // Does the config exists?
            if (File.Exists(Loader.path + "Font.xml"))
            {
                XmlDocument xmlDocument = new XmlDocument();
                fonts = new Dictionary<string, string>();
                xmlDocument.Load(Loader.path + "Font.xml");
                foreach (XmlElement childNode in xmlDocument.DocumentElement.ChildNodes)
                {
                    fonts[childNode.GetAttribute("name")] = childNode.InnerText;
                }
            }
        }
    }
}
