using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.TestCodes
{
    public class ObjectManagerTest : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            //Managers.Managers.Instance.DI.Get(Defines.Load_Object.Player).SetActive(false);

            var inventory = Managers.Managers.Instance.DI.Get<IsolateIsland.Runtime.Inventory.Inventory>();
            inventory.PrintItemList();

            //Managers.Managers.Instance.DI.Get<IsolateIsland.Runtime.Inventory.Inventory>().PrintItemList();
            //Managers.Managers.Instance.DI.Get<IsolateIsland.Runtime.Inventory.Inventory>().PrintItemList();
            //Managers.Managers.Instance.DI.Get<IsolateIsland.Runtime.Inventory.Inventory>().PrintItemList();
        }
    }
}