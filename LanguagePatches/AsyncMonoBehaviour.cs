/**
 * Language Patches Framework
 * Translates the game into different Languages
 * Copyright (c) 2017 Thomas P.
 * Licensed under the terms of the MIT License
 */

using System;
using System.Collections.Generic;
using UnityEngine;

namespace LanguagePatches
{
    /// <summary>
    /// A system that can split up tasks over multiple threads
    /// </summary>
    public class AsyncMonoBehaviour : MonoBehaviour
    {
        /// <summary>
        /// The method where code is executed
        /// </summary>
        public enum Method
        {
            UPDATE,
            FIXEDUPDATE,
            LATEUPDATE
        }

        /// <summary>
        /// The actions that should be executed
        /// </summary>
        private readonly Dictionary<Method, List<Action>> actions = new Dictionary<Method, List<Action>>();
        private readonly Dictionary<Method, Int32> counter = new Dictionary<Method, Int32>();

        /// <summary>
        /// Registers a method for execution
        /// </summary>
        protected void RegisterTask(Method method, Action task)
        {
            if (actions.ContainsKey(method))
                actions[method].Add(task);
            else
                actions.Add(method, new List<Action> { task });
        }

        /// <summary>
        /// Runs a method
        /// </summary>
        private void Run(Method method)
        {
            if (!actions.ContainsKey(method))
                return;
            if (!counter.ContainsKey(method))
                counter.Add(method, 0);
            actions[method][counter[method]]();
            counter[method]++;
            if (counter[method] == actions[method].Count)
                counter[method] = 0;
        }

        /// <summary>
        /// MonoBehaviour Update
        /// </summary>
        void Update()
        {
            Run(Method.UPDATE);
        }

        /// <summary>
        /// MonoBehaviour FixedUpdate
        /// </summary>
        void FixedUpdate()
        {
            Run(Method.FIXEDUPDATE);
        }

        /// <summary>
        /// MonoBehaviour LateUpdate
        /// </summary>
        void LateUpdate()
        {
            Run(Method.LATEUPDATE);
        }
    }
}