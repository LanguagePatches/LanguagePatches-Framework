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
    [KSPAddon(KSPAddon.Startup.PSystemSpawn, false)]
    public class PlanetShifter : MonoBehaviour
    {
        public Dictionary<string, string> LoadConfig()
        {
            if (File.Exists(Loader.path + "Body.xml"))
            {
                var body = new Dictionary<string, string>();

                // Load descriptions from .xml
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(Loader.path + "Body.xml");
                foreach (XmlElement child in xmlDocument.DocumentElement.ChildNodes)
                {
                    body[child.Name] = child.InnerText;
                }

                return body;
            }
            else
            {
                return null;
            }
        }

        void Start()
        {
            if (Loader.loadCache == "active")
            { 
                var body = LoadConfig();

                if (body != null)
                {
                    // Scan the solar system and replace descriptions
                    foreach (CelestialBody cb in FlightGlobals.Bodies)
                    {
                        if (body.Keys.Contains(cb.gameObject.name))
                        {
                            cb.bodyDescription = body[cb.gameObject.name];
                            cb.CBUpdate();
                        }
                    }
                }
            }
        }
    }
}
