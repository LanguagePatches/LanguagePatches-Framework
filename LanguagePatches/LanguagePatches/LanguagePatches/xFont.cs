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
using System.Runtime.InteropServices;
using UnityEngine;

namespace LanguagePatches
{
    public static class xFont
    {
        /// <summary>
        /// Patches the Font of a TextMesh
        /// </summary>
        /// <param name="mesh">The Mesh that we should patch</param>
        /// <param name="size">[Optional] New text-size of the mesh</param>
        public static void ReplaceFont(TextMesh mesh, [Optional][DefaultParameterValue(-1)] float size)
        {
            if (Storage.Font.trueTypeFonts != null && Storage.Font.trueTypeFonts.ContainsKey(mesh.font.name))
            {
                Font fnt = Storage.Font.trueTypeFonts[mesh.font.name];
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

        /// <summary>
        /// Patches the Font of a ScreenSafeGUIText
        /// </summary>
        /// <param name="text">The SSUI-Text that we should patch</param>
        /// <param name="size">[Optional] New text-size of the mesh</param>
        public static void ReplaceFont(ScreenSafeGUIText text, [Optional][DefaultParameterValue(-1)] float size)
        {
            if (Storage.Font.trueTypeFonts != null && Storage.Font.trueTypeFonts.ContainsKey(text.textStyle.font.name))
            {
                Font fnt = Storage.Font.trueTypeFonts[text.textStyle.font.name];
                text.textStyle.font = fnt;
                text.textStyle.richText = true;
                if (size != -1)
                {
                    text.text = "<size=" + size.ToString() + ">" + text.text + "</size>";
                }
            }
        }

        /// <summary>
        /// Patches the Font of a SpriteText
        /// </summary>
        /// <param name="text">The SpriteText that we should patch</param>
        public static void ReplaceFont(SpriteText text)
        {
            if (Storage.Font.spriteFont.ContainsKey(text.font.name))
            {
                Debug.Log(true);
                KeyValuePair<TextAsset, Material> font = Storage.Font.spriteFont[text.font.name];
                text.SetFont(font.Key, font.Value);
            }
        }

        /// <summary>
        /// Patches the Font of a SpriteTextRich
        /// </summary>
        /// <param name="text">The SpriteTextRich that we should patch</param>
        public static void ReplaceFont(SpriteTextRich text)
        {
            foreach (SpriteFontMultiple.SpriteFontInstance font in text.font.fonts)
            {
                if (Storage.Font.spriteFont.ContainsKey(font.name))
                {
                    KeyValuePair<TextAsset, Material> fontKV = Storage.Font.spriteFont[font.name];
                    font.fontText = fontKV.Key;
                    font.SpriteFont.Load(fontKV.Key);
                    font.material = fontKV.Value;
                }
            }
        }
    }
}