using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Inventory
{
    public class WeaponItem : DressableItem
    {
        public string GetAttackAnimKey { get; protected set; } = "Attack";

        protected override void Initalize()
        {
            base.Initalize();
        }
    }
}