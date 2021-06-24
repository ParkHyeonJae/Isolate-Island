using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Inventory
{
    internal class DefaultItemApplyer : ItemApplyer
    {
        internal override void Use<T>(in T item)
        {
            base.Use<T>(item);
            Debug.Log("DefaultItemApplyer : Use");



        }

        internal override void Drop<T>(in T item)
        {
            base.Drop<T>(item);
            Debug.Log("DefaultItemApplyer : Drop");
        }
    }
}