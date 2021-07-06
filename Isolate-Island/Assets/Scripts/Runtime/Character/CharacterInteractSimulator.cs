using IsolateIsland.Runtime.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Character
{
    public class CharacterInteractSimulator
    {
        private Animator _animator;
        private Entity _entity;
        public CharacterInteractSimulator(Animator animator)
        {
            _animator = animator;

            Init();
        }

        public void Init()
        {
            Managers.Managers.Instance.Input.OnMouseAction += Input_OnAttackMouseAction;
            Managers.Managers.Instance.Event.GetListener<Event.OnDetectCasterEvent>().Subscribe((entity, isInteractable) =>
            {
                _entity = entity;
                switch (isInteractable)
                {
                    // Interact
                    case true when entity is InteractableEntity:
                        //Managers.Managers.Instance.Input.OnMouseAction += Input_OnAttackMouseAction;
                        Managers.Managers.Instance.Input.OnMouseAction += Input_OnInteractMouseAction;
                        break;

                    // Attack
                    case false:
                        //Managers.Managers.Instance.Input.OnMouseAction += Input_OnAttackMouseAction;
                        Managers.Managers.Instance.Input.OnMouseAction -= Input_OnInteractMouseAction;
                        break;
                }
            });
        }

        private void Input_OnAttackMouseAction(Utils.Defines.MouseEvent evt)
        {
            var item            = Managers.Managers.Instance.Inventory.Dressable.GetParts(Stat.EParts.PARTS_LEFT_HAND);
            var weaponItem      = item as WeaponItem;
            string key = string.Empty;

            if (weaponItem is null) key = Utils.Defines.AnimationKeys.DefaultAttackAnimationKey;
            else key = weaponItem.GetAttackAnimKey;
            _animator.Play(key);
        }

        private void Input_OnInteractMouseAction(Utils.Defines.MouseEvent evt)
        {
            //Debug.Log("Interact !");
            if (evt == Utils.Defines.MouseEvent.Click)
            {
                Managers.Managers.Instance.Event.GetListener<Event.OnInteractCasterEvent>()?.Invoke(_entity);
            }
        }

        public void Simulate()
        {

        }


    }
}