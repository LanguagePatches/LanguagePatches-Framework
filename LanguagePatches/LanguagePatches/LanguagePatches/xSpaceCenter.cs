/**'
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

using UnityEngine;

namespace LanguagePatches
{
    [KSPAddon(KSPAddon.Startup.SpaceCentre, false)]
    public class xSpaceCenter : MonoBehaviour
    {
        ScreenSafeGUIText SSGT;

        private void Start()
        {
            SSGT = UnityEngine.Object.FindObjectOfType<ScreenSafeGUIText>();

            if (Storage.SpaceCenter.replaceFont)
            {
                xFont.ReplaceFont(SSGT, Storage.SpaceCenter.size);
            }
        }

        private void Update()
        {
            if (Storage.Load && Storage.SpaceCenter.ssuiText.Count != 0 && this.SSGT.text != "" && this.SSGT.text != null)
            {
                SSGT.text = (Storage.SpaceCenter.ssuiText.ContainsKey(SSGT.text)) ? Storage.SpaceCenter.ssuiText[SSGT.text] : SSGT.text;
            }
        }
    }
}
