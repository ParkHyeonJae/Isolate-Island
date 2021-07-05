using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IsolateIsland.Runtime.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace IsolateIsland.Runtime.Inventory
{
    public class UI_InventoryAttributeConfigurator : UI_InventoryConfigurator
    {
        

        internal struct DressableAttributeForm
        {
            internal enum Equipment
            {
                Bag,
                Equipment_Parent,
                ProductiveTable,
                Person_Image,
                Head,
                Body,
                Leg,
                Weapon,
                Item
            }

            internal GameObject obj_uiBag;
            internal GameObject obj_Equipment;
            internal GameObject obj_ProductiveTable;
            internal GameObject obj_Person_Image;
            internal GameObject obj_Head;
            internal GameObject obj_Body;
            internal GameObject obj_Leg;
            internal GameObject obj_Weapon;
            internal GameObject obj_Item;


            internal UI_InventoryAttributeSetter ui_Person_Image;
            internal UI_InventoryAttributeSetter ui_Head;
            internal UI_InventoryAttributeSetter ui_Body;
            internal UI_InventoryAttributeSetter ui_Leg;
            internal UI_InventoryAttributeSetter ui_Weapon;
            internal UI_InventoryAttributeSetter ui_Item;

        }


        private Transform _itemListParent;
        public Transform ItemListParent 
            => _itemListParent = _itemListParent 
            ?? Managers.Managers.Instance.DI.Get(
                Utils.Defines.Load_Object.Item_List
                )?.transform;

        

        
        internal DressableAttributeForm _dressableAttrForm = new DressableAttributeForm();

        private void Start()
        {
            CachingAttributeAssets();
        }
        

        class InventoryAttributeSetterComparer : IComparer<UI_InventoryAttributeSetter>
        {
            public int Compare(UI_InventoryAttributeSetter x, UI_InventoryAttributeSetter y)
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

        internal override void OnSelectAttribute(ItemBase item)
        {
            base.OnSelectAttribute(item);
            //attributeForm.obj_use.SetActive(true);
            //attributeForm.obj_drop.SetActive(true);
        }

        internal override void OnSettingInventory(KeyValuePair<ItemBase, int>[] itemList, UI_InventorySetter[] setter)
        {
            base.OnSettingInventory(itemList, setter);
        }

        internal override void OnResetInventroy(KeyValuePair<ItemBase, int>[] itemList, UI_InventorySetter[] setter)
        {
            base.OnResetInventroy(itemList, setter);
        }


        internal override void SetAttribute()
        {
            var itemList = Managers.Managers.Instance.Inventory.Game.Items.ToArray();

            var setter = Managers.Managers.Instance.DI.Gets<UI_InventoryAttributeSetter>();

            Managers.Managers.Instance.DI.Get(AttributeForm.Load_Making.Making).SetActive(false);

            Array.Sort(setter, new InventoryAttributeSetterComparer());

            //CachingAttributeAssets();

            OnResetInventroy(itemList, setter);
            OnSettingInventory(itemList, setter);

        }




        internal void CachingDressableAttributeAssets()
        {
            // Caching Objects

            _dressableAttrForm.obj_uiBag             = Managers.Managers.Instance.DI.Get(DressableAttributeForm.Equipment.Bag);

            _dressableAttrForm.obj_Equipment         = Managers.Managers.Instance.DI.Get(DressableAttributeForm.Equipment.Equipment_Parent).transform.GetChild(0).gameObject;
            _dressableAttrForm.obj_ProductiveTable   = Managers.Managers.Instance.DI.Get(_dressableAttrForm.obj_uiBag.transform, DressableAttributeForm.Equipment.ProductiveTable);

            _dressableAttrForm.obj_Equipment        .SetActive(true);
            _dressableAttrForm.obj_ProductiveTable  .SetActive(false);

            _dressableAttrForm.obj_Person_Image     = Managers.Managers.Instance.DI.Get(DressableAttributeForm.Equipment.Person_Image);
            _dressableAttrForm.obj_Head  = Managers.Managers.Instance.DI.Get(DressableAttributeForm.Equipment.Head);
            _dressableAttrForm.obj_Body = Managers.Managers.Instance.DI.Get(DressableAttributeForm.Equipment.Body);
            _dressableAttrForm.obj_Leg = Managers.Managers.Instance.DI.Get(DressableAttributeForm.Equipment.Leg);
            _dressableAttrForm.obj_Weapon = Managers.Managers.Instance.DI.Get(DressableAttributeForm.Equipment.Weapon);
            _dressableAttrForm.obj_Item = Managers.Managers.Instance.DI.Get(DressableAttributeForm.Equipment.Item);

            // Caching Components



            //_dressableAttrForm.ui_Person_Image = _dressableAttrForm.obj_Person_Image.GetOrAddComponent<UI_InventoryAttributeSetter>();
            _dressableAttrForm.ui_Head = _dressableAttrForm.obj_Head.GetOrAddComponent<UI_InventoryAttributeSetter>();
            _dressableAttrForm.ui_Body = _dressableAttrForm.obj_Body.GetOrAddComponent<UI_InventoryAttributeSetter>();
            _dressableAttrForm.ui_Leg = _dressableAttrForm.obj_Leg.GetOrAddComponent<UI_InventoryAttributeSetter>();
            _dressableAttrForm.ui_Weapon = _dressableAttrForm.obj_Weapon.GetOrAddComponent<UI_InventoryAttributeSetter>();
            _dressableAttrForm.ui_Item = _dressableAttrForm.obj_Item.GetOrAddComponent<UI_InventoryAttributeSetter>();

            //_dressableAttrForm.ui_Head.SetAttribute()

            //_attributeForm.item_useButton.onClick.RemoveAllListeners();
            //_attributeForm.item_dropButton.onClick.RemoveAllListeners();

            //_attributeForm.item_useButton.onClick.AddListener(Button_OnUse);
            //_attributeForm.item_dropButton.onClick.AddListener(Button_OnDrop);

        }

        

        
    }
}