using IsolateIsland.Runtime.Character;
using IsolateIsland.Runtime.Event;
using System;
using System.Linq;
using UnityEngine;

namespace IsolateIsland.Runtime.Inventory
{
    // 장비 전용 인벤토리
    public sealed class DressableInventory : Inventory
    {
        private UI_InventoryAttributeConfigurator inventoryAttributeConfigurator;
        internal UI_InventoryAttributeConfigurator InventoryAttributeConfigurator
            => inventoryAttributeConfigurator = inventoryAttributeConfigurator ?? Managers.Managers.Instance.DI.Get<UI_InventoryAttributeConfigurator>();

        private UI_InventoryAttributeConfigurator.DressableAttributeForm _form;
        private Stat.DressableStat _stat;

        private void SetPartsToAttribute(Stat.EParts parts, DressableItem dressableItem, Action<UI_InventoryAttributeSetter> action)
        {
            switch (parts)
            {
                case Stat.EParts.PARTS_NONE:
                    Debug.LogWarning($"The Parts Has NULL : {dressableItem.name}");
                    return;
                case Stat.EParts.PARTS_HEAD:
                    action?.Invoke(_form.ui_Head);
                    return;
                case Stat.EParts.PARTS_LEFT_HAND:
                    action?.Invoke(_form.ui_Weapon);
                    return;
                case Stat.EParts.PARTS_RIGHT_HAND:
                    action?.Invoke(_form.ui_Item);
                    return;
                case Stat.EParts.PARTS_BODY:
                    action?.Invoke(_form.ui_Body);
                    return;
                case Stat.EParts.PARTS_LEG:
                    action?.Invoke(_form.ui_Leg);
                    return;
            }
        }

        public DressableItem GetParts(Stat.EParts eParts)
             => Items.Keys.OfType<DressableItem>().FirstOrDefault((item)
                 => item.DressableCombinationNode.DressableStat.DRESSABLE_Parts == eParts);


        public T GetParts<T>(Stat.EParts eParts) where T : CharacterDressablePartsSetter
        {
            var _setters = Managers.Managers.Instance.DI.Gets<T>();

            var parts = _setters.Where(e => e.Parts == eParts).Single();

            return parts;
        }

        protected override void OnObtainItem(ItemBase @base)
        {
            var dressableItem = @base as DressableItem;

            if (dressableItem is null)
                return;

            _form = InventoryAttributeConfigurator._dressableAttrForm;

            _stat = dressableItem.DressableCombinationNode.DressableStat;

            var userStat = Managers.Managers.Instance.statManager.UserStat;
            _stat.ApplyDressable(ref userStat);
            Managers.Managers.Instance.statManager.UserStat = userStat;

           var parts = _stat.DRESSABLE_Parts;

            SetPartsToAttribute(parts, dressableItem, (setter) => {
                setter.SetAttribute(dressableItem, InventoryAttributeConfigurator.OnSelectAttribute);
            });

            Managers.Managers.Instance.Event.GetListener<DressableEventListener>()
                .Invoke(Utils.Defines.EDressableState.Use, dressableItem);

            Managers.Managers.Instance.Event.GetListener<Event.OnUIUpdateEvent>()?.Invoke();
        }

        protected override void OnCountingItem(ItemBase @base)
        {

            Managers.Managers.Instance.Event.GetListener<Event.OnUIUpdateEvent>()?.Invoke();
        }

        protected override void OnSubtractItem(ItemBase @base)
        {
            var dressableItem = @base as DressableItem;

            if (dressableItem is null)
                return;

            _form = InventoryAttributeConfigurator._dressableAttrForm;

            _stat = dressableItem.DressableCombinationNode.DressableStat;

            var userStat = Managers.Managers.Instance.statManager.UserStat;
            _stat.DeApplyDressable(ref userStat);
            Managers.Managers.Instance.statManager.UserStat = userStat;

            var parts = _stat.DRESSABLE_Parts;

            SetPartsToAttribute(parts, dressableItem, (setter) => {
                setter.OnReset();
            });

            Managers.Managers.Instance.Event.GetListener<DressableEventListener>()
                .Invoke(Utils.Defines.EDressableState.Drop, dressableItem);

            Managers.Managers.Instance.Event.GetListener<Event.OnUIUpdateEvent>()?.Invoke();
        }

        protected override void OnProductItem(ItemBase @base)
        {

            Managers.Managers.Instance.Event.GetListener<Event.OnUIUpdateEvent>()?.Invoke();
        }

    }
}