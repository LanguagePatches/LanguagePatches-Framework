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
using System.Linq;
using UnityEngine;

namespace LanguagePatches
{
    [KSPAddon(KSPAddon.Startup.PSystemSpawn, false)]
    public class PlanetShifter : MonoBehaviour
    {
        public void Start()
        {
            if (Storage.Body.bodyDescription.Count > 0 && Storage.Load)
            {
                // Function that returns every patchable body
                Func<CelestialBody, bool> predicate = cb => Storage.Body.bodyDescription.Keys.Contains(cb.gameObject.name);

                // Scan the solar system and replace descriptions
                foreach (CelestialBody cb in FlightGlobals.Bodies.Where(predicate))
                {
                    cb.bodyDescription = Storage.Body.bodyDescription[cb.gameObject.name];
                }
            }
        }
    }
}

