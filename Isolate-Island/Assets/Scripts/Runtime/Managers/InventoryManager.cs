using IsolateIsland.Runtime.Inventory;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var craftingTable = Managers.Instance.combinationManager.CraftingTable;
            foreach (var _item in craftingTable)
            {
                var _combinationNode = _item.Value;
                if (!IsProductiveItem(_combinationNode))        // 만들 수 있는 아이템인지
                    continue;

                //Todo : 생산 가능한 아이템들
                Debug.Log($"제작가능한 아이템 목록 : {_combinationNode.name}");
            }
        }

        private bool IsProductiveItem(Combination.CombinationNode @node)
        {
            if (Items.Count == 0 || @node.combinationNodes.Length == 0)
                return false;

            return @node.combinationNodes.All((_node) =>
            {
                foreach (var _item in Items.Keys)
                {
                    var nodeCompare = _item.GetCombinationNode == _node.combinationNode;
                    var countCompare = _node.Count <= Items[_item];
                    if (nodeCompare && countCompare)
                        return true;
                }
                return false;
            });
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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("현재 아이템 목록 : \n");

            Items.ToList().ForEach(e => sb.Append($"{e.Key.name} : {e.Value}개\n"));

            return sb.ToString();
        }
    }
}