using IsolateIsland.Runtime.Inventory;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IsolateIsland.Runtime.Managers
{
    public class InventoryManager : IManagerInit
    {
        private Dictionary<ItemBase, int> items;
        public Dictionary<ItemBase, int> Items
        {
            get
            {
                if (items == null)
                    items = new Dictionary<ItemBase, int>(new ItemsEqualityComparer());

                return items;
            }
        }

        class ItemsEqualityComparer : IEqualityComparer<ItemBase>
        {
            public bool Equals(ItemBase x, ItemBase y)
            {
                return ((string)x).Equals((string)y);
            }

            public int GetHashCode(ItemBase obj)
            {
                return (int)obj;
            }
        }

        [ContextMenu("InquiryProductiveItem")]
        public void InquiryProductiveItem()
        {
            foreach (var _item in Items)
            {
                var _combinationNode = _item.Key.GetCombinationNode;
                if (!IsProductiveItem(_combinationNode))
                    continue;

                //Todo : 생산 가능한 아이템들

            }
        }

        private bool IsProductiveItem(Combination.CombinationNode @node)
        {
            foreach (var _node in @node.combinationNodes)
            {
                // Find Out Productive
                if (Items.Keys.Any(_item =>
                _item.GetCombinationNode == _node.combinationNode
                && _node.Count < Items[_item]))
                {
                    return true;
                }
                return false;
            }


            return false;
        }

        public void OnInit()
        {

        }

        public void AddItem(ItemBase @base)
        {
            var value = 0;
            if (!Items.TryGetValue(@base, out value))
            {
                Items.Add(@base, 1);
                Debug.Log($"{@base.GetCombinationNode.name} 획득");
                Debug.Log(@base.ToString());
                return;
            }

            Debug.Log($"{@base.GetCombinationNode.name} : {Items[@base]} + 1");
            Items[@base] = value + 1;
        }
    }
}