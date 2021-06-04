using UnityEngine;

namespace IsolateIsland.Runtime.Character
{
    public class CharacterController : MonoBehaviour
    {
        [Range(0, 50)]
        [Tooltip("캐릭터 이동 속도")]
        [SerializeField] float m_MoveSpeed = 20;

        [Tooltip("XY 좌표 기준으로 이동할 것인가")]
        [SerializeField] protected bool m_bIsXY = false;

        protected float xAxis;
        protected float yAxis;

        void Awake() => Init();

        public virtual void Init()
        {
            xAxis = 0f;
            yAxis = 0f;
        }

        void Update() => Move();

        public virtual void Move()
        {
            xAxis = Input.GetAxisRaw("Horizontal");
            yAxis = Input.GetAxisRaw("Vertical");

            var _x = xAxis * m_MoveSpeed * Time.deltaTime;
            var _y = yAxis * m_MoveSpeed * Time.deltaTime;
            var _z = yAxis * m_MoveSpeed * Time.deltaTime;

            var _translate = m_bIsXY ? new Vector3(_x, _y, 0) : new Vector3(_x, 0, _z);

            transform.Translate(_translate, Space.World);
        }
    }
}