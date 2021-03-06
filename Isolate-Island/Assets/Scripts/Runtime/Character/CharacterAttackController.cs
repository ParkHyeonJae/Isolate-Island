using IsolateIsland.Runtime.Event;
using IsolateIsland.Runtime.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Character
{
    public class CharacterAttackController : MonoBehaviour
    {
        CharacterDressablePartsSetter _weaponPartsSetter;
        public CharacterDressablePartsSetter weaponPartsSetter
            => _weaponPartsSetter = _weaponPartsSetter ?? Managers.Managers.Instance.Inventory.Dressable
                .GetParts<CharacterDressablePartsSetter>
                (Stat.EParts.PARTS_LEFT_HAND);
        
        public WeaponItem weaponParts
            =>  Managers.Managers.Instance.Inventory.Dressable
                .GetParts(Stat.EParts.PARTS_LEFT_HAND) as WeaponItem;

        private SpriteRenderer _weaponRenderer;
        public SpriteRenderer weaponRenderer
            => _weaponRenderer = _weaponRenderer ?? weaponPartsSetter.GetComponent<SpriteRenderer>();

        
        private BoxCollider2D _weaponCollider;
        public BoxCollider2D weaponCollider
            => _weaponCollider = _weaponCollider ?? weaponPartsSetter.GetComponent<BoxCollider2D>();

        private BoxCollider2D _dressableWeaponCollider;
        public BoxCollider2D dressableWeaponCollider
        {
            get
            {
                if (_dressableWeaponCollider is null)
                    _dressableWeaponCollider = Managers.Managers
                        .Instance.Inventory.Dressable.GetParts(Stat.EParts.PARTS_LEFT_HAND)?.GetComponent<BoxCollider2D>();

                if (_dressableWeaponCollider is null)
                {
                    _dressableWeaponCollider = Managers.Managers
                        .Instance.Inventory.Dressable.GetParts<CharacterDressablePartsSetter>(Stat.EParts.PARTS_LEFT_HAND)?.GetComponent<BoxCollider2D>();
                    _dressableWeaponCollider.size = Vector2.one;
                }
                return _dressableWeaponCollider;
            }
        }

        private CharacterInteractSimulator characterInteractSimulator;


        private Rigidbody2D _rigidBody;
        public Rigidbody2D rigidBody
            => _rigidBody = _rigidBody ?? GetComponent<Rigidbody2D>();


        private CharacterAnimController _characterAnimController;
        public CharacterAnimController characterAnimController
            => _characterAnimController = _characterAnimController ?? GetComponent<CharacterAnimController>();

        private void Awake()
        {
            weaponCollider.enabled = false;
            characterInteractSimulator = new CharacterInteractSimulator(characterAnimController.animator);
        }

        // Invoke by Animation Event Trigger
        public void OnEnterAttack()
        {
            Debug.Log("Enter Attack");
            
            var renderer = weaponRenderer;
            var collider = weaponCollider;
            collider.size = dressableWeaponCollider.size;
            collider.enabled = true;

            if (weaponParts is null)
                Managers.Managers.Instance.Sound.PlayOneShot("플레이어 손 공격");

            if (renderer.sprite is null)
                return;

            var sprite = renderer.sprite;

            //rigidBody.AddForce(characterAnimController.MoveNormalDir * 20f, ForceMode2D.Impulse);


            weaponParts?.AttackAnimationFactory(this, Utils.Defines.EAttackAnimationKeyState.OnEnter);
        }

        // Invoke by Animation Event Trigger
        public void OnExitAttack()
        {
            Debug.Log("Exit Attack");

            var collider = weaponCollider;

            collider.enabled = false;

            //rigidBody.velocity = Vector2.zero;

            Managers.Managers.Instance.Event.GetListener<OnAttackAnimationEvent>()
                ?.Invoke(this, Utils.Defines.EAttackAnimationKeyState.OnExit);

            weaponParts?.AttackAnimationFactory(this, Utils.Defines.EAttackAnimationKeyState.OnExit);
        }
    }
}