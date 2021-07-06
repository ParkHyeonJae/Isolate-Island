using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Ai
{
    public class Enemy : EnemyAI
    {
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
    }
}