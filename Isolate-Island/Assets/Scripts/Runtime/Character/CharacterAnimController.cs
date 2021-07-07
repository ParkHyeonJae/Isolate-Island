using UnityEngine;
using IsolateIsland.Runtime.Event;

namespace IsolateIsland.Runtime.Character
{
    public class CharacterAnimController : CharacterController
    {
        [SerializeField] private bool isFliping = false;

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
            Managers.Managers.Instance.Event.GetListener<OnGameoverEvent>().Subscribe(() => Dead());
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
        }

        public override void Move()
        {
            base.Move();
            if (xAxis == 0 && yAxis == 0)
            {
                Animator.SetBool("isRun", false);
                return;
            }

            var flipX = ((isFliping) ? (xAxis > 0) : (xAxis < 0)) ? 0f : -180f;
            transform.rotation = Quaternion.Euler(0, flipX, 0);

            Animator.SetBool("isRun", true);

        }

        public void Dead()
        {
            animator.Play("Dead");
            Managers.Managers.Instance.statManager.UserStat.MoveSpeed = 0;
        }
    }
}