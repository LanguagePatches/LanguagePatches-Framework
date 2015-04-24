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
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using System.IO;
using UnityEngine;


namespace LanguagePatches
{
    [KSPAddon(KSPAddon.Startup.EveryScene, false)]
    public class xItem : MonoBehaviour
    {
        private GameObject _UI;
        private List<zItem> NzItems;
        private static List<zItem> zItems;

        public static void Load()
        {
            if (Loader.loadCache == "active" && File.Exists(Loader.path + "Item.xml"))
            { 
                XmlDocument document = new XmlDocument();
                document.Load(Loader.path + "Item.xml");
                zItems = new List<zItem>();
                foreach (XmlNode node in document.ChildNodes)
                {
                    if (node.Name == "Items")
                    {
                        foreach (XmlNode node2 in node.ChildNodes)
                        {
                            if (node2 is XmlElement)
                            {
                                XmlElement element = node2 as XmlElement;
                                zItem item = new zItem
                                {
                                    Scene = ToGameScenes(element.GetAttribute("scene")),
                                    Path = element.GetAttribute("path"),
                                    Type = element.GetAttribute("type"),
                                    zDict = new Dictionary<string, string>()
                                };
                                foreach (XmlNode node3 in node2.ChildNodes)
                                {
                                    if (node3 is XmlElement)
                                    {
                                        item.zDict[((XmlElement)node3).GetAttribute("name")] = node3.InnerText;
                                    }
                                }
                                zItems.Add(item);
                            }
                        }
                    }
                }
            }
            Debug.Log("xItem Loaded:" + zItems.Count.ToString());
        }

        private void OnGUI()
        {
        }

        private void Start()
        {
            if (Loader.loadCache == "active" && File.Exists(Loader.path + "Item.xml"))
            { 
                if (zItems == null)
                {
                    Load();
                }
                this._UI = GameObject.Find("_UI");
                if (this._UI != null)
                {
                    Debug.Log("UI..................................................");
                }
                if (zItems != null)
                {
                    this.NzItems = new List<zItem>();
                    foreach (zItem item in zItems)
                    {
                        if (item.Scene == HighLogic.LoadedScene)
                        {
                            this.NzItems.Add(item);
                        }
                    }
                }
            }
        }

        public static GameScenes ToGameScenes(string scene)
        {
            switch (scene.ToUpper())
            {
                case "LOADING":
                    return GameScenes.LOADING;

                case "LOADINGBUFFER":
                    return GameScenes.LOADINGBUFFER;

                case "MAINMENU":
                    return GameScenes.MAINMENU;

                case "SETTINGS":
                    return GameScenes.SETTINGS;

                case "CREDITS":
                    return GameScenes.CREDITS;

                case "SPACECENTER":
                    return GameScenes.SPACECENTER;

                case "EDITOR":
                    return GameScenes.EDITOR;

                case "FLIGHT":
                    return GameScenes.FLIGHT;

                case "TRACKSTATION":
                    return GameScenes.TRACKSTATION;

                case "SPH":
                    return GameScenes.PSYSTEM;

                case "PSYSTEM":
                    return (GameScenes.TRACKSTATION | GameScenes.MAINMENU);
            }
            return GameScenes.LOADING;
        }

        private void Update()
        {
            if (Loader.loadCache == "active" && File.Exists(Loader.path + "Item.xml"))
            {
                if (this._UI != null)
                {
                    foreach (zItem item in this.NzItems)
                    {
                        SpriteTextRich rich;
                        if (item.NowObject == null)
                        {
                            item.NowObject = this._UI.transform.Find(item.Path);
                        }
                        if (item.NowObject != null)
                        {
                            string type = item.Type;
                            if (type != null)
                            {
                                if (!(type == "SpriteText"))
                                {
                                    if (type == "SpriteTextRich")
                                    {
                                        goto Label_0106;
                                    }
                                }
                                else
                                {
                                    SpriteText component = item.NowObject.gameObject.GetComponent<SpriteText>();
                                    if ((component != null) && item.zDict.ContainsKey(component.Text))
                                    {
                                        component.Text = item.zDict[component.Text];
                                    }
                                }
                            }
                        }
                        continue;
                    Label_0106:
                        rich = item.NowObject.gameObject.GetComponent<SpriteTextRich>();
                        if ((rich != null) && item.zDict.ContainsKey(rich.Text))
                        {
                            rich.Text = item.zDict[rich.Text];
                        }
                    }
                }
            }
        }

        public class zItem
        {
            public Transform NowObject { get; set; }

            public string Path { get; set; }

            public GameScenes Scene { get; set; }

            public string Type { get; set; }

            public Dictionary<string, string> zDict { get; set; }
        }
    }
}

