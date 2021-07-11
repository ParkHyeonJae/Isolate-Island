using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace IsolateIsland.Runtime.Inventory
{
    public class ThrowWeaponItem : WeaponItem
    {
        protected override void Initalize()
        {
            base.Initalize();
        }
        public override string GetAttackAnimKey()
        {
            return Utils.Defines.AnimationKeys.DefaultAttackAnimationKey;
        }
    }
}