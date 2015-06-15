using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;
using System.Xml;

namespace LanguagePatches
{
    [KSPAddon(KSPAddon.Startup.Credits, false)]
    public class Credits : MonoBehaviour
    {
        void Start()
        {
            if (File.Exists(Loader.path + "Credits.xml") && Loader.loadCache)
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(Loader.path + "Credits.xml");
                TextMesh[] textMeshes = GameObject.FindObjectsOfType<TextMesh>();
                foreach (TextMesh mesh in textMeshes)
                {
                    if (mesh.text == "Artists")
                    {
                        mesh.text = "";
                    }
                    else
                    {
                        foreach (XmlElement child in xmlDocument.DocumentElement.ChildNodes)
                        {
                            switch (child.GetAttribute("name"))
                            {
                                case "legacy":
                                    if (mesh.text == "Legacy")
                                        mesh.text = child.InnerText;
                                    break;
                                case "GameToolsDeveloper":
                                    if (mesh.text == "Game & Tools Developer")
                                        mesh.text = child.InnerText;
                                    break;
                                case "createdBy":
                                    if (mesh.text == "Created By")
                                        mesh.text = child.InnerText;
                                    break;
                                case "contentDesign":
                                    if (mesh.text == "Content Design")
                                        mesh.text = child.InnerText;
                                    break;
                                case "leadGameDeveloper":
                                    if (mesh.text == "Lead Game Developer")
                                        mesh.text = child.InnerText;
                                    break;
                                case "UIdeveloper":
                                    if (mesh.text == "UI Developer")
                                        mesh.text = child.InnerText;
                                    break;
                                case "aka":
                                    if (mesh.text == "(aka HarvesteR)")
                                        mesh.text = "(" + child.InnerText + " HarvesterR)";
                                    else if (mesh.text == "Miguel Piña (aka MaxMaps)  ")
                                        mesh.text = "Miguel Piña (" + child.InnerText + " MaxMaps)  ";
                                    else if (mesh.text == "Christoph Thürsam (aka PorkJet)")
                                        mesh.text = "Christoph Thürsam (" + child.InnerText + " PorkJet)";
                                    else if (mesh.text == "Brian Provan (aka Arsonide)")
                                        mesh.text = "Brian Provan (" + child.InnerText + " Arsonide)";
                                    else if (mesh.text == "Frank Pierce (aka Frizzank)")
                                        mesh.text = "Frank Pierce (" + child.InnerText + " Frizzank)";
                                    else if (mesh.text == "Bob Palmer (aka RoverDude)")
                                        mesh.text = "Bob Palmer (" + child.InnerText + " RoverDude)";
                                    break;
                                case "executiveProducers":
                                    if (mesh.text == "Executive Producers")
                                        mesh.text = child.InnerText;
                                    break;
                                case "programming":
                                    if (mesh.text == "Programming")
                                        mesh.text = child.InnerText;
                                    break;
                                case "otherTracks":
                                    if (mesh.text == "Other Tracks")
                                        mesh.text = child.InnerText;
                                    break;
                                case "soundDesigner":
                                    if (mesh.text == "Sound Designer")
                                        mesh.text = child.InnerText;
                                    break;
                                case "producer":
                                    if (mesh.text == "Producer")
                                        mesh.text = child.InnerText;
                                    break;
                                case "3dArtists":
                                    if (mesh.text == "3d")
                                        mesh.text = child.InnerText;
                                    break;
                                case "communityContributors":
                                    if (mesh.text == "Community Contributors")
                                        mesh.text = child.InnerText;
                                    break;
                                case "gameAndServerDeveloper":
                                    if (mesh.text == "Game & Server Developer")
                                        mesh.text = child.InnerText;
                                    break;
                                case "specialThanksTo":
                                    if (mesh.text == "Special Thanks to")
                                        mesh.text = child.InnerText;
                                    break;
                                case "loadingScreenArtworkBy":
                                    if (mesh.text == "Loading Screen Artwork by")
                                        mesh.text = child.InnerText;
                                    break;
                                case "QAdirectorLead":
                                    if (mesh.text == "QA Director / Lead")
                                        mesh.text = child.InnerText;
                                    break;
                                case "theAwesomeQAteam":
                                    if (mesh.text == "The Awesome QA Team")
                                        mesh.text = child.InnerXml;
                                    break;
                                case "KSPmainTheme":
                                    if (mesh.text == "KSP Main Theme")
                                        mesh.text = child.InnerText;
                                    break;
                                case "originalMusic":
                                    if (mesh.text == "Original Music")
                                        mesh.text = child.InnerText;
                                    break;
                                case "writtenBy":
                                    if (mesh.text == "Written by Kevin MacLeod")
                                        mesh.text = child.InnerText + " Kevin MacLeod";
                                    break;
                                case "communityLead":
                                    if (mesh.text == "Community Lead")
                                        mesh.text = child.InnerText;
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}
