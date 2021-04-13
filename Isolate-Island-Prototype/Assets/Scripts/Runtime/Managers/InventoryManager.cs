using IsolateIsland.Runtime.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Managers
{
    public class InventoryManager : IManagerInit
    {
        private List<ItemBase> items;
        public List<ItemBase> Items
        {
            get
            {
                if (items == null)
                    items = new List<ItemBase>();

                return items;
            }
        }

        public void OnInit()
        {
            
        }

        public void AddItem(ItemBase @base) => Items.Add(@base);
    }
}