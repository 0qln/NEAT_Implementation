using NeuralNetworks_LIB;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Data;
using UnityEngine.XR;
using System.Runtime.CompilerServices;

namespace NeatImplementation {
    /// <summary>
    /// An extension to the List class, which contains useful methods
    /// </summary>
    public static class ListHelper {
        /// <summary>
        /// Uses Fisher�Yates shuffle to randomize the list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(this IList<T> list) {
            int n = list.Count;
            while (n > 1) {
                n--;
                int k = new Random().Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}