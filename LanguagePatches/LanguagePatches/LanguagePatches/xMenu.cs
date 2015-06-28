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
using UnityEngine;

namespace LanguagePatches
{
    [KSPAddon(KSPAddon.Startup.MainMenu, false)]
    public class xMenu : MonoBehaviour
    {
        // Update the MainMenu Buttons
        private void Update()
        {
            GameObject gameObject = GameObject.Find("MainMenu");
            if (Storage.Load && Storage.Menu.buttons.Count != 0 && gameObject != null)
            {
                MainMenu component = gameObject.GetComponent<MainMenu>();

                foreach (TextMesh tm in component.GetComponentsInChildren<TextMesh>(true))
                {
                    if (Storage.Menu.buttons.ContainsKey(tm.name))
                    {
                        KeyValuePair<float, string> newTM = Storage.Menu.buttons[tm.name];
                        tm.text = newTM.Value;
                        if (Storage.Menu.replaceFont)
                            xFont.ReplaceFont(tm, newTM.Key);

                    }
                }

                component.SpaceportURL = (Storage.Menu.SpacePortURL != "") ? Storage.Menu.SpacePortURL : component.SpaceportURL;
                component.KSPsiteURL = (Storage.Menu.CommunityURL != "") ? Storage.Menu.CommunityURL : component.KSPsiteURL;

                // rien ne va plus
                Destroy(this);
            }
        }
    }
}
