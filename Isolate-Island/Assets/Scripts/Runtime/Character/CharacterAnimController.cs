using UnityEngine;
using IsolateIsland.Runtime.Event;

namespace IsolateIsland.Runtime.Character
{
    public class CharacterAnimController : CharacterController
    {
        [SerializeField] private bool isFliping = false;
       
        public void OnEnterWakeup()
        {
            Managers.Managers.Instance.GameManager.enableMove = false;
        }
        public void OnExitWakeup()
        {
            Managers.Managers.Instance.GameManager.enableMove = true;
        }

        public override void Init()
        {
            base.Init();
            animator.SetTrigger("Wakeup");
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
                animator.SetBool("isRun", false);
                return;
            }

            var flipX = ((isFliping) ? (xAxis > 0) : (xAxis < 0)) ? 0f : -180f;
            transform.rotation = Quaternion.Euler(0, flipX, 0);

            animator.SetBool("isRun", true);

        }

        public void Dead()
        {
            animator.Play("Dead");
            //Managers.Managers.Instance.statManager.UserStat.MoveSpeed = 0;
            Managers.Managers.Instance.GameManager.enableMove = false;
        }
    }
}