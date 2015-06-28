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
using System.IO;
using System.Linq;
using UnityEngine;

namespace LanguagePatches
{
    [KSPAddon(KSPAddon.Startup.EveryScene, false)]
    public class xText : MonoBehaviour
    {
        private List<int> patchedInstances = new List<int>();
        StreamWriter logger;

        public void Awake()
        {
            logger = new StreamWriter(Storage.directory + "/" + HighLogic.LoadedScene + ".log");
        }

        public void Update()
        {
            if (Storage.Load)
            {
                // Patch all SpriteTexts
                Func<SpriteText, bool> predicate = t => !patchedInstances.Contains(t.GetHashCode());

                foreach (SpriteText txt in Resources.FindObjectsOfTypeAll<SpriteText>().Where(predicate))
                {
                    Log("[SpriteText] " + Escape(txt.Text));
                    txt.Text = Trans(txt.Text);
                    if (Storage.Text.replaceFont)
                        xFont.ReplaceFont(txt);
                    patchedInstances.Add(txt.GetInstanceID());
                    txt.UpdateMesh();
                }

                // Patch all SpriteTextRichs
                Func<SpriteTextRich, bool> predicateRich = t => !patchedInstances.Contains(t.GetHashCode());

                foreach (SpriteTextRich txt in Resources.FindObjectsOfTypeAll<SpriteTextRich>().Where(predicateRich))
                {
                    Log("[SpriteTextRich] " + Escape(txt.Text));
                    txt.Text = Trans(txt.Text);
                    //if (Storage.Text.replaceFont)
                    //    xFont.ReplaceFont(txt);
                    patchedInstances.Add(txt.GetInstanceID());
                    txt.UpdateMesh();
                }

                // Patch all TextMeshes
                Func<TextMesh, bool> predicateTM = t => !patchedInstances.Contains(t.GetHashCode());

                foreach (TextMesh txt in Resources.FindObjectsOfTypeAll<TextMesh>().Where(predicateTM))
                {
                    //Log("[TextMesh] " + Escape(txt.text));
                    if (txt.text.Contains("Loading..."))
                    {
                        txt.text = Storage.Loading.loadingString;
                        if (Storage.Text.replaceFont)
                            xFont.ReplaceFont(txt);
                    }
                    patchedInstances.Add(txt.GetInstanceID());
                }

                // Patch all BaseEvents
                Func<BaseEvent, bool> predicateBE = b => !patchedInstances.Contains(b.id);

                if (HighLogic.LoadedScene == GameScenes.FLIGHT)
                {
                    foreach (Part part in FlightGlobals.ActiveVessel.Parts)
                    {
                        foreach (BaseEvent e in part.Events.Where(predicateBE))
                        {
                            Log("[BaseEvent]: " + Escape(e.GUIName));
                            e.guiName = Trans(e.GUIName);
                            patchedInstances.Add(e.id);
                        }

                        foreach (PartModule partModule in part.Modules)
                        {
                            foreach (BaseEvent e in partModule.Events.Where(predicateBE))
                            {
                                Log("[BaseEvent]: " + Escape(e.GUIName));
                                e.guiName = Trans(e.GUIName);
                                patchedInstances.Add(e.id);
                            }
                        }
                    }
                }
            }
        }

        // Translate a SpriteText
        public static string Trans(string value)
        {
            // Check for Loading-String
            if (value == "Loading...")
            {
                value = Storage.Loading.loadingString;
            }

            // Translate Content
            if (Storage.Text.texts.Count > 0)
            {
                value = (!Storage.Text.texts.ContainsKey(Escape(value))) ? value : Unescape(Storage.Text.texts[Escape(value)]);
            }

            // Return Content
            return value;
        }

        public static string Escape(string s)
        {
            s = s.Replace("\n", "\\n").Replace("\r", "\\r").Replace("\t", "\\t");
            return s;
        }

        public static string Unescape(string s)
        {
            s = s.Replace(@"\n", "\n").Replace(@"\r", "\r").Replace(@"\t", "\t");
            return s;
        }

        private void Log(object o)
        {
            logger.WriteLine(o);
        }
    }
}
