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
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace LanguagePatches
{
    [KSPAddon(KSPAddon.Startup.Instantly, true)]
    public class xLoading : MonoBehaviour
    {
        public void Awake()
        {
            if (Storage.Load)
            {
                // Hijack the Game's prefab and replace their LoadingTips
                FieldInfo field = LoadingScreen.Instance.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic).Where(f => f.FieldType == typeof(List<LoadingScreen.LoadingScreenState>)).First();
                List<LoadingScreen.LoadingScreenState> states = field.GetValue(LoadingScreen.Instance) as List<LoadingScreen.LoadingScreenState>;

                // Loop through all ScreenStates
                foreach (LoadingScreen.LoadingScreenState state in states)
                {
                    state.tips = Storage.Loading.tips.ToArray();
                }

                // Overwrite the List with the new one
                field.SetValue(LoadingScreen.Instance, states);

                TextMesh text = TextMesh.FindObjectOfType<TextMesh>();
                if (Storage.Loading.replaceFont)
                    xFont.ReplaceFont(text, Storage.Loading.size);
            }
            Destroy(this); // Don't hang around..
        }
    }
}