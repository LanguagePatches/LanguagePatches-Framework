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
 * Thomas User Interface - dynamic wrapper for Unitys legacy GUI.
 * 
 * https://kerbalspaceprogram.com
 */

using LanguagePatches;
using System.Collections.Generic;

namespace UnityEngine
{
	// Wrapper class for Unity's GUI and GUILayout classes, to support patching of GUI-texts
	public class TUILayout
	{
        private static List<string> logged = new List<string>();

		public static void Label(UnityEngine.Texture image, UnityEngine.GUILayoutOption[] options)
		{
			GUILayout.Label(image, options);
		}

		public static void Label(System.String text, UnityEngine.GUILayoutOption[] options)
		{
			GUILayout.Label(Trans(text), options);
		}

		public static void Label(UnityEngine.GUIContent content, UnityEngine.GUILayoutOption[] options)
		{
			GUILayout.Label(Trans(content), options);
		}

		public static void Label(UnityEngine.Texture image, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			GUILayout.Label(image, style, options);
		}

		public static void Label(System.String text, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			GUILayout.Label(Trans(text), style, options);
		}

		public static void Label(UnityEngine.GUIContent content, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			GUILayout.Label(Trans(content), style, options);
		}

		public static void Box(UnityEngine.Texture image, UnityEngine.GUILayoutOption[] options)
		{
			GUILayout.Box(image, options);
		}

		public static void Box(System.String text, UnityEngine.GUILayoutOption[] options)
		{
			GUILayout.Box(Trans(text), options);
		}

		public static void Box(UnityEngine.GUIContent content, UnityEngine.GUILayoutOption[] options)
		{
			GUILayout.Box(Trans(content), options);
		}

		public static void Box(UnityEngine.Texture image, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			GUILayout.Box(image, style, options);
		}

		public static void Box(System.String text, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			GUILayout.Box(Trans(text), style, options);
		}

		public static void Box(UnityEngine.GUIContent content, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			GUILayout.Box(Trans(content), style, options);
		}

		public static System.Boolean Button(UnityEngine.Texture image, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.Button(image, options);
		}

		public static System.Boolean Button(System.String text, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.Button(Trans(text), options);
		}

		public static System.Boolean Button(UnityEngine.GUIContent content, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.Button(Trans(content), options);
		}

		public static System.Boolean Button(UnityEngine.Texture image, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.Button(image, style, options);
		}

		public static System.Boolean Button(System.String text, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.Button(Trans(text), style, options);
		}

		public static System.Boolean Button(UnityEngine.GUIContent content, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.Button(Trans(content), style, options);
		}

		public static System.Boolean RepeatButton(UnityEngine.Texture image, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.RepeatButton(image, options);
		}

		public static System.Boolean RepeatButton(System.String text, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.RepeatButton(Trans(text), options);
		}

		public static System.Boolean RepeatButton(UnityEngine.GUIContent content, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.RepeatButton(Trans(content), options);
		}

		public static System.Boolean RepeatButton(UnityEngine.Texture image, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.RepeatButton(image, style, options);
		}

		public static System.Boolean RepeatButton(System.String text, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.RepeatButton(Trans(text), style, options);
		}

		public static System.Boolean RepeatButton(UnityEngine.GUIContent content, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.RepeatButton(Trans(content), style, options);
		}

		public static System.String TextField(System.String text, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.TextField(Trans(text), options);
		}

		public static System.String TextField(System.String text, System.Int32 maxLength, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.TextField(Trans(text), maxLength, options);
		}

		public static System.String TextField(System.String text, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.TextField(Trans(text), style, options);
		}

		public static System.String TextField(System.String text, System.Int32 maxLength, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.TextField(Trans(text), maxLength, style, options);
		}

		public static System.String PasswordField(System.String password, System.Char maskChar, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.PasswordField(Trans(password), maskChar, options);
		}

		public static System.String PasswordField(System.String password, System.Char maskChar, System.Int32 maxLength, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.PasswordField(Trans(password), maskChar, maxLength, options);
		}

		public static System.String PasswordField(System.String password, System.Char maskChar, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.PasswordField(Trans(password), maskChar, style, options);
		}

		public static System.String PasswordField(System.String password, System.Char maskChar, System.Int32 maxLength, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.PasswordField(Trans(password), maskChar, maxLength, style, options);
		}

		public static System.String TextArea(System.String text, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.TextArea(Trans(text), options);
		}

		public static System.String TextArea(System.String text, System.Int32 maxLength, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.TextArea(Trans(text), maxLength, options);
		}

