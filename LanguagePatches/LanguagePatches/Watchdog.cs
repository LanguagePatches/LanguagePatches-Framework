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

using System.IO;
using System;
using System.Reflection;
using UnityEngine;

namespace LanguagePatches
{
    [KSPAddon(KSPAddon.Startup.Instantly, false)]
    public class Watchdog : MonoBehaviour
    {
        private static string assemblyLocation = "";

        public void Start()
        {
            if (Loader.watchdogActive)
            {
                // Create an Event Handler for the crash of the Application
                assemblyLocation = Assembly.GetAssembly(typeof(Game)).Location;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }
        }

        private void OnDestroy()
        {
            // When the Process exits, delete the modified .dll and restore the original one
            if (Loader.watchdogActive)
            {
                UnityEngine.Debug.Log("WATCHDOG!");
                File.Delete(assemblyLocation);
                File.Move(assemblyLocation.Replace(".dll", ".old"), assemblyLocation);
            }
        }
    }
}
