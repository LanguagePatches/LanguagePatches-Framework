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

using Mono.Cecil;
using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using UnityEngine;
//using UnityEngine;

namespace LanguagePatches
{
    public class Storage
    {
        // Storage-class for xFont.cs
        public class Font
        {
            public static Dictionary<string, UnityEngine.Font> trueTypeFonts = new Dictionary<string, UnityEngine.Font>();
            public static Dictionary<string, KeyValuePair<TextAsset, Material>> spriteFont = new Dictionary<string, KeyValuePair<TextAsset, Material>>();
            public static AssetBundle asset = null;
        }

        // Storage-class for xBody.cs
        public class Body
        {
            public static Dictionary<string, string> bodyDescription = new Dictionary<string, string>();
        }

        // Storage-class for xImage.cs
        public class Image
        {
            public static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
            public static List<int> levels = new List<int>();
        }

        // Storage-class for xLoading.cs
        public class Loading
        {
            public static int size = -1;
            public static bool replaceFont = true;
            public static List<string> tips = new List<string>();
            public static string loadingString = "Loading...";
        }

        // Storage-class for xMenu.cs
        public class Menu
        {
            public static bool replaceFont = true;
            public static string SpacePortURL = "";
            public static string CommunityURL = "";
            public static Dictionary<string, KeyValuePair<float, string>> buttons = new Dictionary<string, KeyValuePair<float, string>>();
        }

        // Storage-class for xSpaceCenter.cs
        public class SpaceCenter
        {
            public static bool replaceFont = true;
            public static float size = -1;
            public static Dictionary<string, string> ssuiText = new Dictionary<string, string>();
        }

        // Storage-class for xText.cs
        public class Text
        {
            public static Dictionary<string, string> texts = new Dictionary<string, string>();
            public static bool replaceFont = true;
        }

        // Storage-class for xCredits.cs
        public class Credits
        {
            public static Dictionary<string, string> texts = new Dictionary<string, string>();
        }

        // Storage-class for TUI.cs and TUILayout.cs
        public class TUI
        {
            public static Dictionary<string, string> texts = new Dictionary<string, string>();
        }

        // Path of the Plugin
        public static string directory
        {
            get
            {
                return System.IO.Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
            }
        }

        // Path of the Cache-File
        public static string cachePath = "";

        // Cache-State of the Plugin
        public static bool Load
        {
            get
            {
                // Read the cache-file and parse it's content to a Boolean
                bool status = false;
                StreamReader cacheReader = new StreamReader(cachePath);
                string line = cacheReader.ReadLine();
                cacheReader.Close();
                status = Boolean.Parse(line);
                return status;
            }
            set
            {
                // Write the new Status to the File
                if (File.Exists(Storage.cachePath))
                {
                    File.Delete(Storage.cachePath);
                }

                TextWriter cache = new StreamWriter(Storage.cachePath);
                cache.Write(value);
                cache.Close();
            }
        }

