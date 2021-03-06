using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Character
{
    public class CharacterAnimController : CharacterController
    {
        [SerializeField] private Animator animator;
        public Animator Animator
        {
            get
            {
                if (animator == null)
                    animator = GetComponent<Animator>();

                return animator;
            }
        }

        public override void Init()
        {
            base.Init();

        }

        public override void Move()
        {
            base.Move();
            if (xAxis == 0 && zAxis == 0)
            {
                Animator.SetBool("isRun", false);
                return;
            }

            var flipX = (xAxis > 0) ? 0f : -180f;
            transform.rotation = Quaternion.Euler(0, flipX, 0);

            Animator.SetBool("isRun", true);

        }
    }
}