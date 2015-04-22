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
using System.Collections.Generic;

namespace LanguagePatches
{
    [KSPAddon(KSPAddon.Startup.SpaceCentre, false)]
    public class xSpaceCenter : MonoBehaviour
    {
        private ScreenSafeGUIText SSGT;
        private string vab = "Vehicle Assembly Building", sph = "Spaceplane Hangar", ac = "Astronaut Complex", ts = "Tracking Station", ab = "Assembly Building", rwy = "Runway", mc = "Mission Control", flg = "Flag Pole", rnd = "Research and Development", lnchpd = "Launch Pad";

        private void Start()
        {
            if (Loader.loadCache == "active")
            {
                this.SSGT = UnityEngine.Object.FindObjectOfType<ScreenSafeGUIText>();
            }
        }

        private void Update()
        {
            if (File.Exists(Loader.path + "SpaceCenter.xml") && Loader.loadCache == "active")
            {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.Load(Loader.path + "SpaceCenter.xml");
                    foreach (XmlElement child in xmlDocument.DocumentElement.ChildNodes)
                    {
                        switch (child.GetAttribute("building"))
                        {
                            case "VAB":
                                vab = child.InnerText;
                                break;
                            case "SPH":
                                sph = child.InnerText;
                                break;
                            case "Astronaut Complex":
                                ac = child.InnerText;
                                break;
                            case "Tracking Station":
                                ts = child.InnerText;
                                break;
                            case "Administration Building":
                                ab = child.InnerText;
                                break;
                            case "Runway":
                                rwy = child.InnerText;
                                break;
                            case "Flag Pole":
                                flg = child.InnerText;
                                break;
                            case "Mission Control":
                                mc = child.InnerText;
                                break;
                            case "R&D":
                                rnd = child.InnerText;
                                break;
                            case "Launch Pad":
                                lnchpd = child.InnerText;
                                break;
                        }
                    }


                    if ((this.SSGT.text != "") || (this.SSGT.text != null))
                    {
                        switch (this.SSGT.text)
                        {
                            case "Vehicle Assembly Building":
                                this.SSGT.text = vab;
                                break;

                            case "Astronaut Complex":
                                this.SSGT.text = ac;
                                break;

                            case "Spaceplane Hangar":
                                this.SSGT.text = sph;
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
                                this.SSGT.text = lnchpd;
                                break;

                            case "Runway":
                                this.SSGT.text = rwy;
                                break;

                            case "Mission Control":
                                this.SSGT.text = mc;
                                break;

                            case "Administration Building":
                                this.SSGT.text = ab;
                                break;

                        }
                    }
            }
        }
    }
}
