using System;
using UnityEngine;

namespace IsolateIsland.Runtime.Inventory
{
    // 장비 전용 인벤토리
    public sealed class ProductiveInventory : Inventory
    {
        private UI_InventoryAttributeConfigurator inventoryAttributeConfigurator;
        internal UI_InventoryAttributeConfigurator InventoryAttributeConfigurator
            => inventoryAttributeConfigurator = inventoryAttributeConfigurator ?? Managers.Managers.Instance.DI.Get<UI_InventoryAttributeConfigurator>();

        protected override void OnObtainItem(ItemBase @base)
        {

        }

        protected override void OnCountingItem(ItemBase @base)
        {

        }

        protected override void OnSubtractItem(ItemBase @base)
        {

        }

        protected override void OnProductItem(ItemBase @base)
        {

        }

    }
}