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
using System.IO;
using System.Xml;
using UnityEngine;

namespace LanguagePatches
{
    [KSPAddon(KSPAddon.Startup.MainMenu, false)]
    internal class xMenu : MonoBehaviour
    {
        private static bool fontIfy = true;
        private bool isOver = false;

        // Helper class
        public static void TM(TextButton3D tb3D, string text, float size = -1)
        {
            TextMesh component = tb3D.transform.GetComponent<TextMesh>();
            component.text = text;
            if (fontIfy)
                xFont.FontIfy(component, size);
        }
        // Update the MainMenu Buttons
        private void Update()
        {
            if (Loader.loadCache)
            {
                // Does our config exists?
                if (!isOver && File.Exists(Loader.path + "Menu.xml"))
                {
                    GameObject gameObject = GameObject.Find("MainMenu");
                    if (!(gameObject == null))
                    {
                        MainMenu component = gameObject.GetComponent<MainMenu>();
                        foreach (TextMesh mesh in GameObject.FindObjectsOfType<TextMesh>())
                        {
                            xFont.FontIfy(mesh);
                        }
                        List<string> menu = new List<string>();
                        XmlDocument xmlDocument = new XmlDocument();
                        xmlDocument.Load(Loader.path + "Menu.xml");

                        if (xmlDocument.DocumentElement.HasAttribute("replaceFont"))
                        {
                            fontIfy = bool.Parse(xmlDocument.DocumentElement.GetAttribute("replaceFont"));
                        }

                        foreach (XmlElement child in xmlDocument.DocumentElement.ChildNodes)
                        {
                            // Patch the Buttons
                            switch (child.GetAttribute("name"))
                            {
                                case "backBtn":
                                    xMenu.TM(component.backBtn, child.InnerText, GetSize(child));
                                    break;
                                case "startBtn":
                                    xMenu.TM(component.startBtn, child.InnerText, GetSize(child));
                                    gameObject.transform.FindChild("stage 2").FindChild("Header").GetComponent<TextMesh>().text = child.InnerText;
                                    break;
                                case "settingBtn":
                                    xMenu.TM(component.settingBtn, child.InnerText, GetSize(child));
                                    break;
                                case "spaceportBtn":
                                    xMenu.TM(component.spaceportBtn, child.InnerText, GetSize(child));
                                    break;
                                case "commBtn":
                                    xMenu.TM(component.commBtn, child.InnerText, GetSize(child));
                                    break;
                                case "continueBtn":
                                    xMenu.TM(component.continueBtn, child.InnerText, GetSize(child));
                                    break;
                                case "creditsBtn":
                                    xMenu.TM(component.creditsBtn, child.InnerText, GetSize(child));
                                    break;
                                case "newGameBtn":
                                    xMenu.TM(component.newGameBtn, child.InnerText, GetSize(child));
                                    break;
                                case "quitBtn":
                                    xMenu.TM(component.quitBtn, child.InnerText, GetSize(child));
                                    break;
                                case "scenariosBtn":
                                    xMenu.TM(component.scenariosBtn, child.InnerText, GetSize(child));
                                    break;
                                case "trainingBtn":
                                    xMenu.TM(component.trainingBtn, child.InnerText, GetSize(child));
                                    break;
                                case "KSPsiteURL":
                                    component.KSPsiteURL = child.InnerText;
                                    break;
                                case "SpaceportURL":
                                    component.SpaceportURL = child.InnerText;
                                    break;
                                default:
                                    break;
                            }
                        }

                        // rien ne va plus
                        isOver = true;
                    }
                }
            }
        }

        private float GetSize(XmlElement element)
        {
            if (element.HasAttribute("size"))
            {
                return float.Parse(element.GetAttribute("size"));
            }
            else
            {
                return -1;
            }
        }
    }
}
