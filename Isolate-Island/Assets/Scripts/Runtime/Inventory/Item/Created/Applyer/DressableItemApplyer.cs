using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IsolateIsland.Runtime.Inventory
{
    internal class DressableItemApplyer : StatItemApplyer
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

            var dressable = item as DressableItem;
            if (dressable is null)
                return;

            var parts = Managers.Managers.Instance.Inventory.Dressable
                    .GetParts(dressable.DressableCombinationNode.DressableStat.DRESSABLE_Parts);

            if (parts)
                Drop(parts);

            Managers.Managers.Instance.Inventory.Dressable.AddItem(item);

            dressable.OnEnterDressable();
        }

        internal override void Drop<T>(in T item)
        {
            
            Debug.Log("DressableItemApplyer : Drop");

            switch (true)
            {
                case true when HasUseInDressable_GameInventroy(item):
                    Managers.Managers.Instance.Inventory.Dressable.SubtractItem(item);
                    Managers.Managers.Instance.Inventory.Game.AddItem(item);
                    break;
                case true when HasUseInDressableInventroy(item):
                    Managers.Managers.Instance.Inventory.Dressable.SubtractItem(item);
                    Managers.Managers.Instance.Inventory.Game.AddItem(item);
                    break;
                case true when HasUseInGameInventroy(item):
                    Managers.Managers.Instance.Inventory.Game.SubtractItem(item);
                    break;
                default:
                    break;
            }
            
            var config = Managers.Managers.Instance.DI.Get<UI_InventoryAttributeConfigurator>();
            config.SetAttribute();
            var dressable = item as DressableItem;

            if (dressable is null)
                return;

            dressable.OnExitDressable();
        }

        internal bool HasUseInDressable_GameInventroy<T>(in T item) where T : ItemBase
        {
            var dressableContain = Managers.Managers.Instance.Inventory.Dressable.IsContain(item);
            var gameContain = Managers.Managers.Instance.Inventory.Game.IsContain(item);

            if (dressableContain & gameContain)
                return true;
            return false;
        }

        internal bool HasUseInDressableInventroy<T>(in T item) where T : ItemBase
        {
            var dressableContain = Managers.Managers.Instance.Inventory.Dressable.IsContain(item);
            var gameContain      = Managers.Managers.Instance.Inventory.Game.IsContain(item);

            if (dressableContain & !gameContain) 
                return true;
            return false;
        }
        internal bool HasUseInGameInventroy<T>(in T item) where T : ItemBase
        {
            var dressableContain = Managers.Managers.Instance.Inventory.Dressable.IsContain(item);
            var gameContain      = Managers.Managers.Instance.Inventory.Game.IsContain(item);

            if (!dressableContain & gameContain) 
                return true;
            return false;
        }
    }
}