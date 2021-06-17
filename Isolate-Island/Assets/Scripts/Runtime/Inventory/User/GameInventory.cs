using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Inventory
{
    public class GameInventory : Inventory
    {
        protected override void OnObtainItem(ItemBase item)
        {
            Debug.Log($"{item.CombinationNode.name} 획득");
            Debug.Log(item.ToString());
        }
        protected override void OnCountingItem(ItemBase item)
        {
            Debug.Log($"{item.CombinationNode.name} : {Items[item]} + 1");
        }

        protected override void OnSubtractItem(ItemBase item)
        {
            Debug.Log($"아이템 제거 : {item.CombinationNode.name}");
        }

        protected override void OnProductItem(ItemBase newItem)
        {
            Debug.Log($"새로운 아이템 {newItem.CombinationNode.name}");
        }

    }
}