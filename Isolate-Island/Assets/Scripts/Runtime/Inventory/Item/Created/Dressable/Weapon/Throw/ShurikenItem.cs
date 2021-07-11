using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace IsolateIsland.Runtime.Inventory
{
    public class ShurikenItem : ThrowWeaponItem
    {
        protected override void Initalize()
        {
            base.Initalize();
        }
        public override string GetAttackAnimKey()
        {
            return Utils.Defines.AnimationKeys.DefaultAttackAnimationKey;
        }


        protected override void OnEnterAttack()
        {
            base.OnEnterAttack();

            Managers.Managers.Instance.Sound.PlayOneShot("플레이어 표창 공격");
        }

        protected override void OnExitAttack()
        {
            base.OnExitAttack();
        }
    }
}