using IsolateIsland.Runtime.Combination;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Inventory
{
    internal abstract class ItemApplyer
    {
        protected bool IsConsumable<T>(in T item) where T : ItemBase
        {
            var converted = item.CombinationNode as DressableCombinationNode;
            return converted.IsComsumable;
        }

        internal virtual void Use<T>(in T item) where T : ItemBase
        {
            if (!(item is SubArrowItem))
                Managers.Managers.Instance.Inventory.Game.SubtractItem(item);
            var config = Managers.Managers.Instance.DI.Get<UI_InventoryAttributeConfigurator>();
            config.SetAttribute();

        }
        internal virtual void Drop<T>(in T item) where T : ItemBase
        {
            Managers.Managers.Instance.Inventory.Game.SubtractItem(item);
            var config = Managers.Managers.Instance.DI.Get<UI_InventoryAttributeConfigurator>();
            config.SetAttribute();
        }
    }
}