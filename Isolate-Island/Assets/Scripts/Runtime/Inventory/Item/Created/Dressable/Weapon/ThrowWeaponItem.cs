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


        protected override void OnEnterAttack()
        {

            var itemSlot = Managers.Managers.Instance.Inventory.Dressable.GetParts(Stat.EParts.PARTS_LEFT_HAND);

            if (itemSlot is null || !(itemSlot is ThrowWeaponItem))
                return;

            var count = Managers.Managers.Instance.Inventory.Dressable.GetItemCount(itemSlot);
            Managers.Managers.Instance.Inventory.Dressable.OnCountDownItem(itemSlot);
            Managers.Managers.Instance.Event.GetListener<Event.OnUIUpdateEvent>()?.Invoke();

            var throwItem = itemSlot as ThrowWeaponItem;
            var _throw = Managers.Managers.Instance.Pool.Instantiate(throwItem.CombinationNode.name);

            var moveTo = _throw.GetOrAddComponent<ItemBehaviourModule_MolotovCocktail>();
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