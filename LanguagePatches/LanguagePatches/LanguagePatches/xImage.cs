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
using System.Linq;
using System.Xml;
using UnityEngine;

namespace LanguagePatches
{
    [KSPAddon(KSPAddon.Startup.EveryScene, false)]
    public class xImage : MonoBehaviour
    {
        private static Dictionary<string, int> config;

        private static List<int> LoadedLevels;

        private static Dictionary<string, string> ImageFiles;

        private bool IsOver = false;

        public static void LoadConfig()
        {
            // Does our config exists?
            if (File.Exists(Loader.path + "Image.xml"))
            {
                XmlDocument xmlDocument = new XmlDocument();
                xImage.config = new Dictionary<string, int>();
                xmlDocument.Load(Loader.path + "Image.xml");
                foreach (XmlElement childNode in xmlDocument.DocumentElement.ChildNodes)
                {
                    // Load image paths
                    xImage.config[childNode.GetAttribute("id")] = int.Parse(childNode.InnerText);
                }
            }
        }

        private void Start()
        {
            if (Loader.loadCache)
            {
                // Load the actual images
                if (xImage.ImageFiles == null)
                {
                    xImage.ImageFiles = new Dictionary<string, string>();
                    foreach (string list in Directory.GetFiles(Loader.images).ToList<string>())
                    {
                        xImage.ImageFiles.Add(Path.GetFileNameWithoutExtension(list), list);
                    }
                    if (xImage.config == null)
                    {
                        xImage.LoadConfig();
                    }
                    xImage.LoadedLevels = new List<int>();
                }
            }
        }

        private void Update()
        {
            if (Loader.loadCache)
            {
            // Apply the new Textures
                if (!this.IsOver)
                {
                    if (!xImage.LoadedLevels.Contains(Application.loadedLevel))
                    {
                        xImage.LoadedLevels.Add(Application.loadedLevel);
                        Material[] materialArray = Resources.FindObjectsOfTypeAll<Material>();
                        for (int i = 0; i < (int)materialArray.Length; i++)
                        {
                            Material material = materialArray[i];
                            Texture texture = material.GetTexture("_MainTex");
                            if (!(texture == null))
                            {
                                if (xImage.ImageFiles.ContainsKey(texture.name))
                                {
                                    Texture2D texture2D = new Texture2D(xImage.config[texture.name], xImage.config[texture.name], TextureFormat.ARGB32, false);
                                    texture2D.LoadImage(File.ReadAllBytes(xImage.ImageFiles[texture.name]));
                                    material.SetTexture("_MainTex", texture2D);
                                    xImage.ImageFiles.Remove(texture.name);
                                    GameObject gameObject = new GameObject(string.Concat("DDOL_", texture.name));
                                    gameObject.AddComponent<MeshRenderer>().material = material;
                                    UnityEngine.Object.DontDestroyOnLoad(gameObject);
                                }
                            }
                        }
                        this.IsOver = true;
                    }
                }
            }
        }
    }
}
