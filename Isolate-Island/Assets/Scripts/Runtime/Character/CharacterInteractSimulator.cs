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
        private BoxCollider2D _interactCasterCollider;
        public BoxCollider2D interactCasterCollider
            => _interactCasterCollider = _interactCasterCollider ?? Managers.Managers.Instance.DI.Get<CharacterInteractCaster>()?.GetComponent<BoxCollider2D>();

        public CharacterInteractSimulator(Animator animator)
        {
            _animator = animator;

            Init();
        }

        public void Init()
        {
            Managers.Managers.Instance.Input.OnMouseAction += Input_OnAttackMouseAction;
            Managers.Managers.Instance.Input.OnMouseAction += Input_OnInteractMouseAction;
            if (interactCasterCollider)
                interactCasterCollider.enabled = false;
            Managers.Managers.Instance.Event.GetListener<Event.OnDetectCasterEvent>().Subscribe((entity, isInteractable) =>
            {
                _entity = entity;

                if (entity is InteractableEntity)
                    Managers.Managers.Instance.Event.GetListener<Event.OnInteractCasterEvent>()?.Invoke(_entity);
            });
        }

        private void Input_OnAttackMouseAction(Utils.Defines.MouseEvent evt)
        {
            var item            = Managers.Managers.Instance.Inventory.Dressable.GetParts(Stat.EParts.PARTS_LEFT_HAND);
            var weaponItem      = item as WeaponItem;
            string key = string.Empty;

            if (weaponItem is null) key = Utils.Defines.AnimationKeys.DefaultAttackAnimationKey;
            else key = weaponItem.GetAttackAnimKey();
            _animator.Play(key);
        }

        private void Input_OnInteractMouseAction(Utils.Defines.MouseEvent evt)
        {
            if (evt == Utils.Defines.MouseEvent.Click)
            {
                interactCasterCollider.enabled = true;
                Managers.Managers.Instance.Util.AddTimer(0.05f, () => interactCasterCollider.enabled = false);
            }
        }

        public void Simulate()
        {

        }


    }
}