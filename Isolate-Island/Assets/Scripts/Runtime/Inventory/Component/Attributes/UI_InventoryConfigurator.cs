using IsolateIsland.Runtime.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IsolateIsland.Runtime.Inventory
{
    public abstract class UI_InventoryConfigurator : MonoBehaviour
    {
        internal class AttributeForm
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
            internal Text item_useText;
            internal Text item_dropText;
        }

        protected ItemBase _selectItem = null;
        private AttributeForm _attributeForm;
        internal AttributeForm attributeForm
        {
            get
            {
                if (_attributeForm is null)
                {
                    _attributeForm = new AttributeForm();
                    CachingAttributeAssets();
                }

                return _attributeForm;
            }
        }

        private void OnEnable() => SetAttribute();
        internal abstract void SetAttribute();

        internal virtual void OnResetInventroy(
            KeyValuePair<ItemBase, int>[] itemList
            , UI_InventorySetter[] setter)
        {
            // Reset
            for (int i = 0; i < setter.Length; i++)
            {
                var attrIdx = setter[i];
                attrIdx.OnReset();
            }
        }


        internal virtual void OnSettingInventory(KeyValuePair<ItemBase, int>[] itemList
            , UI_InventorySetter[] setter)
        {
            // Setting
            for (int i = 0; i < itemList.Length; i++)
            {
                var attrIdx = setter[i];
                var table = itemList[i].Key;

                attrIdx.SetAttribute(table, OnSelectAttribute);
            }

        }

        internal IStatAble GetTypeToReturnStatAbleFactory(ItemBase item)
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
        internal ItemApplyer GetTypeToReturnApplyerFactory(ItemBase item)
        {
            switch (item)
            {
                case StatItem _:
                    return new StatItemApplyer();
                case DressableItem _:
                    return new DressableItemApplyer();
                default:
                    return new DefaultItemApplyer();
            }
        }

        internal string dressable_use => "장착";
        internal string dressable_drop => "해제";
        internal string stat_use => "사용";
        internal string stat_drop => "버림";

        internal virtual void SetGUIText(ItemBase item, out string use, out string drop)
        {
            switch (item)
            {
                case StatItem _:
                    use = stat_use;
                    drop = stat_drop;
                    break;
                case DressableItem _:
                    use = dressable_use;
                    drop = dressable_drop;
                    break;
                default:
                    use = stat_use;
                    drop = stat_drop;
                    break;
            }
        }

        internal virtual void OnSelectAttribute(ItemBase item)
        {
            OnInitalizeButtonEvent();

            string use = string.Empty, drop = string.Empty;
            SetGUIText(item, out use, out drop);


            attributeForm.item_useText.text = use;
            attributeForm.item_dropText.text = drop;



            _selectItem = item;

            attributeForm.obj_making.SetActive(true);


            attributeForm.item_image.sprite = item.CombinationNode.sprite;
            attributeForm.item_nameText.text = item.CombinationNode.name;
            attributeForm.item_descriptionText.text = item.CombinationNode.description;


            var statAble = GetTypeToReturnStatAbleFactory(item);
            attributeForm.item_statText.text = (!(statAble is null)) ? statAble.GetStatInfo : "";

        }

        protected virtual void Button_OnUse()
        {
            var selectItem = _selectItem;

            var itemApplyer = GetTypeToReturnApplyerFactory(selectItem);
            itemApplyer.Use(selectItem);


        }

        protected virtual void Button_OnDrop()
        {
            var selectItem = _selectItem;

            var itemApplyer = GetTypeToReturnApplyerFactory(selectItem);
            itemApplyer.Drop(selectItem);
        }

        protected virtual void CachingAttributeAssets()
        {
            // Caching Objects
            attributeForm.obj_making = Managers.Managers.Instance.DI.Get(AttributeForm.Load_Making.Making);
            attributeForm.obj_image = Managers.Managers.Instance.DI.Get(AttributeForm.Load_Making.Item_Image);
            attributeForm.obj_name = Managers.Managers.Instance.DI.Get(AttributeForm.Load_Making.Item_Name);
            attributeForm.obj_stat = Managers.Managers.Instance.DI.Get(AttributeForm.Load_Making.Item_Stat);
            attributeForm.obj_description = Managers.Managers.Instance.DI.Get(AttributeForm.Load_Making.Item_Description);
            attributeForm.obj_use = Managers.Managers.Instance.DI.Get(AttributeForm.Load_Making.Item_Use);
            attributeForm.obj_drop = Managers.Managers.Instance.DI.Get(AttributeForm.Load_Making.Item_Drop);


            // Caching Components

            attributeForm.item_image = attributeForm.obj_image.GetComponent<Image>();
            attributeForm.item_nameText = attributeForm.obj_name.GetComponent<Text>();
            attributeForm.item_statText = attributeForm.obj_stat.GetComponent<Text>();
            attributeForm.item_descriptionText = attributeForm.obj_description.GetComponent<Text>();
            attributeForm.item_useButton = attributeForm.obj_use.GetComponent<Button>();
            attributeForm.item_dropButton = attributeForm.obj_drop.GetComponent<Button>();
            attributeForm.item_useText = attributeForm.item_useButton.transform.GetChild(0).GetComponent<Text>();
            attributeForm.item_dropText = attributeForm.item_dropButton.transform.GetChild(0).GetComponent<Text>();


            OnInitalizeButtonEvent();
        }

        protected virtual void OnInitalizeButtonEvent()
        {
            attributeForm.item_useButton.onClick.RemoveAllListeners();
            attributeForm.item_dropButton.onClick.RemoveAllListeners();

            attributeForm.item_useButton.onClick.AddListener(Button_OnUse);
            attributeForm.item_dropButton.onClick.AddListener(Button_OnDrop);
        }
    }
}