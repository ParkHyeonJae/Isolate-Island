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


        private void Awake()
        {
            Managers.Managers.Instance.Event.GetListener<Event.OnClickConfigButtonEventListener>().Subscribe(OnInitQuickSlot);
        }
        public void OnInitQuickSlot(Utils.Defines.EDressableState eDressableState)
        {
            var itemParts = Managers.Managers.Instance.Inventory.Dressable.GetParts(EParts.PARTS_RIGHT_HAND);
            if (itemParts is null)
                return;

            var itemCount = Managers.Managers.Instance.Inventory.Dressable.GetItemCount(itemParts);
            _slotImage.sprite = itemParts.DressableCombinationNode.sprite;
            _slotText.text = itemCount.ToString();
        }

        public void OnUse()
        {
            //Managers.Managers.Instance.Inventory.Game.SubtractItem()
        }
    }
}