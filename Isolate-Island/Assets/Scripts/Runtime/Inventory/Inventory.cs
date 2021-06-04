using UnityEngine;

namespace IsolateIsland.Runtime.Inventory
{
    public class Inventory : MonoBehaviour
    {
        public virtual void AppendItem(ItemBase item)
        {
            Managers.Managers.Instance.Inventory.AddItem(item);
        }

        [ContextMenu("TryInqury")]
        public void Inquiry()
        {
            Managers.Managers.Instance.Inventory.InquiryProductiveItem();
        }

        [ContextMenu("Product")]
        public void Product()
        {
            Managers.Managers.Instance.Inventory.TryInquiryProductiveItem();
        }




        [ContextMenu("PrintItemList")]
        public void PrintItemList()
        {
            Debug.Log(Managers.Managers.Instance.Inventory.ToString());
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