using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Utils
{
    public class DIInjectAttribute : Attribute
    {
        string _name = string.Empty;
        Type _type = default;

        public DIInjectAttribute(Type type) { _type = type; }

        public DIInjectAttribute(string name, Type type) { _name = name; _type = type; }



    }

}