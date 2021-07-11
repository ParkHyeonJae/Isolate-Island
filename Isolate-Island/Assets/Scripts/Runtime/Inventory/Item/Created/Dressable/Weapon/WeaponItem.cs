using IsolateIsland.Runtime.Character;
using IsolateIsland.Runtime.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Inventory
{
    public class WeaponItem : DressableItem
    {
        public virtual string GetAttackAnimKey()
        {
            return Utils.Defines.AnimationKeys.DefaultAttackAnimationKey;
        }
        //private string _attackAnimKey { get; set; } = Utils.Defines.AnimationKeys.DefaultAttackAnimationKey;

        protected CharacterAttackController characterAttackController = null;

        protected override void Initalize()
        {
            base.Initalize();

            //Managers.Managers.Instance.Event.GetListener<OnAttackAnimationEvent>()?.Subscribe(AttackAnimationFactory);
        }

        public void AttackAnimationFactory(CharacterAttackController characterAttackController, Utils.Defines.EAttackAnimationKeyState state)
        {
            this.characterAttackController = characterAttackController;
            switch (state)
            {
                case Utils.Defines.EAttackAnimationKeyState.OnEnter:
                    OnEnterAttack();
                    break;
                case Utils.Defines.EAttackAnimationKeyState.OnExit:
                    OnExitAttack();
                    break;
                default:
                    break;
            }
        }

        protected virtual void OnEnterAttack()
        {

        }


        protected virtual void OnExitAttack()
        {

        }
    }
}