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
    [KSPAddon(KSPAddon.Startup.Credits, false)]
    public class Credits : MonoBehaviour
    {
        private void Start()
        {
            if (Storage.Credits.texts.Count > 0 && Storage.Load)
            {
                TextMesh[] textMeshes = GameObject.FindObjectsOfType<TextMesh>();
                foreach (TextMesh mesh in textMeshes)
                {
                    if (mesh.text == "Artists")
                    {
                        mesh.text = "";
                    }
                    else
                    {
                        foreach (KeyValuePair<string, string> text in Storage.Credits.texts)
                        {
                            switch (text.Key)
                            {
                                case "legacy":
                                    if (mesh.text == "Legacy")
                                        mesh.text = text.Value;
                                    break;

                                case "GameToolsDeveloper":
                                    if (mesh.text == "Game & Tools Developer")
                                        mesh.text = text.Value;
                                    break;

                                case "createdBy":
                                    if (mesh.text == "Created By")
                                        mesh.text = text.Value;
                                    break;

                                case "contentDesign":
                                    if (mesh.text == "Content Design")
                                        mesh.text = text.Value;
                                    break;

                                case "leadGameDeveloper":
                                    if (mesh.text == "Lead Game Developer")
                                        mesh.text = text.Value;
                                    break;

                                case "UIdeveloper":
                                    if (mesh.text == "UI Developer")
                                        mesh.text = text.Value;
                                    break;

                                case "aka":
                                    if (mesh.text == "(aka HarvesteR)")
                                        mesh.text = "(" + text.Value + " HarvesterR)";
                                    else if (mesh.text == "Miguel Piña (aka MaxMaps)  ")
                                        mesh.text = "Miguel Piña (" + text.Value + " MaxMaps)  ";
                                    else if (mesh.text == "Christoph Thürsam (aka PorkJet)")
                                        mesh.text = "Christoph Thürsam (" + text.Value + " PorkJet)";
                                    else if (mesh.text == "Brian Provan (aka Arsonide)")
                                        mesh.text = "Brian Provan (" + text.Value + " Arsonide)";
                                    else if (mesh.text == "Frank Pierce (aka Frizzank)")
                                        mesh.text = "Frank Pierce (" + text.Value + " Frizzank)";
                                    else if (mesh.text == "Bob Palmer (aka RoverDude)")
                                        mesh.text = "Bob Palmer (" + text.Value + " RoverDude)";
                                    break;

                                case "executiveProducers":
                                    if (mesh.text == "Executive Producers")
                                        mesh.text = text.Value;
                                    break;

                                case "programming":
                                    if (mesh.text == "Programming")
                                        mesh.text = text.Value;
                                    break;

                                case "otherTracks":
                                    if (mesh.text == "Other Tracks")
                                        mesh.text = text.Value;
                                    break;

                                case "soundDesigner":
                                    if (mesh.text == "Sound Designer")
                                        mesh.text = text.Value;
                                    break;

                                case "producer":
                                    if (mesh.text == "Producer")
                                        mesh.text = text.Value;
                                    break;

                                case "3dArtists":
                                    if (mesh.text == "3d")
                                        mesh.text = text.Value;
                                    break;

                                case "communityContributors":
                                    if (mesh.text == "Community Contributors")
                                        mesh.text = text.Value;
                                    break;

                                case "gameAndServerDeveloper":
                                    if (mesh.text == "Game & Server Developer")
                                        mesh.text = text.Value;
                                    break;

                                case "specialThanksTo":
                                    if (mesh.text == "Special Thanks to")
                                        mesh.text = text.Value;
                                    break;

                                case "loadingScreenArtworkBy":
                                    if (mesh.text == "Loading Screen Artwork by")
                                        mesh.text = text.Value;
                                    break;

                                case "QAdirectorLead":
                                    if (mesh.text == "QA Director / Lead")
                                        mesh.text = text.Value;
                                    break;

                                case "theAwesomeQAteam":
                                    if (mesh.text == "The Awesome QA Team")
                                        mesh.text = text.Value;
                                    break;

                                case "KSPmainTheme":
                                    if (mesh.text == "KSP Main Theme")
                                        mesh.text = text.Value;
                                    break;

                                case "originalMusic":
                                    if (mesh.text == "Original Music")
                                        mesh.text = text.Value;
                                    break;

                                case "writtenBy":
                                    if (mesh.text == "Written by Kevin MacLeod")
                                        mesh.text = text.Value + " Kevin MacLeod";
                                    break;

                                case "communityLead":
                                    if (mesh.text == "Community Lead")
                                        mesh.text = text.Value;
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}