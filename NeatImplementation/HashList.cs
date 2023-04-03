using System.Collections;
using System.Collections.Generic;
using System;
using System.Data;
using System.Runtime.CompilerServices;

namespace NeatImplementation {
    /// <summary>
    /// Contains a HashSet and a List.
    /// </summary>
    /// <typeparam name="Element"></typeparam>
    public class HashList<Element> {

        private List<Element> list;
        private HashSet<Element> hashSet;

        /// <summary>
        /// Constructor
        /// </summary>
        public HashList() {
            list = new List<Element>();
            hashSet = new HashSet<Element>();
        }

        /// <summary>
        /// Returns an element given its index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Element GetElement(int index) {
            return list[index];
        }

        /// <summary>
        /// Adds an element to the list and the HashSet
        /// </summary>
        /// <param name="element"></param>
        public void AddElement(Element element) {
            if (!hashSet.Contains(element)) {
                list.Add(element);
                hashSet.Add(element);
            }
        }

        /// <summary>
        /// Removes an element given its index in the list
        /// </summary>
        /// <param name="index"></param>
        public void RemoveElementAt(int index) {
            list.RemoveAt(index);
            hashSet.Remove(GetElement(index));
        }

        /// <summary>
        /// Removes an element given the element
        /// </summary>
        /// <param name="element"></param>
        public void RemoveElement(Element element) {
            list.Remove(element);
            hashSet.Remove(element);
        }

        /// <summary>
        /// Clears the Hashset and the List
        /// </summary>
        public void Clear() {
            list.Clear();
            hashSet.Clear();
        }

        /// <summary>
        /// Returns true if the <paramref name="element"/> exists.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool Contains(Element element) {
            return hashSet.Contains(element);
        }
    }
}