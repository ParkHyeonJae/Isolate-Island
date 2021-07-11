using IsolateIsland.Runtime.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Inventory
{
    public class BluntItem : ProximityWeaponItem
    {
        public override string GetAttackAnimKey()
        {
            return Utils.Defines.AnimationKeys.DefaultAttackAnimationKey;
        }

        protected override void Initalize()
        {
            base.Initalize();
        }


        public override void OnEnterDressable()
        {
            base.OnEnterDressable();
            var user = Managers.Managers.Instance.DI.Get(Utils.Defines.Load_Object.User);

            if (user is null)
                return;
        }


        public override void OnExitDressable()
        {
            base.OnExitDressable();
        }

        protected override void OnEnterAttack()
        {
            base.OnEnterAttack();
            Managers.Managers.Instance.Sound.PlayOneShot("플레이어 둔기 공격");
        }
    }
}

