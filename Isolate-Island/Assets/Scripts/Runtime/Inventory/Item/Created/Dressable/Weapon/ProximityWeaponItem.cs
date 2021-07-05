using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Inventory
{
    public class ProximityWeaponItem : WeaponItem
    {
        protected override void Initalize()
        {
            base.Initalize();

            GetAttackAnimKey = "Attack";
        }
    }
}