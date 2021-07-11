using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Inventory
{
    /// <summary>
    /// 근접 무기(Proximity)의 베이스 스크립트
    /// </summary>
    public class ProximityWeaponItem : WeaponItem
    {
        protected override void Initalize()
        {
            base.Initalize();

            GetAttackAnimKey = Utils.Defines.AnimationKeys.DefaultAttackAnimationKey;
        }
    }
}