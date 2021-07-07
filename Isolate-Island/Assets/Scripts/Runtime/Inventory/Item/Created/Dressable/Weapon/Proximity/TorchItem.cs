using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Inventory
{
    public class TorchItem : ProximityWeaponItem
    {
        protected override void Initalize()
        {
            base.Initalize();

            GetAttackAnimKey = Utils.Defines.AnimationKeys.DefaultAttackAnimationKey;
        }
    }
}

