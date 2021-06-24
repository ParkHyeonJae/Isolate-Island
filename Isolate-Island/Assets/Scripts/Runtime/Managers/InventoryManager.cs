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
        private GameInventory _gameInventory;
        public GameInventory Game 
            => _gameInventory = _gameInventory ?? new GameInventory();

        private DressableInventory _dressableInventory;
        public DressableInventory Dressable 
            => _dressableInventory = _dressableInventory ?? new DressableInventory();

        private ProductiveInventory _productiveInventory;
        public ProductiveInventory Productive
            => _productiveInventory = _productiveInventory ?? new ProductiveInventory();


        public void AddItem(ItemBase item)
        {

        }

        public virtual void AppendItem(ItemBase item)
        {
            //Managers.Instance.Inventory.AddItem(item);
        }

        [ContextMenu("TryInqury")]
        public void Inquiry()
        {
            //Managers.Instance.Inventory.InquiryProductiveItem();
        }

        public void OnInit()
        {


        }

        [ContextMenu("Product")]
        public void Product()
        {
            //Managers.Managers.Instance.Inventory.TryInquiryProductiveItem();
        }

    }
}