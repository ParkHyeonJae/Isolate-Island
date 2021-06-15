using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IsolateIsland.Runtime.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace IsolateIsland.Runtime.Inventory
{
    internal abstract class ItemApplyer
    {
        internal virtual void Use<T>(in T item) where T : ItemBase
        {
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


    internal class StatItemApplyer : ItemApplyer
    {
        internal override void Use<T>(in T item)
        {
            base.Use<T>(item);
            Debug.Log("StatItemApplyer : Use");
            var converted = item as StatItem;

            var effectStat = Managers.Managers.Instance.statManager.UserStat;
            converted.StatCombinationNode.Stat.Apply(ref effectStat);
            Managers.Managers.Instance.statManager.UserStat = effectStat;
            Debug.Log(Managers.Managers.Instance.statManager.UserStat.ToString());
        }

        internal override void Drop<T>(in T item)
        {
            base.Drop<T>(item);
            Debug.Log("StatItemApplyer : Drop");
        }
    }

    internal class DressableItemApplyer : ItemApplyer
    {
        internal override void Use<T>(in T item)
        {
            base.Use<T>(item);
            Debug.Log("DressableItemApplyer : Use");



        }

        internal override void Drop<T>(in T item)
        {
            base.Drop<T>(item);
            Debug.Log("DressableItemApplyer : Drop");
        }
    }


    public class UI_InventoryAttributeConfigurator : MonoBehaviour
    {
        internal struct AttributeForm
        {
            internal enum Load_Making
            {
                Making,
                Item_Image,
                Item_Use,
                Item_Drop,
                Item_Name,
                Item_Stat,
                Item_Description
            }

            internal GameObject obj_making;
            internal GameObject obj_image;
            internal GameObject obj_name;
            internal GameObject obj_stat;
            internal GameObject obj_description;
            internal GameObject obj_use;
            internal GameObject obj_drop;


            internal Image item_image;
            internal Text item_nameText;
            internal Text item_statText;
            internal Text item_descriptionText;
            internal Button item_useButton;
            internal Button item_dropButton;
        }

        private Transform _itemListParent;
        public Transform ItemListParent 
            => _itemListParent = _itemListParent 
            ?? Managers.Managers.Instance.DI.Get(
                Utils.Defines.Load_Object.Item_List
                )?.transform;

        private ItemBase _selectItem = null;

        private AttributeForm _attributeForm = new AttributeForm();

        private EventHandler<ItemBase> _onClickInventroyItemHandler;

        private void OnEnable() => SetAttribute();

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


        internal void OnResetInventroy(
            KeyValuePair<ItemBase, int>[] itemList
            , UI_InventoryAttributeSetter[] setter)
        {
            // Reset
            for (int i = 0; i < setter.Length; i++)
            {
                var attrIdx = setter[i];
                attrIdx.OnReset();
            }
        }


        internal void OnSettingInventory(KeyValuePair<ItemBase, int>[] itemList
            , UI_InventoryAttributeSetter[] setter)
        {
            // Setting
            for (int i = 0; i < itemList.Length; i++)
            {
                var attrIdx = setter[i];
                var table = itemList[i].Key;

                attrIdx.SetAttribute(table, OnSelectAttribute);
            }

        }


        internal void SetAttribute()
        {
            var itemList = Managers.Managers.Instance.Inventory.Game.Items.ToArray();

            var setter = Managers.Managers.Instance.DI.Gets<UI_InventoryAttributeSetter>();

            Managers.Managers.Instance.DI.Get(AttributeForm.Load_Making.Making).SetActive(false);

            Array.Sort(setter, new InventoryAttributeSetterComparer());

            CachingAttributeAssets();

            OnResetInventroy(itemList, setter);
            OnSettingInventory(itemList, setter);

        }

       

        


        private void CachingAttributeAssets()
        {
            // Caching Objects
            _attributeForm.obj_making       = Managers.Managers.Instance.DI.Get(AttributeForm.Load_Making.Making);
            _attributeForm.obj_image        = Managers.Managers.Instance.DI.Get(AttributeForm.Load_Making.Item_Image);
            _attributeForm.obj_name         = Managers.Managers.Instance.DI.Get(AttributeForm.Load_Making.Item_Name);
            _attributeForm.obj_stat         = Managers.Managers.Instance.DI.Get(AttributeForm.Load_Making.Item_Stat);
            _attributeForm.obj_description  = Managers.Managers.Instance.DI.Get(AttributeForm.Load_Making.Item_Description);
            _attributeForm.obj_use          = Managers.Managers.Instance.DI.Get(AttributeForm.Load_Making.Item_Use);
            _attributeForm.obj_drop         = Managers.Managers.Instance.DI.Get(AttributeForm.Load_Making.Item_Drop);


            // Caching Components

            _attributeForm.item_image           = _attributeForm.obj_image.GetComponent<Image>();
            _attributeForm.item_nameText        = _attributeForm.obj_name.GetComponent<Text>();
            _attributeForm.item_statText        = _attributeForm.obj_stat.GetComponent<Text>();
            _attributeForm.item_descriptionText = _attributeForm.obj_description.GetComponent<Text>();
            _attributeForm.item_useButton       = _attributeForm.obj_use.GetComponent<Button>();
            _attributeForm.item_dropButton      = _attributeForm.obj_drop.GetComponent<Button>();


            _attributeForm.item_useButton.onClick.RemoveAllListeners();
            _attributeForm.item_dropButton.onClick.RemoveAllListeners();

            _attributeForm.item_useButton.onClick.AddListener(Button_OnUse);
            _attributeForm.item_dropButton.onClick.AddListener(Button_OnDrop);


        }

        private ItemApplyer GetTypeToReturnApplyerFactory(ItemBase item)
        {
            switch (item)
            {
                case StatItem _:
                    return new StatItemApplyer();
                case DressableItem _:
                    return new DressableItemApplyer();
                default:
                    return default(ItemApplyer);
            }
        }

        private void Button_OnUse()
        {
            var selectItem = _selectItem;

            var itemApplyer = GetTypeToReturnApplyerFactory(selectItem);
            itemApplyer.Use(selectItem);

            
        }

        private void Button_OnDrop()
        {
            var selectItem = _selectItem;

            var itemApplyer = GetTypeToReturnApplyerFactory(selectItem);
            itemApplyer.Drop(selectItem);
        }

        IStatAble GetTypeToReturnStatAbleFactory(ItemBase item)
        {
            switch (item)
            {
                case StatItem statItem:
                    return statItem;
                case DressableItem dressableItem:
                    return dressableItem;
                default:
                    return default(IStatAble);
            }
        }

        private void OnSelectAttribute(ItemBase item)
        {
            _selectItem = item;

            _attributeForm.obj_making.SetActive(true);


            _attributeForm.item_image.sprite = item.CombinationNode.sprite;
            _attributeForm.item_nameText.text = item.CombinationNode.name;
            _attributeForm.item_descriptionText.text = item.CombinationNode.description;


            var statAble = GetTypeToReturnStatAbleFactory(item);
            _attributeForm.item_statText.text = (statAble != null) ? statAble.GetStatInfo : "";

        }


    }
}