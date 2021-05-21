using UnityEngine;

namespace IsolateIsland.Runtime.Inventory
{
    public class Inventory : MonoBehaviour
    {
        public virtual void AppendItem(ItemBase item)
        {
            Managers.Managers.Instance.inventoryManager.AddItem(item);
        }

        [ContextMenu("TryInqury")]
        public void Inquiry()
        {
            Managers.Managers.Instance.inventoryManager.InquiryProductiveItem();
        }


        [ContextMenu("PrintItemList")]
        public void PrintItemList()
        {
            Debug.Log(Managers.Managers.Instance.inventoryManager.ToString());
        }
    }
}