using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Ai
{
    public class Enemy : EnemyAI
    {
        [SerializeField] private GameObject _weapon;

        private BoxCollider2D _collider = null;
        public BoxCollider2D collider => _collider ?? _weapon.GetComponent<BoxCollider2D>();

        protected override bool Attack()
        {
            if (base.Attack())
            {
                animator.SetTrigger("isAttack");
            }

            return base.Attack();
        }

        protected override bool Move()
        {
            animator.SetBool("isMove", base.Move());

            return base.Move();
        }

        protected override bool Track()
        {
            animator.SetBool("isMove", base.Track());

            return base.Track();
        }

        // Invoke by Animation Event Trigger
        public void OnEnterAttack()
        {
            Debug.Log("Enter Attack");

            collider.enabled = true;
        }

        // Invoke by Animation Event Trigger
        public void OnExitAttack()
        {
            Debug.Log("Exit Attack");

            collider.enabled = false;
        }

    }
}