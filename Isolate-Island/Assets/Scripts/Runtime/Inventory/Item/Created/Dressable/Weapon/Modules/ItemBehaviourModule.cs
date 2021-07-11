using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Inventory
{
    public class ItemBehaviourModule : ItemModule
    {
        public override void Do()
        {
            ItemInvoker._isCollisionCheck = false;
            BoxCollider2D.isTrigger = true;
        }
    }
}