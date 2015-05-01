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
        private string vab = "Vehicle Assembly Building", sph = "Space Plane Hangar", ac = "Astronaut Complex", ts = "Tracking Station", ab = "Administration Building", rwy = "Runway" , mc = "Mission Control", flg = "Flag Pole", rnd = "Research and Development", lnchpd = "Launch Pad";

        private void Start()
        {
            this.SSGT = UnityEngine.Object.FindObjectOfType<ScreenSafeGUIText>();
        }

        private void Update()
        {
            if (Loader.loadCache && File.Exists(Loader.path + "SpaceCenter.xml"))
            {            
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(Loader.path + "SpaceCenter.xml");
                bool fontIfy = true;
                if (xmlDocument.DocumentElement.HasAttribute("replaceFont") && !bool.Parse(xmlDocument.DocumentElement.GetAttribute("replaceFont")))
                {
                    fontIfy = false;
                }

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
                        case "AstronautComplex":
                            ac = child.InnerText;
                            break;
                        case "TrackingStation":
                            ts = child.InnerText;
                            break;
                        case "AdministrationBuilding":
                            ab = child.InnerText;
                            break;
                        case "Runway":
                            rwy = child.InnerText;
                            break;
                        case "FlagPole":
                            flg = child.InnerText;
                            break;
                        case "MissionControl":
                            mc = child.InnerText;
                            break;
                        case "RD":
                            rnd = child.InnerText;
                            break;
                        case "LaunchPad":
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

                if (fontIfy)
                {
                    if (xmlDocument.DocumentElement.HasAttribute("size"))
                    {
                        xFont.FontIfy(SSGT, float.Parse(xmlDocument.DocumentElement.GetAttribute("size")));
                    }
                    else
                    {
                        xFont.FontIfy(SSGT);
                    }
                }
            }
        }
    }
}
