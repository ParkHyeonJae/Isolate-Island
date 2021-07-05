using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Utils
{
    public class Comparers
    {
        public class StringStateComparer : IEqualityComparer<string>
        {
            public bool Equals(string x, string y) => x.GetHashCode() == y.GetHashCode();
            public int GetHashCode(string obj) => obj.GetHashCode();
        }

        public class ObjectComparer : IEqualityComparer<GameObject>
        {
            public bool Equals(GameObject x, GameObject y)
            {
                return Object.ReferenceEquals(x, y);
            }

            public int GetHashCode(GameObject obj)
            {
                return obj.GetHashCode();
            }
        }

    }

    
}