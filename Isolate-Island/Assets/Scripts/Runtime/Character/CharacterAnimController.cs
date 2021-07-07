using UnityEngine;

namespace IsolateIsland.Runtime.Character
{
    public class CharacterAnimController : CharacterController
    {
        [SerializeField] private bool isFliping = false;
       
        public override void Init()
        {
            base.Init();
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
                animator.SetBool("isRun", false);
                return;
            }

            var flipX = ((isFliping) ? (xAxis > 0) : (xAxis < 0)) ? 0f : -180f;
            transform.rotation = Quaternion.Euler(0, flipX, 0);

            animator.SetBool("isRun", true);

        }
    }
}