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

using System;
using System.Xml;
using System.IO;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace LanguagePatches
{
    [KSPAddon(KSPAddon.Startup.SpaceCentre, false)]
    public class xSpaceCenter : MonoBehaviour
    {
        private string vab, ac, sph, flg, rnd, ts, lpd, rwy, ab, mc;
        private ScreenSafeGUIText SSGT;
        private void Start()
        {
            this.SSGT = UnityEngine.Object.FindObjectOfType<ScreenSafeGUIText>();
            if (GameDatabase.Instance.GetConfigNodes(Loader.langPrefix + "SpaceCenter").Count() == 1)
            {
                ConfigNode spaceCenterNode = GameDatabase.Instance.GetConfigNodes(Loader.langPrefix + "SpaceCenter")[0];
                vab = spaceCenterNode.GetValue("VAB");
                sph = spaceCenterNode.GetValue("SPH");
                ac = spaceCenterNode.GetValue("astronautComplex");
                flg = spaceCenterNode.GetValue("flagPole");
                rnd = spaceCenterNode.GetValue("RnD");
                ts = spaceCenterNode.GetValue("trackingStation");
                lpd = spaceCenterNode.GetValue("launchPad");
                rwy = spaceCenterNode.GetValue("runway");
                ab = spaceCenterNode.GetValue("administrationBuilding");
                mc = spaceCenterNode.GetValue("missionControl");
            }
            else
            {
                // If we have multiple nodes, kill us
                Destroy(this);
            }
        }

        private void Update()
        {
            if (Loader.loadCache && (this.SSGT.text != null || this.SSGT.text != ""))
            {
                switch (this.SSGT.text)
                {
                    case "Vehicle Assembly Building":
                        this.SSGT.text = vab;
                        break;
                    case "Spaceplane Hangar":
                        this.SSGT.text = sph;
                        break;
                    case "Astronaut Complex":
                        this.SSGT.text = ac;
                        break;
                    case "Flag Pole":
                        this.SSGT.text = flg;
                        break;
                    case "Research and Development":
                        this.SSGT.text = rnd;
                        break;
                    case "Tracking Station":
                        this.SSGT.text = ts;
                        break;
                    case "Launch Pad":
                        this.SSGT.text = lpd;
                        break;
                    case "Runway":
                        this.SSGT.text = rwy;
                        break;
                    case "Administration Building":
                        this.SSGT.text = ab;
                        break;
                    case "Mission Control":
                        this.SSGT.text = mc;
                        break;
                }
            }
        }
    }
}
