using IsolateIsland.Runtime.Character;
using IsolateIsland.Runtime.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Inventory
{
    /// <summary>
    /// 원거리 무기(장궁, 강궁)의 베이스 스크립트
    /// </summary>
    public class RangedWeaponItem : WeaponItem
    {
        protected override void Initalize()
        {
            base.Initalize();

            GetAttackAnimKey = Utils.Defines.AnimationKeys.RangedAttackAnimationKey;
        }


        protected override void OnEnterAttack()
        {
            var itemSlot = Managers.Managers.Instance.Inventory.Dressable.GetParts(Stat.EParts.PARTS_RIGHT_HAND);


            if (!(itemSlot is SubArrowItem))
                return;

            Managers.Managers.Instance.Inventory.Dressable.SubtractItem(itemSlot);
            Managers.Managers.Instance.Event.GetListener<Event.OnUIUpdateEvent>()?.Invoke();

            var arrowItem = itemSlot as SubArrowItem;
            var arrow = Managers.Managers.Instance.Pool.Instantiate(arrowItem.name);

            var moveTo = arrow.GetOrAddComponent<ItemBehaviourModule_Arrow>();
            var characterPos = characterAttackController.transform.position;
            var mousePos = Managers.Managers.Instance.Camera.defaultCamera.ScreenToWorldPoint(Input.mousePosition);
            if (moveTo is null)
                return;
            moveTo.SetStartPosition(characterPos);
            moveTo.SetDestinationPosition(mousePos);
            moveTo.SetRange(Managers.Managers.Instance.statManager.UserStat.RANGE);
            moveTo.Do();
        }

        protected override void OnExitAttack()
        {

        }

    }
}