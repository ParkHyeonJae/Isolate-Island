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

        public override string GetAttackAnimKey()
        {
            var itemSlot = Managers.Managers.Instance.Inventory.Dressable.GetParts(Stat.EParts.PARTS_RIGHT_HAND);

            if (itemSlot is null || !(itemSlot is SubArrowItem))
                return Utils.Defines.AnimationKeys.DefaultAttackAnimationKey;
            return Utils.Defines.AnimationKeys.RangedAttackAnimationKey;
        }

        protected override void Initalize()
        {
            base.Initalize();
        }


        protected override void OnEnterAttack()
        {
            var itemSlot = Managers.Managers.Instance.Inventory.Dressable.GetParts(Stat.EParts.PARTS_RIGHT_HAND);

            if (itemSlot is null || !(itemSlot is SubArrowItem))
            {
                Managers.Managers.Instance.Sound.PlayOneShot("플레이어 활 공격");
                return;
            }

            Managers.Managers.Instance.Inventory.Dressable.OnCountDownItem(itemSlot);
            Managers.Managers.Instance.Event.GetListener<Event.OnUIUpdateEvent>()?.Invoke();

            var arrowItem = itemSlot as SubArrowItem;
            var arrow = Managers.Managers.Instance.Pool.Instantiate(arrowItem.CombinationNode.name);

            var moveTo = arrow.GetOrAddComponent<ItemBehaviourModule_Arrow>();
            var characterPos = characterAttackController.transform.position;
            var mousePos = Managers.Managers.Instance.Camera.defaultCamera.ScreenToWorldPoint(Input.mousePosition);
            if (moveTo is null)
                return;
            moveTo.SetStartPosition(characterPos);
            moveTo.SetDestinationPosition(mousePos);
            moveTo.SetRange(Managers.Managers.Instance.statManager.UserStat.RANGE);
            moveTo.Do();

            Managers.Managers.Instance.Sound.PlayOneShot("플레이어 화살 공격");
        }

        protected override void OnExitAttack()
        {

        }

    }
}