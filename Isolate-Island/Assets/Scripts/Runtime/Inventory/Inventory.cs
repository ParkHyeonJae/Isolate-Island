using UnityEngine;

namespace IsolateIsland.Runtime.Inventory
{
    public class Inventory : MonoBehaviour
    {
        public virtual void AppendItem(ItemBase item)
        {
            Managers.Managers.Instance.inventoryManager.AddItem(item);
        }
    }
}