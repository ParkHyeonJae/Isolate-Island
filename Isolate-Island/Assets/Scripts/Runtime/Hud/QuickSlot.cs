using IsolateIsland.Runtime.Inventory;
using IsolateIsland.Runtime.Stat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IsolateIsland.Runtime.Hud
{
    public class QuickSlot : MonoBehaviour
    {
        [Header("Quick Slot")]
        [SerializeField] private Image _slotImage;
        [SerializeField] private Text _slotText;
        [SerializeField] private Sprite _emptyImage;

        private DressableItem _added_dressableItem = null;

        private void Awake()
        {
            Managers.Managers.Instance.Event.GetListener<Event.OnClickConfigButtonEventListener>().Subscribe(OnInitQuickSlot);
            Managers.Managers.Instance.Event.GetListener<Event.OnUIUpdateEvent>().Subscribe(RenewQuickSlotUI);
            Managers.Managers.Instance.Event.GetListener<Event.OnCollectItemEvent>().Subscribe(() 
                => Managers.Managers.Instance.Util.AddTimer(0.5f, () => RenewQuickSlotUI()));
        }
        public void OnInitQuickSlot(Utils.Defines.EDressableState eDressableState)
        {
            RenewQuickSlotUI();
        }


        public void RenewQuickSlotUI()
        {
            var itemParts = Managers.Managers.Instance.Inventory.Dressable.GetParts(EParts.PARTS_RIGHT_HAND);
            var itemParts2 = Managers.Managers.Instance.Inventory.Dressable.GetParts(EParts.PARTS_LEFT_HAND) as ThrowWeaponItem;
            if (itemParts is null && itemParts2 is null)
            {
                _slotImage.sprite = _emptyImage;
                _slotText.text = "";
                return;
            }

            if (itemParts != null && itemParts2 == null)
                _added_dressableItem = itemParts;
            if(itemParts == null && itemParts2 != null)
                _added_dressableItem = itemParts2;
            if (itemParts != null && itemParts2 != null)
                _added_dressableItem = itemParts2;

            var itemCount = Managers.Managers.Instance.Inventory.Dressable.GetItemCount(_added_dressableItem);
            _slotImage.sprite = _added_dressableItem.DressableCombinationNode.sprite;
            _slotText.text = $"X {itemCount.ToString()}";

        }
        public void OnUse()
        {
            if (_added_dressableItem is null)
                return;
            if (_added_dressableItem is SubArrowItem)
                return;
            if (_added_dressableItem is ThrowWeaponItem)
                return;

            var ap = new DressableItemApplyer();
            ap.Use(_added_dressableItem);
            Managers.Managers.Instance.Inventory.Dressable.SubtractItem(_added_dressableItem);
            RenewQuickSlotUI();
        }
    }
}