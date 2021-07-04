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

        private SpriteRenderer _weaponRenderer;
        public SpriteRenderer weaponRenderer
            => _weaponRenderer = _weaponRenderer ?? weaponPartsSetter.GetComponent<SpriteRenderer>();

        
        private BoxCollider2D _weaponCollider;
        public BoxCollider2D weaponCollider
            => _weaponCollider = _weaponCollider ?? weaponPartsSetter.GetComponent<BoxCollider2D>();



        private void Awake()
        {
            weaponCollider.enabled = false;
        }

        // Invoke by Animation Event Trigger
        public void OnEnterAttack()
        {
            Debug.Log("Enter Attack");

            var renderer = weaponRenderer;
            var collider = weaponCollider;

            if (renderer.sprite is null)
                return;

            var sprite = renderer.sprite;
            collider.size = new Vector2(1f, 1f);
            collider.enabled = true;
        }

        // Invoke by Animation Event Trigger
        public void OnExitAttack()
        {
            Debug.Log("Exit Attack");

            var collider = weaponCollider;

            collider.enabled = false;

        }
    }
}