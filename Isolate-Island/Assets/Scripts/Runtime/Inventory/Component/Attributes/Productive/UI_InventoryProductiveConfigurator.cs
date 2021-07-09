using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IsolateIsland.Runtime.Inventory
{
    public class UI_InventoryProductiveConfigurator : UI_InventoryConfigurator
    {
        class InventoryProductiveSetterComparer : IComparer<UI_InventoryProductiveSetter>
        {
            public int Compare(UI_InventoryProductiveSetter x, UI_InventoryProductiveSetter y)
            {
                var index01 = x.Index;
                var index02 = y.Index;

                if (index01 > index02)
                    return 1;
                else if (index01 == index02)
                    return 0;
                else
                    return -1;
            }
        }
        private void Start()
        {
            CachingAttributeAssets();
        }
        protected Combination.CombinationNode _selectNode;
        internal override void SetAttribute()
        {
            var itemList = Managers.Managers.Instance.Inventory.Game.Items.ToArray();

            var setter = Managers.Managers.Instance.DI.Gets<UI_InventoryProductiveSetter>();
            Array.Sort(setter, new InventoryProductiveSetterComparer());

            var productiveLists = Managers.Managers.Instance.Inventory.Game.GetProductiveItem();

            OnResetInventroy(itemList, setter);
            OnSettingInventory(itemList, setter);

            for (int i = 0; i < productiveLists.Count; i++)
            {
                if (i >= setter.Length)
                    continue;

                var node = productiveLists[i];


                setter[i].SetProductiveAttribute(node, OnSelectProductiveAttribute);
            }

            
        }

        internal override void OnSettingInventory(KeyValuePair<ItemBase, int>[] itemList, UI_InventorySetter[] setter)
        {

        }

        internal override void OnResetInventroy(KeyValuePair<ItemBase, int>[] itemList, UI_InventorySetter[] setter)
        {
            base.OnResetInventroy(itemList, setter);
        }

        internal virtual void OnSelectProductiveAttribute(Combination.CombinationNode node)
        {
            OnInitalizeButtonEvent();

            attributeForm.item_useText.text = "조합";

            _selectNode = node;

            attributeForm.obj_making.SetActive(true);


            attributeForm.item_image.sprite = node.sprite;
            attributeForm.item_nameText.text = node.name;
            attributeForm.item_descriptionText.text = node.description;

            var statNode = node as Combination.StatCombinationNode;
            attributeForm.item_statText.text = (!(statNode is null)) ? statNode.GetStatInfo() : "";

            attributeForm.obj_use.SetActive(true);
            attributeForm.obj_drop.SetActive(false);
        }


        protected override void Button_OnUse()
        {
            Managers.Managers.Instance.Inventory.Game.ProductItem(_selectNode);


            SetAttribute();
            Managers.Managers.Instance.DI.Get<UI_InventoryAttributeConfigurator>().SetAttribute();

        }


        protected override void Button_OnDrop()
        {

        }

    }
}