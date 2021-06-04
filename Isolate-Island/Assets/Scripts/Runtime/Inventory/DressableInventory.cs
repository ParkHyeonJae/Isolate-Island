namespace IsolateIsland.Runtime.Inventory
{
    public class DressableInventory : Inventory
    {
        public override void AppendItem(ItemBase item)
        {
            base.AppendItem(item);

            Managers.Managers.Instance.Inventory.AddItem(item);
        }

    }
}