using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Ai
{
    public class Pig : AnimalAI
    {
        public override void ReduceHp(int value)
        {
            base.ReduceHp(value);
            animator.SetTrigger("isHit");
        }

        protected override bool Move()
        {
            animator.SetBool("isMove", base.Move());

            if (Managers.Managers.Instance.GameManager.isDay == false)
            {
                sleepCount += Time.time - deltaTime;
                deltaTime = Time.time;
            }

            return base.Move();
        }

        protected override bool RunMove()
        {
            animator.SetBool("isRun", base.RunMove());

            if (Managers.Managers.Instance.GameManager.isDay == false)
            {
                sleepCount += Time.time - deltaTime;
                deltaTime = Time.time;
            }

            return base.RunMove();
        }
    }
}