        // Load the .xml Configuration and parse it into the static Dictionaries
        public static void LoadConfiguration()
        {
            if (File.Exists(Loader.path + "Body.xml"))
            {
                // Body
                XmlDocument body = new XmlDocument();
                body.Load(Loader.path + "Body.xml");

                // Loop through all childs
                foreach (XmlElement e in body.DocumentElement.ChildNodes)
                {
                    Body.bodyDescription.Add(e.Name, e.InnerText);
                }
            }

            // Image
            foreach (string file in Directory.GetFiles(Loader.images + "/"))
            {
                // Paths
                string name = Path.GetFileNameWithoutExtension(file);
                string path = Loader.images + "/" + name + ".png";

                // Create the Texture
                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(File.ReadAllBytes(path));

                // Add the Texture to the Dictionary
                Image.textures.Add(name, texture);
            }

            if (File.Exists(Loader.path + "Loading.xml"))
            {
                // Loading
                XmlDocument loading = new XmlDocument();
                loading.Load(Loader.path + "Loading.xml");

                // Settings
                if (loading.DocumentElement.HasAttribute("loadingText"))
                {
                    Loading.loadingString = loading.DocumentElement.GetAttribute("loadingText");
                }

                if (loading.DocumentElement.HasAttribute("size"))
                {
                    Loading.size = Int32.Parse(loading.DocumentElement.GetAttribute("size"));
                }

                if (loading.DocumentElement.HasAttribute("replaceFont"))
                {
                    Loading.replaceFont = Boolean.Parse(loading.DocumentElement.GetAttribute("replaceFont"));
                }

                // Loop through all childs
                foreach (XmlElement e in loading.DocumentElement.ChildNodes)
                {
                    Loading.tips.Add(e.InnerText);
                }
            }

            if (File.Exists(Loader.path + "Menu.xml"))
            {
                // Menu
                XmlDocument menu = new XmlDocument();
                menu.Load(Loader.path + "Menu.xml");

                // Should we replace the font?
                if (menu.DocumentElement.HasAttribute("replaceFont"))
                {
                    Menu.replaceFont = Boolean.Parse(menu.DocumentElement.GetAttribute("replaceFont"));
                }

                // Loop through all childs
                foreach (XmlElement e in menu.DocumentElement.ChildNodes)
                {
                    // Name of the node
                    string name = e.GetAttribute("name");

                    // Get KSPSiteURL / SpaceportURL
                    if (name == "KSPsiteURL")
                    {
                        Menu.CommunityURL = e.InnerText;
                    }
                    else if (name == "SpaceportURL")
                    {
                        Menu.SpacePortURL = e.InnerText;
                    }
                    else
                    {
                        // Get the text-size
                        int size = -1;
                        if (e.HasAttribute("size"))
                        {
                            size = Int32.Parse(e.GetAttribute("size"));
                        }

                        // Glue everything together
                        KeyValuePair<float, string> value = new KeyValuePair<float, string>((float)size, e.InnerText);

                        // Add it to the dictionary
                        Menu.buttons.Add(name, value);

                    }
                }
            }

            if (File.Exists(Loader.path + "SpaceCenter.xml"))
            {
                // SpaceCenter
                XmlDocument spaceCenter = new XmlDocument();
                spaceCenter.Load(Loader.path + "SpaceCenter.xml");

                // Should we replace the font?
                if (spaceCenter.DocumentElement.HasAttribute("replaceFont"))
                {
                    SpaceCenter.replaceFont = Boolean.Parse(spaceCenter.DocumentElement.GetAttribute("replaceFont"));
                }

                // New size of the text
                if (spaceCenter.DocumentElement.HasAttribute("size"))
                {
                    SpaceCenter.size = Int32.Parse(spaceCenter.DocumentElement.GetAttribute("size"));
                }

                // Loop through all childs
                foreach (XmlElement e in spaceCenter.DocumentElement.ChildNodes)
                {
                    string name = e.GetAttribute("name");
                    if (!SpaceCenter.ssuiText.ContainsKey(name) && name != "")
                    {
                        SpaceCenter.ssuiText.Add(name, e.InnerText);
                    }
                }
            }

            if (File.Exists(Loader.path + "Text.xml"))
            {
                // Text
                XmlDocument text = new XmlDocument();
                text.Load(Loader.path + "Text.xml");

                // Should we replace the font?
                if (text.DocumentElement.HasAttribute("replaceFont"))
                {
                    Text.replaceFont = Boolean.Parse(text.DocumentElement.GetAttribute("replaceFont"));
                }

                // Loop through all childs
                foreach (XmlElement e in text.DocumentElement.ChildNodes)
                {
                    string en = ((XmlCDataSection)e.FirstChild.FirstChild).InnerText;
                    string txt = ((XmlCDataSection)e.LastChild.FirstChild).InnerText;

                    if (!Text.texts.ContainsKey(en))
                    {
                        Text.texts.Add(en, txt);
                    }
                }
            }

            if (Loader.assetPath != "")
            {
                // Font
                string path = KSPUtil.ApplicationRootPath + Loader.rawPath + Path.DirectorySeparatorChar + Loader.assetPath;
                string extension = Path.GetExtension(path);
                byte[] assetBuffer = File.ReadAllBytes(path.Replace(extension, "_" + ((int)Application.platform).ToString() + extension));

                Font.asset = AssetBundle.CreateFromMemoryImmediate(assetBuffer);

                // Load all true-type fonts
                foreach (UnityEngine.Font ttf in Font.asset.LoadAll(typeof(UnityEngine.Font)))
                {
                    Font.trueTypeFonts.Add(ttf.name, ttf);
                }

                // Load all SpriteFonts - TextAssets
                List<TextAsset> textAssets = new List<TextAsset>();
                foreach (TextAsset textAsset in Font.asset.LoadAll(typeof(TextAsset)))
                {
                    textAssets.Add(textAsset);
                }

                // Textures
                List<Texture2D> textures = new List<Texture2D>();
                foreach (Texture2D texture in Font.asset.LoadAll(typeof(Texture2D)))
                {
                    textures.Add(texture);
                }

                // Glue them together
                foreach (TextAsset textAsset in textAssets)
                {
                    Texture2D textTexture = textures.Find(t => t.name == textAsset.name);

                    Material textMaterial = new Material(Shader.Find("Sprite/Vertex Colored"));
                    textMaterial.SetTexture("_MainTex", textTexture);
                    Material.DontDestroyOnLoad(textMaterial);

                    KeyValuePair<TextAsset, Material> value = new KeyValuePair<TextAsset, Material>(textAsset, textMaterial);
                    Font.spriteFont.Add(textAsset.name, value);
                }
            }

            if (File.Exists(Loader.path + "GUI.xml"))
            {
                // GUI
                XmlDocument gui = new XmlDocument();
                gui.Load(Loader.path + "GUI.xml");

                // Loop through all childs and load the texts
                foreach (XmlElement e in gui.DocumentElement.ChildNodes)
                {
                    string en = ((XmlCDataSection)e.FirstChild.FirstChild).InnerText;
                    string txt = ((XmlCDataSection)e.LastChild.FirstChild).InnerText;

                    if (!Text.texts.ContainsKey(en))
                    {
                        TUI.texts.Add(en, txt);
                    }
                }

                // Patch the Assembly
                if (!Loader.watchdogActive)
                {
                    Assembly mainAssembly = Assembly.GetAssembly(typeof(Game));
                    AssemblyDefinition assembly = AssemblyDefinition.ReadAssembly(mainAssembly.Location);
                    // Go through all types
                    foreach (TypeDefinition typeDef in assembly.MainModule.Types)
                    {
                        if (!typeDef.IsClass)
                            continue;

                        // Go through all methods
                        foreach (MethodDefinition method in typeDef.Methods)
                        {
                            if (!method.HasBody)
                                continue;

                            // Get the ILProcessor
                            ILProcessor ilProcessor = method.Body.GetILProcessor();
                            List<Instruction> toPatch = new List<Instruction>();

                            foreach (Instruction instruction in ilProcessor.Body.Instructions)
                            {
                                if (instruction.OpCode == OpCodes.Call && instruction.Operand != null && ((instruction.Operand as MethodReference).DeclaringType.Name == "GUI" || (instruction.Operand as MethodReference).DeclaringType.Name == "GUILayout"))
                                {
                                    toPatch.Add(instruction);
                                }
                            }

                            for (int i = 0; i < toPatch.Count; i++)
                            {
                                // Get the original Method
                                MethodReference reference = toPatch[i].Operand as MethodReference;

                                // Get the TUI-Type
                                Type tui = null;
                                if (reference.DeclaringType.Name == "GUI")
                                {
                                    tui = typeof(UnityEngine.TUI);
                                }
                                else
                                {
                                    tui = typeof(UnityEngine.TUILayout);
                                }

                                // Save it's name
                                string methodName = reference.Name;

                                // Get it's parameters
                                List<Type> parameters = new List<Type>();
                                Assembly[] loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();

                                // Loop through all Parameters and convert them to System.Types
                                foreach (TypeReference param in reference.Parameters.Select(p => p.ParameterType))
                                {
                                    // Placeholders
                                    Type type = null;
                                    int j = 0;

                                    // Loop through all loaded Assemblies and try to get the Type
                                    while (type == null && j < loadedAssemblies.Count())
                                    {
                                        type = Type.GetType(param.FullName + ", " + loadedAssemblies[j].FullName);
                                        j++;
                                    }

                                    // Add the Type to the parameter-List
                                    parameters.Add(type);
                                }

                                // Get the new Reference from TUI
                                MethodInfo newMethod = null;
                                if (parameters.Count == 0)
                                    newMethod = tui.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static, null, new Type[] { }, null);
                                else
                                    newMethod = tui.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public, null, parameters.ToArray(), null);
                                Instruction newInstruction = Instruction.Create(OpCodes.Call, assembly.MainModule.Import(newMethod));

                                // Inject the new Reference into the ILProcessor
                                ilProcessor.InsertAfter(toPatch[i], newInstruction);
                                ilProcessor.Remove(toPatch[i]);
                            }

                            // Update the Method
                            assembly.MainModule.Import(method);
                        }

                        // Update the Type
                        assembly.MainModule.Import(typeDef);
                    }

                    // Make a backup of the old .dll and write the new one to disk
                    File.Copy(mainAssembly.Location, mainAssembly.Location.Replace(".dll", ".old"), true);
                    FileStream stream = new FileStream(mainAssembly.Location, FileMode.Create, FileAccess.Write, FileShare.Write);
                    assembly.Write(stream);
                    stream.Close();

                    // Create the LaunchHelper
                    Assembly launch = Assembly.Load(LH.LaunchHelper.Helper);
                    Debug.Log(launch.FullName);
                    object o = Activator.CreateInstance(launch.GetExportedTypes().First(t => t.Name == "LaunchHelper"));

                    // Activate it..
                    FieldInfo commandLineArgs = o.GetType().GetField("CommandLineArgs");
                    commandLineArgs.SetValue(o, Environment.GetCommandLineArgs());
                    MethodInfo load = o.GetType().GetMethod("Load");
                    load.Invoke(o, null);

                    // And kill ourselves
                    Application.Quit();
                }
            }
        }
    }
}

