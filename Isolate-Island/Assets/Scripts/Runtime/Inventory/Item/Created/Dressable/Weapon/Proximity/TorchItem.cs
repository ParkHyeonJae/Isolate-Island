using IsolateIsland.Runtime.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Inventory
{
    public class TorchItem : ProximityWeaponItem
    {
        GameObject _lightObject;
        protected override void Initalize()
        {
            base.Initalize();

            GetAttackAnimKey = Utils.Defines.AnimationKeys.DefaultAttackAnimationKey;
        }


        public override void OnEnterDressable()
        {
            base.OnEnterDressable();
            var user = Managers.Managers.Instance.DI.Get(Utils.Defines.Load_Object.User);

            if (user is null)
                return;


            //#region TEST
            //_lightObject = Managers.Managers.Instance.Pool.Instantiate("횟불_라이트");

            //_lightObject.transform.SetParent(user.transform);
            //_lightObject.transform.localPosition = new Vector3(0, -3.3f, 0);
            //#endregion
        }


        public override void OnExitDressable()
        {
            base.OnExitDressable();

            Managers.Managers.Instance.Pool.Destroy(_lightObject);

            //var parts = Managers.Managers.Instance.Inventory.Dressable.GetParts<CharacterDressablePartsSetter>(Stat.EParts.PARTS_LEFT_HAND);
            //_lightObject.transform.SetParent(parts.transform);
        }
    }
}

