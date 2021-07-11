using IsolateIsland.Runtime.Interact;
using UnityEngine;

namespace IsolateIsland.Runtime.Character
{
    public class CharacterController : HitInteractEntity
    {
        public float m_MoveSpeed => Managers.Managers.Instance.statManager.UserStat.MoveSpeed;

        [Range(0, 400)]
        [SerializeField] float m_RigidbodyMoveSpeed = 100;
        public float RigidbodyMoveSpeed => m_RigidbodyMoveSpeed;

        [Tooltip("XY 좌표 기준으로 이동할 것인가")]
        [SerializeField] protected bool m_bIsXY = false;
        [SerializeField] protected bool m_bIsRigidbodyMove = false;

        protected float xAxis;
        protected float yAxis;
        public Vector2 MoveDir => new Vector2(xAxis, yAxis); 
        public Vector2 MoveNormalDir => new Vector2(xAxis, yAxis).normalized; 

        void Awake() => Init();

        public virtual void Init()
        {
            xAxis = 0f;
            yAxis = 0f;
        }
        protected virtual void OnUpdate()
        {
            if (Managers.Managers.Instance.GameManager.enableMove == false)
                return;
            Move();
        }
        void Update() => OnUpdate();

        public virtual void Move()
        {
            xAxis = Input.GetAxisRaw("Horizontal");
            yAxis = Input.GetAxisRaw("Vertical");

            if (!m_bIsRigidbodyMove)
            {
                var _x = xAxis * m_MoveSpeed * Time.deltaTime;
                var _y = yAxis * m_MoveSpeed * Time.deltaTime;
                var _z = yAxis * m_MoveSpeed * Time.deltaTime;

                var _translate = m_bIsXY ? new Vector3(_x, _y, 0) : new Vector3(_x, 0, _z);

                transform.Translate(_translate, Space.World);
            }
            else
            {
                if (GetRigidBody2D is null)
                    return;

                GetRigidBody2D.AddForce(MoveNormalDir * m_RigidbodyMoveSpeed, ForceMode2D.Force);
                GetRigidBody2D.velocity = Vector2.zero;
            }
        }
    }
}