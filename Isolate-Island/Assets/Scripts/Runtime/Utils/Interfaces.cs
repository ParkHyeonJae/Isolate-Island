using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Utils
{
    internal interface IBuilder<T> where T : new()
    {
        T Build();
    }
    internal interface IStatAble
    {
        string GetStatInfo { get; }
    }

}


