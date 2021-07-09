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

        private DressableItem _added_dressableItem = null;

        private void Awake()
        {
            Managers.Managers.Instance.Event.GetListener<Event.OnClickConfigButtonEventListener>().Subscribe(OnInitQuickSlot);
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
            if (itemParts is null)
            {
                _slotImage.sprite = null;
                _slotText.text = "X 00";
                return;
            }

            _added_dressableItem = itemParts;

            var itemCount = Managers.Managers.Instance.Inventory.Dressable.GetItemCount(itemParts);
            _slotImage.sprite = itemParts.DressableCombinationNode.sprite;
            _slotText.text = $"X {itemCount.ToString()}";
        }
        public void OnUse()
        {
            if (_added_dressableItem is null)
                return;
            var ap = new DressableItemApplyer();
            ap.Use(_added_dressableItem);
            Managers.Managers.Instance.Inventory.Dressable.SubtractItem(_added_dressableItem);
            RenewQuickSlotUI();
        }
    }
}