/**
 * Language Patches Framework
 * Translates the game into different Languages
 * Copyright (c) 2017 Thomas P.
 * Licensed under the terms of the MIT License
 */

using System;
using System.Collections;
using System.Collections.Generic;

namespace LanguagePatches
{
    /// <summary>
    /// A class that combines multiple lists
    /// </summary>
    public class CombinedDictionary
    {
        /// <summary>
        /// The values stored in the dict
        /// </summary>
        protected Dictionary<Type, IDictionary> _values { get; set; }

        /// <summary>
        /// Creates a new CombinedList
        /// </summary>
        public CombinedDictionary()
        {
            _values = new Dictionary<Type, IDictionary>();
        }

        /// <summary>
        /// Adds a new element to the list
        /// </summary>
        public void Add<T1, T2>(T1 key, T2 value)
        {
            if (_values.ContainsKey(TypeOf<T1, T2>()))
                _values[TypeOf<T1, T2>()].Add(key, value);
            else
                _values.Add(TypeOf<T1, T2>(), new Dictionary<T1, T2> { { key, value } });
        }

        /// <summary>
        /// Checks whether the dict contains a key value pair
        /// </summary>
        public Boolean Contains<T1, T2>(T1 key, T2 value)
        {
            if (!_values.ContainsKey(TypeOf<T1, T2>()))
                return false;
            IDictionary dict = _values[TypeOf<T1, T2>()];
            if (!dict.Contains(key))
                return false;
            return dict[key] == (Object)value;
        }

        /// <summary>
        /// Checks if the dict contains a key
        /// </summary>
        public Boolean Contains<T1, T2>(T1 key)
        {
            return _values.ContainsKey(TypeOf<T1, T2>()) && _values[TypeOf<T1, T2>()].Contains(key);
        }

        /// <summary>
        /// Replaces the value of key with value
        /// </summary>
        public void Set<T1, T2>(T1 key, T2 value)
        {
            if (!_values.ContainsKey(TypeOf<T1, T2>()))
                return;
            _values[TypeOf<T1, T2>()][key] = value;
        }

        /// <summary>
        /// Replaces the value of key with value
        /// </summary>
        public T2 Get<T1, T2>(T1 key)
        {
            if (!_values.ContainsKey(TypeOf<T1, T2>()))
                return default(T2);
            return (T2)_values[TypeOf<T1, T2>()][key];
        }

        /// <summary>
        /// Combines two generics to an unique type
        /// </summary>
        protected Type TypeOf<T1, T2>()
        {
            return typeof(KeyValuePair<T1, T2>);
        }
    }
}