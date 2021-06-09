using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace IsolateIsland.Runtime.Inventory
{
    public abstract class Inventory
    {

        protected abstract void OnObtainItem(ItemBase item);
        protected abstract void OnCountingItem(ItemBase item);
        protected abstract void OnSubtractItem(ItemBase item);
        protected abstract void OnProductItem(ItemBase newItem);

        public virtual void PrintItemList() => Debug.Log(ToString());


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

        private void DeleteItem(ItemBase @base)
        {
            if (!Items.ContainsKey(@base))
                return;
            Items.Remove(@base);
        }

        public void SubtractItem(ItemBase @base)
        {
            var value = 0;
            if (!Items.TryGetValue(@base, out value))
                return;

            OnSubtractItem(@base);

            Items[@base] = value - 1;

            if (Items[@base] > 0)
                return;

            DeleteItem(@base);
        }

        private ItemBase FindItemByCombinationNode(Combination.CombinationNode combinationNode)
        {
            //Items.OfType<ItemBase>().First((pair) => pair.CombinationNode == combinationNode);

            foreach (var item in Items)
            {
                if (item.Key.CombinationNode != combinationNode)
                    continue;

                return item.Key;
            }
            return default(ItemBase);

        }

        public void ProductItem(Combination.CombinationNode combinationNode)
        {
            if (!IsProductiveItem(combinationNode))
                return;

            var itemBuilder = new ItemBuilder();
            var item = itemBuilder
                .SetCombinationNode(combinationNode)
                .Build();

            AddItem(item);


            foreach (var node in combinationNode.combinationNodes)
            {
                var itemData = FindItemByCombinationNode(node.combinationNode);
                for (int i = 0; i < node.Count; i++)
                    SubtractItem(itemData);

            }

            OnProductItem(item);
        }

        //[ContextMenu("InquiryProductiveItem")]
        public void InquiryProductiveItem()
        {
            var craftingTable = Managers.Managers.Instance.Combination.CraftingTable;
            foreach (var _item in craftingTable)
            {
                var _combinationNode = _item.Value;
                if (!IsProductiveItem(_combinationNode))        // 만들 수 있는 아이템인지
                    continue;

                //Todo : 생산 가능한 아이템들
                Debug.Log($"제작가능한 아이템 목록 : {_combinationNode.name}");


            }
        }

        //[ContextMenu("TryInquiryProductiveItem")]
        public void TryInquiryProductiveItem()
        {
            var craftingTable = Managers.Managers.Instance.Combination.CraftingTable;
            foreach (var _item in craftingTable)
            {
                var _combinationNode = _item.Value;
                if (!IsProductiveItem(_combinationNode))        // 만들 수 있는 아이템인지
                    continue;

                //Todo : 생산 가능한 아이템들
                Debug.Log($"==== 제작 ====");
                ProductItem(_combinationNode);

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
                    var nodeCompare = _item.CombinationNode == _node.combinationNode;
                    var countCompare = _node.Count <= Items[_item];
                    if (nodeCompare && countCompare)
                        return true;
                }
                return false;
            });
        }


        public void AddItem(ItemBase @base)
        {
            var value = 0;
            if (!Items.TryGetValue(@base, out value))
            {
                Items.Add(@base, 1);
                OnObtainItem(@base);
                return;
            }


            Items[@base] = value + 1;
            OnCountingItem(@base);
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