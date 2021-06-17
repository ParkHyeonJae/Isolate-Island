using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Inventory
{
    public class DressableItemInvoker : ItemInvoker
    {
        protected override void OnInvoke()
        {
            Managers.Managers.Instance.Inventory.Dressable.AddItem(Base);
            gameObject.SetActive(false);
        }

    }
}