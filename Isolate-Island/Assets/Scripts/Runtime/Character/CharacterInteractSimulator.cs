using IsolateIsland.Runtime.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Character
{
    public class CharacterInteractSimulator
    {
        private Animator _animator;
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
                switch (isInteractable)
                {
                    // Interact
                    case true when entity is InteractableEntity:
                        Managers.Managers.Instance.Input.OnMouseAction -= Input_OnAttackMouseAction;
                        Managers.Managers.Instance.Input.OnMouseAction += (evt) => Input_OnInteractMouseAction(evt, entity);
                        Managers.Managers.Instance.Event.GetListener<Event.OnInteractCasterEvent>()?.Invoke(entity);
                        break;

                    // Attack
                    case false:
                        Managers.Managers.Instance.Input.OnMouseAction += Input_OnAttackMouseAction;
                        Managers.Managers.Instance.Input.OnMouseAction -= (evt) => Input_OnInteractMouseAction(evt, entity);
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

        private void Input_OnInteractMouseAction(Utils.Defines.MouseEvent evt, Entity entity)
        {
            //Debug.Log("Interact !");
        }

        public void Simulate()
        {

        }


    }
}