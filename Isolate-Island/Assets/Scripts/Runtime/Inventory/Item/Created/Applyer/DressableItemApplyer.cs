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

            // 사용한 아이템이 장비아이템이 아니라면 장착하지 말 것
            var dressable = item as DressableItem;
            if (dressable is null)
                return;

            // 소비했을 때 인벤토리에 있고, 장비아이템이 맞다면 기존에 장착되어 있는 장비 칸에 이미 장착된 장비가 있는 지 확인해볼 것
            var parts = Managers.Managers.Instance.Inventory.Dressable
                    .GetParts(dressable.DressableCombinationNode.DressableStat.DRESSABLE_Parts);


            /// 예외적인 케이스로 장비 템일 경우, 장착했을 시 하나의 아이템만 장비칸으로 이동하는 반면에, 소비템일 경우에는 해당 아이템의 모든 개수를 장비템에 옮겨야 한다.
            if (IsConsumable(item))
            {
                var count = Managers.Managers.Instance.Inventory.Game.GetItemCount(item);

                // 소비했을 때 아이템이 인벤토리에 없으면 장비칸에 넣지 말 것
                var contain = Managers.Managers.Instance.Inventory.Game.IsContain(item);
                var contain2 = Managers.Managers.Instance.Inventory.Dressable.IsContain(item);
                if (contain == false)
                    return;

                //if (contain2)
                //    base.Use(item);


                // 이미 장착된 장비템이 있다면 Drop으로 다시 원래 자리로 돌려준다.
                if (parts)
                    DropAll(parts, count);

                DropAll(item, count);

                for (int i = 0; i < count; i++)
                    Managers.Managers.Instance.Inventory.Dressable.AddItem(item);
            }
            else
            {
                // 이미 장착된 장비템이 있다면 Drop으로 다시 원래 자리로 돌려준다.
                if (parts)
                    Drop(parts);

                // 일반 상황에서는 장비 칸에 아이템을 하나만 삽입하게 된다.
                Managers.Managers.Instance.Inventory.Dressable.AddItem(item);
            }

            Managers.Managers.Instance.Sound.PlayOneShot("아이템 장착");

            dressable.OnEnterDressable();
        }
        internal void DropAll<T>(in T item, int count) where T : ItemBase
        {
            for (int i = 0; i < count; i++)
                Drop(item);
        }
        internal void BaseDropAll<T>(in T item, int count) where T : ItemBase
        {
            for (int i = 0; i < count; i++)
                base.Drop(item);
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
                    if (IsConsumable(item))
                    {
                        var count = Managers.Managers.Instance.Inventory.Dressable.GetItemCount(item);
                        for (int i = 0; i < count; i++)
                        {
                            Managers.Managers.Instance.Inventory.Dressable.SubtractItem(item);
                            Managers.Managers.Instance.Inventory.Game.AddItem(item);
                        }
                    }
                    else
                    {
                        Managers.Managers.Instance.Inventory.Dressable.SubtractItem(item);
                        Managers.Managers.Instance.Inventory.Game.AddItem(item);
                    }
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