		public static System.String TextArea(System.String text, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.TextArea(Trans(text), style, options);
		}

		public static System.String TextArea(System.String text, System.Int32 maxLength, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.TextArea(Trans(text), maxLength, style, options);
		}

		public static System.Boolean Toggle(System.Boolean value, UnityEngine.Texture image, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.Toggle(value, image, options);
		}

		public static System.Boolean Toggle(System.Boolean value, System.String text, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.Toggle(value, Trans(text), options);
		}

		public static System.Boolean Toggle(System.Boolean value, UnityEngine.GUIContent content, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.Toggle(value, Trans(content), options);
		}

		public static System.Boolean Toggle(System.Boolean value, UnityEngine.Texture image, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.Toggle(value, image, style, options);
		}

		public static System.Boolean Toggle(System.Boolean value, System.String text, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.Toggle(value, Trans(text), style, options);
		}

		public static System.Boolean Toggle(System.Boolean value, UnityEngine.GUIContent content, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.Toggle(value, Trans(content), style, options);
		}

		public static System.Int32 Toolbar(System.Int32 selected, System.String[] texts, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, texts, options);
		}

		public static System.Int32 Toolbar(System.Int32 selected, UnityEngine.Texture[] images, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, images, options);
		}

		public static System.Int32 Toolbar(System.Int32 selected, UnityEngine.GUIContent[] content, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, content, options);
		}

		public static System.Int32 Toolbar(System.Int32 selected, System.String[] texts, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, texts, style, options);
		}

		public static System.Int32 Toolbar(System.Int32 selected, UnityEngine.Texture[] images, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, images, style, options);
		}

		public static System.Int32 Toolbar(System.Int32 selected, UnityEngine.GUIContent[] contents, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, contents, style, options);
		}

		public static System.Int32 SelectionGrid(System.Int32 selected, System.String[] texts, System.Int32 xCount, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.SelectionGrid(selected, texts, xCount, options);
		}

		public static System.Int32 SelectionGrid(System.Int32 selected, UnityEngine.Texture[] images, System.Int32 xCount, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.SelectionGrid(selected, images, xCount, options);
		}

		public static System.Int32 SelectionGrid(System.Int32 selected, UnityEngine.GUIContent[] content, System.Int32 xCount, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.SelectionGrid(selected, content, xCount, options);
		}

		public static System.Int32 SelectionGrid(System.Int32 selected, System.String[] texts, System.Int32 xCount, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.SelectionGrid(selected, texts, xCount, style, options);
		}

		public static System.Int32 SelectionGrid(System.Int32 selected, UnityEngine.Texture[] images, System.Int32 xCount, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.SelectionGrid(selected, images, xCount, style, options);
		}

		public static System.Int32 SelectionGrid(System.Int32 selected, UnityEngine.GUIContent[] contents, System.Int32 xCount, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.SelectionGrid(selected, contents, xCount, style, options);
		}

		public static System.Single HorizontalSlider(System.Single value, System.Single leftValue, System.Single rightValue, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.HorizontalSlider(value, leftValue, rightValue, options);
		}

		public static System.Single HorizontalSlider(System.Single value, System.Single leftValue, System.Single rightValue, UnityEngine.GUIStyle slider, UnityEngine.GUIStyle thumb, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.HorizontalSlider(value, leftValue, rightValue, slider, thumb, options);
		}

		public static System.Single VerticalSlider(System.Single value, System.Single leftValue, System.Single rightValue, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.VerticalSlider(value, leftValue, rightValue, options);
		}

		public static System.Single VerticalSlider(System.Single value, System.Single leftValue, System.Single rightValue, UnityEngine.GUIStyle slider, UnityEngine.GUIStyle thumb, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.VerticalSlider(value, leftValue, rightValue, slider, thumb, options);
		}

		public static System.Single HorizontalScrollbar(System.Single value, System.Single size, System.Single leftValue, System.Single rightValue, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.HorizontalScrollbar(value, size, leftValue, rightValue, options);
		}

		public static System.Single HorizontalScrollbar(System.Single value, System.Single size, System.Single leftValue, System.Single rightValue, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.HorizontalScrollbar(value, size, leftValue, rightValue, style, options);
		}

		public static System.Single VerticalScrollbar(System.Single value, System.Single size, System.Single topValue, System.Single bottomValue, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.VerticalScrollbar(value, size, topValue, bottomValue, options);
		}

		public static System.Single VerticalScrollbar(System.Single value, System.Single size, System.Single topValue, System.Single bottomValue, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.VerticalScrollbar(value, size, topValue, bottomValue, style, options);
		}

		public static void Space(System.Single pixels)
		{
			GUILayout.Space(pixels);
		}

		public static void FlexibleSpace()
		{
			GUILayout.FlexibleSpace();
		}

		public static void BeginHorizontal(UnityEngine.GUILayoutOption[] options)
		{
			GUILayout.BeginHorizontal(options);
		}

		public static void BeginHorizontal(UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			GUILayout.BeginHorizontal(style, options);
		}

		public static void BeginHorizontal(System.String text, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			GUILayout.BeginHorizontal(Trans(text), style, options);
		}

		public static void BeginHorizontal(UnityEngine.Texture image, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			GUILayout.BeginHorizontal(image, style, options);
		}

		public static void BeginHorizontal(UnityEngine.GUIContent content, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			GUILayout.BeginHorizontal(Trans(content), style, options);
		}

		public static void EndHorizontal()
		{
			GUILayout.EndHorizontal();
		}

		public static void BeginVertical(UnityEngine.GUILayoutOption[] options)
		{
			GUILayout.BeginVertical(options);
		}

		public static void BeginVertical(UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			GUILayout.BeginVertical(style, options);
		}

		public static void BeginVertical(System.String text, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			GUILayout.BeginVertical(Trans(text), style, options);
		}

		public static void BeginVertical(UnityEngine.Texture image, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			GUILayout.BeginVertical(image, style, options);
		}

		public static void BeginVertical(UnityEngine.GUIContent content, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			GUILayout.BeginVertical(Trans(content), style, options);
		}

		public static void EndVertical()
		{
			GUILayout.EndVertical();
		}

		public static void BeginArea(UnityEngine.Rect screenRect)
		{
			GUILayout.BeginArea(screenRect);
		}

		public static void BeginArea(UnityEngine.Rect screenRect, System.String text)
		{
			GUILayout.BeginArea(screenRect, Trans(text));
		}

		public static void BeginArea(UnityEngine.Rect screenRect, UnityEngine.Texture image)
		{
			GUILayout.BeginArea(screenRect, image);
		}

		public static void BeginArea(UnityEngine.Rect screenRect, UnityEngine.GUIContent content)
		{
			GUILayout.BeginArea(screenRect, Trans(content));
		}

		public static void BeginArea(UnityEngine.Rect screenRect, UnityEngine.GUIStyle style)
		{
			GUILayout.BeginArea(screenRect, style);
		}

		public static void BeginArea(UnityEngine.Rect screenRect, System.String text, UnityEngine.GUIStyle style)
		{
			GUILayout.BeginArea(screenRect, Trans(text), style);
		}

		public static void BeginArea(UnityEngine.Rect screenRect, UnityEngine.Texture image, UnityEngine.GUIStyle style)
		{
			GUILayout.BeginArea(screenRect, image, style);
		}

		public static void BeginArea(UnityEngine.Rect screenRect, UnityEngine.GUIContent content, UnityEngine.GUIStyle style)
		{
			GUILayout.BeginArea(screenRect, Trans(content), style);
		}

		public static void EndArea()
		{
			GUILayout.EndArea();
		}

		public static UnityEngine.Vector2 BeginScrollView(UnityEngine.Vector2 scrollPosition, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.BeginScrollView(scrollPosition, options);
		}

		public static UnityEngine.Vector2 BeginScrollView(UnityEngine.Vector2 scrollPosition, System.Boolean alwaysShowHorizontal, System.Boolean alwaysShowVertical, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, options);
		}

		public static UnityEngine.Vector2 BeginScrollView(UnityEngine.Vector2 scrollPosition, UnityEngine.GUIStyle horizontalScrollbar, UnityEngine.GUIStyle verticalScrollbar, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.BeginScrollView(scrollPosition, horizontalScrollbar, verticalScrollbar, options);
		}

		public static UnityEngine.Vector2 BeginScrollView(UnityEngine.Vector2 scrollPosition, UnityEngine.GUIStyle style)
		{
			return GUILayout.BeginScrollView(scrollPosition, style);
		}

		public static UnityEngine.Vector2 BeginScrollView(UnityEngine.Vector2 scrollPosition, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.BeginScrollView(scrollPosition, style, options);
		}

		public static UnityEngine.Vector2 BeginScrollView(UnityEngine.Vector2 scrollPosition, System.Boolean alwaysShowHorizontal, System.Boolean alwaysShowVertical, UnityEngine.GUIStyle horizontalScrollbar, UnityEngine.GUIStyle verticalScrollbar, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, options);
		}

		public static UnityEngine.Vector2 BeginScrollView(UnityEngine.Vector2 scrollPosition, System.Boolean alwaysShowHorizontal, System.Boolean alwaysShowVertical, UnityEngine.GUIStyle horizontalScrollbar, UnityEngine.GUIStyle verticalScrollbar, UnityEngine.GUIStyle background, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, background, options);
		}

		public static void EndScrollView()
		{
			GUILayout.EndScrollView();
		}

		public static UnityEngine.Rect Window(System.Int32 id, UnityEngine.Rect screenRect, UnityEngine.GUI.WindowFunction func, System.String text, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.Window(id, screenRect, func, Trans(text), options);
		}

		public static UnityEngine.Rect Window(System.Int32 id, UnityEngine.Rect screenRect, UnityEngine.GUI.WindowFunction func, UnityEngine.Texture image, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.Window(id, screenRect, func, image, options);
		}

		public static UnityEngine.Rect Window(System.Int32 id, UnityEngine.Rect screenRect, UnityEngine.GUI.WindowFunction func, UnityEngine.GUIContent content, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.Window(id, screenRect, func, Trans(content), options);
		}

		public static UnityEngine.Rect Window(System.Int32 id, UnityEngine.Rect screenRect, UnityEngine.GUI.WindowFunction func, System.String text, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.Window(id, screenRect, func, Trans(text), style, options);
		}

		public static UnityEngine.Rect Window(System.Int32 id, UnityEngine.Rect screenRect, UnityEngine.GUI.WindowFunction func, UnityEngine.Texture image, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.Window(id, screenRect, func, image, style, options);
		}

		public static UnityEngine.Rect Window(System.Int32 id, UnityEngine.Rect screenRect, UnityEngine.GUI.WindowFunction func, UnityEngine.GUIContent content, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options)
		{
			return GUILayout.Window(id, screenRect, func, Trans(content), style, options);
		}

		public static UnityEngine.GUILayoutOption Width(System.Single width)
		{
			return GUILayout.Width(width);
		}

		public static UnityEngine.GUILayoutOption MinWidth(System.Single minWidth)
		{
			return GUILayout.MinWidth(minWidth);
		}

		public static UnityEngine.GUILayoutOption MaxWidth(System.Single maxWidth)
		{
			return GUILayout.MaxWidth(maxWidth);
		}

		public static UnityEngine.GUILayoutOption Height(System.Single height)
		{
			return GUILayout.Height(height);
		}

		public static UnityEngine.GUILayoutOption MinHeight(System.Single minHeight)
		{
			return GUILayout.MinHeight(minHeight);
		}

		public static UnityEngine.GUILayoutOption MaxHeight(System.Single maxHeight)
		{
			return GUILayout.MaxHeight(maxHeight);
		}

		public static UnityEngine.GUILayoutOption ExpandWidth(System.Boolean expand)
		{
			return GUILayout.ExpandWidth(expand);
		}

		public static UnityEngine.GUILayoutOption ExpandHeight(System.Boolean expand)
		{
			return GUILayout.ExpandHeight(expand);
		}

        // Translation Utility
        private static string Trans(string text)
        {
            // Log Content
            if (!logged.Contains(text))
            {
                TUI.log.WriteLine("[GUIText] " + xText.Escape(text));
                TUI.log.Flush();
                logged.Add(text);
            }

            // Translate Content
            if (Storage.TUI.texts.Count > 0)
            {
                text = (!Storage.TUI.texts.ContainsKey(xText.Escape(text))) ?
                        text :
                        xText.Unescape(Storage.TUI.texts[xText.Escape(text)]);
            }

            // Return Content
            return text;
        }

        private static GUIContent Trans(GUIContent content)
        {
            // Log Content
            if (!logged.Contains(content.text) && !logged.Contains(content.tooltip))
            {
                TUI.log.WriteLine("[GUIContent] " + xText.Escape(content.text) + " (Tooltip: " + xText.Escape(content.tooltip) + ")");
                TUI.log.Flush();
                logged.Add(content.text);
                logged.Add(content.tooltip);
            }

            // Translate Content
            if (Storage.TUI.texts.Count > 0)
            {
                content.text = (!Storage.TUI.texts.ContainsKey(xText.Escape(content.text))) ?
                    content.text :
                    xText.Unescape(Storage.Text.texts[xText.Escape(content.text)]);
                content.tooltip = (!Storage.TUI.texts.ContainsKey(xText.Escape(content.tooltip))) ?
                    content.tooltip :
                    xText.Unescape(Storage.TUI.texts[xText.Escape(content.tooltip)]);
            }

            // Return Content
            return content;
        }
	}
}