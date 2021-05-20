namespace IsolateIsland.Runtime.Inventory
{
    public class DressableInventory : Inventory
    {

        public enum EParts
        {

        }

        public override void AppendItem(ItemBase item)
        {
            base.AppendItem(item);

            Managers.Managers.Instance.inventoryManager.AddItem(item);
        }

    }
}