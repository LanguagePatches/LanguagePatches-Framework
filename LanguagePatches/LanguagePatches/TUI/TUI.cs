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
using System.IO;

namespace UnityEngine
{
	// Wrapper class for Unity's GUI and GUILayout classes, to support patching of GUI-texts
	public class TUI
	{
        private static System.IO.StreamWriter _log = null;

        public static System.IO.StreamWriter log
        {
            get
            {
                if (_log == null)
                {
                    Directory.CreateDirectory(LanguagePatches.Storage.directory + "/Logs");
                    _log = new StreamWriter(LanguagePatches.Storage.directory + "/Logs/TUI.log");
                }
                return _log;
            }
        }

		public static System.Single HorizontalSlider(UnityEngine.Rect position, System.Single value, System.Single leftValue, System.Single rightValue)
		{
			return GUI.HorizontalSlider(position, value, leftValue, rightValue);
		}

		public static System.Single HorizontalSlider(UnityEngine.Rect position, System.Single value, System.Single leftValue, System.Single rightValue, UnityEngine.GUIStyle slider, UnityEngine.GUIStyle thumb)
		{
			return GUI.HorizontalSlider(position, value, leftValue, rightValue, slider, thumb);
		}

		public static System.Single VerticalSlider(UnityEngine.Rect position, System.Single value, System.Single topValue, System.Single bottomValue)
		{
			return GUI.VerticalSlider(position, value, topValue, bottomValue);
		}

		public static System.Single VerticalSlider(UnityEngine.Rect position, System.Single value, System.Single topValue, System.Single bottomValue, UnityEngine.GUIStyle slider, UnityEngine.GUIStyle thumb)
		{
			return GUI.VerticalSlider(position, value, topValue, bottomValue, slider, thumb);
		}

		public static System.Single Slider(UnityEngine.Rect position, System.Single value, System.Single size, System.Single start, System.Single end, UnityEngine.GUIStyle slider, UnityEngine.GUIStyle thumb, System.Boolean horiz, System.Int32 id)
		{
			return GUI.Slider(position, value, size, start, end, slider, thumb, horiz, id);
		}

		public static System.Single HorizontalScrollbar(UnityEngine.Rect position, System.Single value, System.Single size, System.Single leftValue, System.Single rightValue)
		{
			return GUI.HorizontalScrollbar(position, value, size, leftValue, rightValue);
		}

		public static System.Single HorizontalScrollbar(UnityEngine.Rect position, System.Single value, System.Single size, System.Single leftValue, System.Single rightValue, UnityEngine.GUIStyle style)
		{
			return GUI.HorizontalScrollbar(position, value, size, leftValue, rightValue, style);
		}

		public static System.Single VerticalScrollbar(UnityEngine.Rect position, System.Single value, System.Single size, System.Single topValue, System.Single bottomValue)
		{
			return GUI.VerticalScrollbar(position, value, size, topValue, bottomValue);
		}

		public static System.Single VerticalScrollbar(UnityEngine.Rect position, System.Single value, System.Single size, System.Single topValue, System.Single bottomValue, UnityEngine.GUIStyle style)
		{
			return GUI.VerticalScrollbar(position, value, size, topValue, bottomValue, style);
		}

		public static void BeginGroup(UnityEngine.Rect position)
		{
			GUI.BeginGroup(position);
		}

		public static void BeginGroup(UnityEngine.Rect position, System.String text)
		{
			GUI.BeginGroup(position, Trans(text));
		}

		public static void BeginGroup(UnityEngine.Rect position, UnityEngine.Texture image)
		{
			GUI.BeginGroup(position, image);
		}

		public static void BeginGroup(UnityEngine.Rect position, UnityEngine.GUIContent content)
		{
			GUI.BeginGroup(position, Trans(content));
		}

		public static void BeginGroup(UnityEngine.Rect position, UnityEngine.GUIStyle style)
		{
			GUI.BeginGroup(position, style);
		}

		public static void BeginGroup(UnityEngine.Rect position, System.String text, UnityEngine.GUIStyle style)
		{
			GUI.BeginGroup(position, Trans(text), style);
		}

