/** 
 * Language Patches Framework
 * Translates the game into different Languages
 * Copyright (c) 2016 Thomas P.
 * Licensed under the terms of the MIT License
 */

using System;
using System.ComponentModel;
using System.Threading;
using UnityEngine;

namespace LanguagePatches
{
    /// <summary>
    /// A MonoBehaviour that supports async update methods
    /// </summary>
    public abstract class AsyncMonoBehaviour : MonoBehaviour
    {
        /// <summary>
        /// The background worker that manages the new thread
        /// </summary>
        private BackgroundWorker worker;

        /// <summary>
        /// Starts the worker
        /// </summary>
        protected virtual void Awake()
        {
            worker = new BackgroundWorker();
            worker.DoWork += delegate { while (worker.IsBusy) Thread.Sleep(1000); };
            worker.ProgressChanged += delegate(System.Object sender, ProgressChangedEventArgs e)
            {
                if (e.ProgressPercentage == 10)
                    AsyncUpdate();
                else if (e.ProgressPercentage == 20)
                    AsyncFixedUpdate();
                else if (e.ProgressPercentage == 30)
                    AsyncLateUpdate();
            };
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync();
        }

        // MonoBehaviour Methods
        void Update() { worker.ReportProgress(10); }
        void FixedUpdate() { worker.ReportProgress(20); }
        void LateUpdate() { worker.ReportProgress(30); }

        // Async methods
        protected virtual void AsyncUpdate() { }
        protected virtual void AsyncFixedUpdate() { }
        protected virtual void AsyncLateUpdate() { }
    }
}