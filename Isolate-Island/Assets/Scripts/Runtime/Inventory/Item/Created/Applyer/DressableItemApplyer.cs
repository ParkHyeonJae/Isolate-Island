using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IsolateIsland.Runtime.Inventory
{
    internal class DressableItemApplyer : ItemApplyer
    {
        private UI_InventoryAttributeConfigurator inventoryAttributeConfigurator;
        internal UI_InventoryAttributeConfigurator InventoryAttributeConfigurator
            => inventoryAttributeConfigurator = inventoryAttributeConfigurator ?? Managers.Managers.Instance.DI.Get<UI_InventoryAttributeConfigurator>();


        public DressableItemApplyer()
        {
            InventoryAttributeConfigurator.CachingDressableAttributeAssets();
        }

        internal override void Use<T>(in T item)
        {
            base.Use<T>(item);
            Debug.Log("DressableItemApplyer : Use");

            Managers.Managers.Instance.Inventory.Dressable.AddItem(item);
        }

        internal override void Drop<T>(in T item)
        {
            
            Debug.Log("DressableItemApplyer : Drop");
            Managers.Managers.Instance.Inventory.Dressable.SubtractItem(item);
            Managers.Managers.Instance.Inventory.Game.AddItem(item);
            var config = Managers.Managers.Instance.DI.Get<UI_InventoryAttributeConfigurator>();
            config.SetAttribute();
        }
    }
}