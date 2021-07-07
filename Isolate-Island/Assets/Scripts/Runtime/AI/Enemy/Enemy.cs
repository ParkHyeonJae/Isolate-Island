using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.BT;

namespace IsolateIsland.Runtime.Ai
{
    public class Enemy : EnemyAI
    {
        #region Root
        protected BTCondition sleeping = new BTCondition();
        #endregion

        [SerializeField] private GameObject _weapon;

        private BoxCollider2D _collider = null;
        public BoxCollider2D collider => _collider ?? _weapon.GetComponent<BoxCollider2D>();

        float sleepCount;
        float deltaTime;
        bool isSleeping;

        protected override void Init()
        {
            base.Init();

            behavior.AddChild(sleeping);

            sleeping.action = Sleeping;

            sleepCount = 0;
            deltaTime = 0;
            isSleeping = false;
        }

        public override void ReduceHp(int value)
        {
            base.ReduceHp(value);

            sleepCount = 0;
            deltaTime = 0;
        }

        protected override bool Attack()
        {
            if (base.Attack())
            {
                animator.SetTrigger("isAttack");
                
                sleepCount = 0;
                deltaTime = 0;
            }

            return base.Attack();
        }

        protected override bool Move()
        {
            if (!base.Move())
                return false;

            animator.SetBool("isMove", base.Move());

            if (Managers.Managers.Instance.GameManager.isDay == false)
            {
                sleepCount += Time.time - deltaTime;
                deltaTime = Time.time;
            }

            return base.Move();
        }

        protected override bool Track()
        {
            if (!base.Track())
                return false;

            animator.SetBool("isMove", base.Track());

            sleepCount = 0;
            deltaTime = 0;

            return base.Track();
        }

        protected bool Sleeping()
        {
            if (Managers.Managers.Instance.GameManager.isDay)
            {
                sleepCount = 0;
                deltaTime = 0;
                isSleeping = false;
            }

            if (sleepCount >= enemyData.sleepingTime)
            {
                isSleeping = true;
                animator.SetBool("isSleep", true);
                return true;
            }
            else if (sleepCount >= enemyData.sleepingCooltime && isSleeping)
            {
                animator.SetBool("isSleep", true);
                return true;
            }

            animator.SetBool("isSleep", false);
            return false;
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