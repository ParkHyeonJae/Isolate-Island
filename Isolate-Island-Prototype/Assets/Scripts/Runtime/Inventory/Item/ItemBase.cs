using IsolateIsland.Runtime.Combination;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace IsolateIsland.Runtime.Inventory
{
    [DisallowMultipleComponent]
    [RequireComponent(
          typeof(BoxCollider)
        , typeof(ItemInvoker))]
    public class ItemBase : MonoBehaviour
    {
        [SerializeField] protected CombinationNode _combinationNode;

        class ItemBuilder
        {
            
        }

    }
}