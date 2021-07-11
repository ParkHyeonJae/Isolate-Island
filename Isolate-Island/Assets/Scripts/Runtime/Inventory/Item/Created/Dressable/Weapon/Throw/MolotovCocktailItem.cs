using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace IsolateIsland.Runtime.Inventory
{
    public class MolotovCocktailItem : ThrowWeaponItem
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

            Managers.Managers.Instance.Sound.PlayOneShot("플레이어 둔기 공격");
        }

        protected override void OnExitAttack()
        {
            base.OnExitAttack();
        }
    }
}