		public static void BeginGroup(UnityEngine.Rect position, UnityEngine.Texture image, UnityEngine.GUIStyle style)
		{
			GUI.BeginGroup(position, image, style);
		}

		public static void BeginGroup(UnityEngine.Rect position, UnityEngine.GUIContent content, UnityEngine.GUIStyle style)
		{
			GUI.BeginGroup(position, Trans(content), style);
		}

		public static void EndGroup()
		{
			GUI.EndGroup();
		}

		public static UnityEngine.Vector2 BeginScrollView(UnityEngine.Rect position, UnityEngine.Vector2 scrollPosition, UnityEngine.Rect viewRect)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect);
		}

		public static UnityEngine.Vector2 BeginScrollView(UnityEngine.Rect position, UnityEngine.Vector2 scrollPosition, UnityEngine.Rect viewRect, System.Boolean alwaysShowHorizontal, System.Boolean alwaysShowVertical)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical);
		}

		public static UnityEngine.Vector2 BeginScrollView(UnityEngine.Rect position, UnityEngine.Vector2 scrollPosition, UnityEngine.Rect viewRect, UnityEngine.GUIStyle horizontalScrollbar, UnityEngine.GUIStyle verticalScrollbar)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, horizontalScrollbar, verticalScrollbar);
		}

		public static UnityEngine.Vector2 BeginScrollView(UnityEngine.Rect position, UnityEngine.Vector2 scrollPosition, UnityEngine.Rect viewRect, System.Boolean alwaysShowHorizontal, System.Boolean alwaysShowVertical, UnityEngine.GUIStyle horizontalScrollbar, UnityEngine.GUIStyle verticalScrollbar)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar);
		}

		public static void EndScrollView()
		{
			GUI.EndScrollView();
		}

		public static void EndScrollView(System.Boolean handleScrollWheel)
		{
			GUI.EndScrollView(handleScrollWheel);
		}

		public static void ScrollTo(UnityEngine.Rect position)
		{
			GUI.ScrollTo(position);
		}

		public static System.Boolean ScrollTowards(UnityEngine.Rect position, System.Single maxDelta)
		{
			return GUI.ScrollTowards(position, maxDelta);
		}

		public static UnityEngine.Rect Window(System.Int32 id, UnityEngine.Rect clientRect, UnityEngine.GUI.WindowFunction func, System.String text)
		{
			return GUI.Window(id, clientRect, func, Trans(text));
		}

		public static UnityEngine.Rect Window(System.Int32 id, UnityEngine.Rect clientRect, UnityEngine.GUI.WindowFunction func, UnityEngine.Texture image)
		{
			return GUI.Window(id, clientRect, func, image);
		}

		public static UnityEngine.Rect Window(System.Int32 id, UnityEngine.Rect clientRect, UnityEngine.GUI.WindowFunction func, UnityEngine.GUIContent content)
		{
			return GUI.Window(id, clientRect, func, Trans(content));
		}

		public static UnityEngine.Rect Window(System.Int32 id, UnityEngine.Rect clientRect, UnityEngine.GUI.WindowFunction func, System.String text, UnityEngine.GUIStyle style)
		{
			return GUI.Window(id, clientRect, func, Trans(text), style);
		}

		public static UnityEngine.Rect Window(System.Int32 id, UnityEngine.Rect clientRect, UnityEngine.GUI.WindowFunction func, UnityEngine.Texture image, UnityEngine.GUIStyle style)
		{
			return GUI.Window(id, clientRect, func, image, style);
		}

		public static UnityEngine.Rect Window(System.Int32 id, UnityEngine.Rect clientRect, UnityEngine.GUI.WindowFunction func, UnityEngine.GUIContent title, UnityEngine.GUIStyle style)
		{
			return GUI.Window(id, clientRect, func, Trans(title), style);
		}

		public static UnityEngine.Rect ModalWindow(System.Int32 id, UnityEngine.Rect clientRect, UnityEngine.GUI.WindowFunction func, System.String text)
		{
			return GUI.ModalWindow(id, clientRect, func, Trans(text));
		}

		public static UnityEngine.Rect ModalWindow(System.Int32 id, UnityEngine.Rect clientRect, UnityEngine.GUI.WindowFunction func, UnityEngine.Texture image)
		{
			return GUI.ModalWindow(id, clientRect, func, image);
		}

		public static UnityEngine.Rect ModalWindow(System.Int32 id, UnityEngine.Rect clientRect, UnityEngine.GUI.WindowFunction func, UnityEngine.GUIContent content)
		{
			return GUI.ModalWindow(id, clientRect, func, Trans(content));
		}

		public static UnityEngine.Rect ModalWindow(System.Int32 id, UnityEngine.Rect clientRect, UnityEngine.GUI.WindowFunction func, System.String text, UnityEngine.GUIStyle style)
		{
			return GUI.ModalWindow(id, clientRect, func, Trans(text), style);
		}

		public static UnityEngine.Rect ModalWindow(System.Int32 id, UnityEngine.Rect clientRect, UnityEngine.GUI.WindowFunction func, UnityEngine.Texture image, UnityEngine.GUIStyle style)
		{
			return GUI.ModalWindow(id, clientRect, func, image, style);
		}

		public static UnityEngine.Rect ModalWindow(System.Int32 id, UnityEngine.Rect clientRect, UnityEngine.GUI.WindowFunction func, UnityEngine.GUIContent content, UnityEngine.GUIStyle style)
		{
			return GUI.ModalWindow(id, clientRect, func, Trans(content), style);
		}

		public static void DragWindow(UnityEngine.Rect position)
		{
			GUI.DragWindow(position);
		}

		public static void DragWindow()
		{
			GUI.DragWindow();
		}

		public static void BringWindowToFront(System.Int32 windowID)
		{
			GUI.BringWindowToFront(windowID);
		}

		public static void BringWindowToBack(System.Int32 windowID)
		{
			GUI.BringWindowToBack(windowID);
		}

		public static void FocusWindow(System.Int32 windowID)
		{
			GUI.FocusWindow(windowID);
		}

		public static void UnfocusWindow()
		{
			GUI.UnfocusWindow();
		}

		public static UnityEngine.GUISkin skin
		{
            get { return GUI.skin; }
            set { GUI.skin = value; }
		}

		public static UnityEngine.Color color
		{
            get { return GUI.color; }
            set { GUI.color = value; }
		}

		public static UnityEngine.Color backgroundColor
		{
            get { return GUI.backgroundColor; }
            set { GUI.backgroundColor = value; }
		}

		public static UnityEngine.Color contentColor
		{
            get { return GUI.contentColor; }
            set { GUI.contentColor = value; }
		}

		public static System.Boolean changed
		{
            get { return GUI.changed; }
            set { GUI.changed = value; }
		}

		public static System.Boolean enabled
		{
			get { return GUI.enabled; }
            set { GUI.enabled = value; }
		}

		public static UnityEngine.Matrix4x4 matrix
		{
            get { return GUI.matrix; }
            set { GUI.matrix = value; }
		}

		public static System.String tooltip
		{
            get { return GUI.tooltip; }
            set { GUI.tooltip = Trans(value); }
		}

		public static System.Int32 depth
		{
            get { return GUI.depth; }
            set { GUI.depth = value; }
		}

		public static void Label(UnityEngine.Rect position, System.String text)
		{
			GUI.Label(position, Trans(text));
		}

		public static void Label(UnityEngine.Rect position, UnityEngine.Texture image)
		{
			GUI.Label(position, image);
		}

		public static void Label(UnityEngine.Rect position, UnityEngine.GUIContent content)
		{
			GUI.Label(position, Trans(content));
		}

		public static void Label(UnityEngine.Rect position, System.String text, UnityEngine.GUIStyle style)
		{
			GUI.Label(position, Trans(text), style);
		}

		public static void Label(UnityEngine.Rect position, UnityEngine.Texture image, UnityEngine.GUIStyle style)
		{
			GUI.Label(position, image, style);
		}

		public static void Label(UnityEngine.Rect position, UnityEngine.GUIContent content, UnityEngine.GUIStyle style)
		{
			GUI.Label(position, Trans(content), style);
		}

		public static void DrawTexture(UnityEngine.Rect position, UnityEngine.Texture image, UnityEngine.ScaleMode scaleMode, System.Boolean alphaBlend)
		{
			GUI.DrawTexture(position, image, scaleMode, alphaBlend);
		}

		public static void DrawTexture(UnityEngine.Rect position, UnityEngine.Texture image, UnityEngine.ScaleMode scaleMode)
		{
			GUI.DrawTexture(position, image, scaleMode);
		}

		public static void DrawTexture(UnityEngine.Rect position, UnityEngine.Texture image)
		{
			GUI.DrawTexture(position, image);
		}

		public static void DrawTexture(UnityEngine.Rect position, UnityEngine.Texture image, UnityEngine.ScaleMode scaleMode, System.Boolean alphaBlend, System.Single imageAspect)
		{
			GUI.DrawTexture(position, image, scaleMode, alphaBlend, imageAspect);
		}

		public static void DrawTextureWithTexCoords(UnityEngine.Rect position, UnityEngine.Texture image, UnityEngine.Rect texCoords)
		{
			GUI.DrawTextureWithTexCoords(position, image, texCoords);
		}

		public static void DrawTextureWithTexCoords(UnityEngine.Rect position, UnityEngine.Texture image, UnityEngine.Rect texCoords, System.Boolean alphaBlend)
		{
			GUI.DrawTextureWithTexCoords(position, image, texCoords, alphaBlend);
		}

		public static void Box(UnityEngine.Rect position, System.String text)
		{
			GUI.Box(position, Trans(text));
		}

		public static void Box(UnityEngine.Rect position, UnityEngine.Texture image)
		{
			GUI.Box(position, image);
		}

		public static void Box(UnityEngine.Rect position, UnityEngine.GUIContent content)
		{
			GUI.Box(position, Trans(content));
		}

		public static void Box(UnityEngine.Rect position, System.String text, UnityEngine.GUIStyle style)
		{
			GUI.Box(position, Trans(text), style);
		}

		public static void Box(UnityEngine.Rect position, UnityEngine.Texture image, UnityEngine.GUIStyle style)
		{
			GUI.Box(position, image, style);
		}

		public static void Box(UnityEngine.Rect position, UnityEngine.GUIContent content, UnityEngine.GUIStyle style)
		{
			GUI.Box(position, Trans(content), style);
		}

		public static System.Boolean Button(UnityEngine.Rect position, System.String text)
		{
			return GUI.Button(position, Trans(text));
		}

		public static System.Boolean Button(UnityEngine.Rect position, UnityEngine.Texture image)
		{
			return GUI.Button(position, image);
		}

		public static System.Boolean Button(UnityEngine.Rect position, UnityEngine.GUIContent content)
		{
			return GUI.Button(position, Trans(content));
		}

		public static System.Boolean Button(UnityEngine.Rect position, System.String text, UnityEngine.GUIStyle style)
		{
			return GUI.Button(position, Trans(text), style);
		}

		public static System.Boolean Button(UnityEngine.Rect position, UnityEngine.Texture image, UnityEngine.GUIStyle style)
		{
			return GUI.Button(position, image, style);
		}

		public static System.Boolean Button(UnityEngine.Rect position, UnityEngine.GUIContent content, UnityEngine.GUIStyle style)
		{
			return GUI.Button(position, Trans(content), style);
		}

		public static System.Boolean RepeatButton(UnityEngine.Rect position, System.String text)
		{
			return GUI.RepeatButton(position, Trans(text));
		}

		public static System.Boolean RepeatButton(UnityEngine.Rect position, UnityEngine.Texture image)
		{
			return GUI.RepeatButton(position, image);
		}

		public static System.Boolean RepeatButton(UnityEngine.Rect position, UnityEngine.GUIContent content)
		{
			return GUI.RepeatButton(position, Trans(content));
		}

		public static System.Boolean RepeatButton(UnityEngine.Rect position, System.String text, UnityEngine.GUIStyle style)
		{
			return GUI.RepeatButton(position, Trans(text), style);
		}

		public static System.Boolean RepeatButton(UnityEngine.Rect position, UnityEngine.Texture image, UnityEngine.GUIStyle style)
		{
			return GUI.RepeatButton(position, image, style);
		}

		public static System.Boolean RepeatButton(UnityEngine.Rect position, UnityEngine.GUIContent content, UnityEngine.GUIStyle style)
		{
			return GUI.RepeatButton(position, Trans(content), style);
		}

		public static System.String TextField(UnityEngine.Rect position, System.String text)
		{
			return GUI.TextField(position, Trans(text));
		}

		public static System.String TextField(UnityEngine.Rect position, System.String text, System.Int32 maxLength)
		{
			return GUI.TextField(position, Trans(text), maxLength);
		}

		public static System.String TextField(UnityEngine.Rect position, System.String text, UnityEngine.GUIStyle style)
		{
			return GUI.TextField(position, Trans(text), style);
		}

		public static System.String TextField(UnityEngine.Rect position, System.String text, System.Int32 maxLength, UnityEngine.GUIStyle style)
		{
			return GUI.TextField(position, Trans(text), maxLength, style);
		}

		public static System.String PasswordField(UnityEngine.Rect position, System.String password, System.Char maskChar)
		{
			return GUI.PasswordField(position, Trans(password), maskChar);
		}

		public static System.String PasswordField(UnityEngine.Rect position, System.String password, System.Char maskChar, System.Int32 maxLength)
		{
			return GUI.PasswordField(position, Trans(password), maskChar, maxLength);
		}

		public static System.String PasswordField(UnityEngine.Rect position, System.String password, System.Char maskChar, UnityEngine.GUIStyle style)
		{
			return GUI.PasswordField(position, Trans(password), maskChar, style);
		}

		public static System.String PasswordField(UnityEngine.Rect position, System.String password, System.Char maskChar, System.Int32 maxLength, UnityEngine.GUIStyle style)
		{
			return GUI.PasswordField(position, Trans(password), maskChar, maxLength, style);
		}

		public static System.String TextArea(UnityEngine.Rect position, System.String text)
		{
			return GUI.TextArea(position, Trans(text));
		}

		public static System.String TextArea(UnityEngine.Rect position, System.String text, System.Int32 maxLength)
		{
			return GUI.TextArea(position, Trans(text), maxLength);
		}

		public static System.String TextArea(UnityEngine.Rect position, System.String text, UnityEngine.GUIStyle style)
		{
			return GUI.TextArea(position, Trans(text), style);
		}

		public static System.String TextArea(UnityEngine.Rect position, System.String text, System.Int32 maxLength, UnityEngine.GUIStyle style)
		{
			return GUI.TextArea(position, Trans(text), maxLength, style);
		}

		public static void SetNextControlName(System.String name)
		{
			GUI.SetNextControlName(Trans(name));
		}

		public static System.String GetNameOfFocusedControl()
		{
			return GUI.GetNameOfFocusedControl();
		}

		public static void FocusControl(System.String name)
		{
			GUI.FocusControl(Trans(name));
		}

		public static System.Boolean Toggle(UnityEngine.Rect position, System.Boolean value, System.String text)
		{
			return GUI.Toggle(position, value, Trans(text));
		}

		public static System.Boolean Toggle(UnityEngine.Rect position, System.Boolean value, UnityEngine.Texture image)
		{
			return GUI.Toggle(position, value, image);
		}

		public static System.Boolean Toggle(UnityEngine.Rect position, System.Boolean value, UnityEngine.GUIContent content)
		{
			return GUI.Toggle(position, value, Trans(content));
		}

		public static System.Boolean Toggle(UnityEngine.Rect position, System.Boolean value, System.String text, UnityEngine.GUIStyle style)
		{
			return GUI.Toggle(position, value, Trans(text), style);
		}

		public static System.Boolean Toggle(UnityEngine.Rect position, System.Boolean value, UnityEngine.Texture image, UnityEngine.GUIStyle style)
		{
			return GUI.Toggle(position, value, image, style);
		}

		public static System.Boolean Toggle(UnityEngine.Rect position, System.Boolean value, UnityEngine.GUIContent content, UnityEngine.GUIStyle style)
		{
			return GUI.Toggle(position, value, Trans(content), style);
		}

		public static System.Boolean Toggle(UnityEngine.Rect position, System.Int32 id, System.Boolean value, UnityEngine.GUIContent content, UnityEngine.GUIStyle style)
		{
			return GUI.Toggle(position, id, value, Trans(content), style);
		}

		public static System.Int32 Toolbar(UnityEngine.Rect position, System.Int32 selected, System.String[] texts)
		{
			return GUI.Toolbar(position, selected, texts);
		}

		public static System.Int32 Toolbar(UnityEngine.Rect position, System.Int32 selected, UnityEngine.Texture[] images)
		{
			return GUI.Toolbar(position, selected, images);
		}

		public static System.Int32 Toolbar(UnityEngine.Rect position, System.Int32 selected, UnityEngine.GUIContent[] content)
		{
			return GUI.Toolbar(position, selected, content);
		}

		public static System.Int32 Toolbar(UnityEngine.Rect position, System.Int32 selected, System.String[] texts, UnityEngine.GUIStyle style)
		{
			return GUI.Toolbar(position, selected, texts, style);
		}

		public static System.Int32 Toolbar(UnityEngine.Rect position, System.Int32 selected, UnityEngine.Texture[] images, UnityEngine.GUIStyle style)
		{
			return GUI.Toolbar(position, selected, images, style);
		}

		public static System.Int32 Toolbar(UnityEngine.Rect position, System.Int32 selected, UnityEngine.GUIContent[] contents, UnityEngine.GUIStyle style)
		{
			return GUI.Toolbar(position, selected, contents, style);
		}

		public static System.Int32 SelectionGrid(UnityEngine.Rect position, System.Int32 selected, System.String[] texts, System.Int32 xCount)
		{
			return GUI.SelectionGrid(position, selected, texts, xCount);
		}

		public static System.Int32 SelectionGrid(UnityEngine.Rect position, System.Int32 selected, UnityEngine.Texture[] images, System.Int32 xCount)
		{
			return GUI.SelectionGrid(position, selected, images, xCount);
		}

		public static System.Int32 SelectionGrid(UnityEngine.Rect position, System.Int32 selected, UnityEngine.GUIContent[] content, System.Int32 xCount)
		{
			return GUI.SelectionGrid(position, selected, content, xCount);
		}

		public static System.Int32 SelectionGrid(UnityEngine.Rect position, System.Int32 selected, System.String[] texts, System.Int32 xCount, UnityEngine.GUIStyle style)
		{
			return GUI.SelectionGrid(position, selected, texts, xCount, style);
		}

		public static int SelectionGrid(UnityEngine.Rect position, System.Int32 selected, UnityEngine.Texture[] images, System.Int32 xCount, UnityEngine.GUIStyle style)
		{
			return GUI.SelectionGrid(position, selected, images, xCount, style);
		}

		public static System.Int32 SelectionGrid(UnityEngine.Rect position, System.Int32 selected, UnityEngine.GUIContent[] contents, System.Int32 xCount, UnityEngine.GUIStyle style)
		{
			return GUI.SelectionGrid(position, selected, contents, xCount, style);
		}

        // Translation Utility
        private static string Trans(string text)
        {
            // Log Content
            log.WriteLine("[GUIText] " + text);
            log.Flush();

            // Translate Content
            if (Storage.TUI.texts.Count > 0)
            {
                text = (!Storage.TUI.texts.ContainsKey(xText.Escape(text))) ?
                        text :
                        xText.Unescape(Storage.Text.texts[xText.Escape(text)]);
            }

            // Return Content
            return text;
        }

        private static GUIContent Trans(GUIContent content)
        {
            // Log Content
            log.WriteLine("[GUIContent] " + content.text + " (Tooltip: " + content.tooltip + ")");
            log.Flush();

            // Translate Content
            if (Storage.TUI.texts.Count > 0)
            {
                content.text = (!Storage.TUI.texts.ContainsKey(xText.Escape(content.text))) ? 
                    content.text :
                    xText.Unescape(Storage.Text.texts[xText.Escape(content.text)]);
                content.tooltip = (!Storage.TUI.texts.ContainsKey(xText.Escape(content.tooltip))) ?
                    content.tooltip :
                    xText.Unescape(Storage.Text.texts[xText.Escape(content.tooltip)]);
            }

            // Return Content
            return content;
        }
    }
}
