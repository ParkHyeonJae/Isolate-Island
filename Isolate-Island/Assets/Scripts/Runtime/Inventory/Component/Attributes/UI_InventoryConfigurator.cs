using IsolateIsland.Runtime.Event;
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

            internal Vector3 startPos_use;
            internal Vector3 startPos_drop;

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

        private void Awake()
        {
            Managers.Managers.Instance.Event.GetListener<OnCollectItemEvent>().Subscribe(SetAttribute);
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
                if (i < setter.Length)
                {
                    var attrIdx = setter[i];
                    var table = itemList[i].Key;

                    attrIdx.SetAttribute(table, OnSelectAttribute);
                }
            }

        }

        internal IStatAble GetTypeToReturnStatAbleFactory(ItemBase item)
        {
            switch (item)
            {
                case StatItem statItem:
                    return statItem.StatCombinationNode;
                case DressableItem dressableItem:
                    return dressableItem.DressableCombinationNode;
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
                case DressableItem i:
                    if (i.DressableCombinationNode.Stat.IsConsumable)
                    {
                        use = stat_use;
                        drop = stat_drop;
                    }
                    else
                    {
                        use = dressable_use;
                        drop = dressable_drop;
                    }
                    break;
                default:
                    use = stat_use;
                    drop = stat_drop;
                    break;
            }
        }
        internal enum ItemPlace
        {
            DressableSlot,
            GameSlot
        }

        internal ItemPlace GetItemPlace(ItemBase item)
        {
            if (!(item is DressableItem))
                return ItemPlace.GameSlot;

            var dressableCheck = Managers.Managers.Instance.Inventory.Dressable.IsContain(item);
            var gameCheck = Managers.Managers.Instance.Inventory.Game.IsContain(item);

            if (dressableCheck && !gameCheck)
                return ItemPlace.DressableSlot;

            if (!dressableCheck && gameCheck)
                return ItemPlace.GameSlot;

            return ItemPlace.GameSlot;
        }
        bool InitOnce = true;
        internal virtual void SetGUIButton(ItemBase item)
        {
            if (InitOnce)
            {
                attributeForm.startPos_use = attributeForm.obj_use.transform.position;
                attributeForm.startPos_drop = attributeForm.obj_drop.transform.position;
                InitOnce = false;
            }
            switch (item)
            {
                case StatItem _:
                    if (GetItemPlace(item) == ItemPlace.DressableSlot)
                    {
                        attributeForm.obj_use.SetActive(false);
                        attributeForm.obj_drop.SetActive(true);
                    }
                    else
                    {
                        attributeForm.obj_use.SetActive(true);
                        attributeForm.obj_drop.SetActive(true);
                    }

                    attributeForm.obj_use.transform.position = attributeForm.startPos_use;
                    attributeForm.obj_drop.transform.position = attributeForm.startPos_drop;
                    
                    break;
                case DressableItem _:
                    attributeForm.obj_use.SetActive(true);
                    attributeForm.obj_drop.SetActive(true);

                    attributeForm.obj_use.transform.position = attributeForm.startPos_use;
                    attributeForm.obj_drop.transform.position = attributeForm.startPos_drop;
                    break;
                default:
                    attributeForm.obj_use.SetActive(false);
                    attributeForm.obj_drop.SetActive(true);

                    attributeForm.obj_drop.transform.position = attributeForm.obj_use.transform.position;
                    break;
            }
        }

        internal void SetUseText(in string useText)
            => attributeForm.item_useText.text = useText;

        internal void SetDropText(in string dropText)
            => attributeForm.item_dropText.text = dropText;

        internal virtual void OnSelectAttribute(ItemBase item)
        {
            OnInitalizeButtonEvent();

            string use = string.Empty, drop = string.Empty;
            SetGUIText(item, out use, out drop);
            SetGUIButton(item);

            SetUseText(use);
            SetDropText(drop);



            _selectItem = item;

            attributeForm.obj_making.SetActive(true);
            var dressableContainCheck = Managers.Managers.Instance.Inventory.Dressable.IsContain(_selectItem);
            var gameContainCheck = Managers.Managers.Instance.Inventory.Game.IsContain(_selectItem);

            var dressableCount = Managers.Managers.Instance.Inventory.Dressable.GetItemCount(_selectItem);
            var gameContainCount = Managers.Managers.Instance.Inventory.Game.GetItemCount(_selectItem);

            var count = dressableContainCheck
                ? dressableCount
                : gameContainCount;

            if (dressableContainCheck && gameContainCheck)
                count = dressableCount + gameContainCount;
            attributeForm.item_image.sprite = item.CombinationNode.sprite;
            attributeForm.item_nameText.text = item.CombinationNode.name + " +" + count;
            attributeForm.item_descriptionText.text = item.CombinationNode.description;


            var statAble = GetTypeToReturnStatAbleFactory(item);
            attributeForm.item_statText.text = (!(statAble is null)) ? statAble.GetStatInfo() : "";

            Managers.Managers.Instance.Sound.PlayOneShot("아이템 마우스 클릭");
        }

        protected virtual void Button_OnUse()
        {
            var selectItem = _selectItem;

            Use(selectItem);
        }

        public void Use<T>(in T selectItem) where T : ItemBase
        {
            var itemApplyer = GetTypeToReturnApplyerFactory(selectItem);
            itemApplyer.Use(selectItem);
        }
        public void Drop<T>(in T selectItem) where T : ItemBase
        {
            var itemApplyer = GetTypeToReturnApplyerFactory(selectItem);
            itemApplyer.Drop(selectItem);
        }

        protected virtual void Button_OnDrop()
        {
            var selectItem = _selectItem;

            Drop(selectItem);
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

            attributeForm.startPos_use = attributeForm.obj_use.transform.position;
            attributeForm.startPos_drop = attributeForm.obj_drop.transform.position;

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
            attributeForm.item_useButton.onClick.AddListener(() => 
                Managers.Managers.Instance.Event.GetListener<OnClickConfigButtonEventListener>()
                    .Invoke(Defines.EDressableState.Use));
            attributeForm.item_dropButton.onClick.AddListener(Button_OnDrop);
            attributeForm.item_dropButton.onClick.AddListener(() =>
                Managers.Managers.Instance.Event.GetListener<OnClickConfigButtonEventListener>()
                    .Invoke(Defines.EDressableState.Drop));
        }
    }
}