using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Character
{
    public class CharacterController : MonoBehaviour
    {
        [Range(0, 50)]
        [Tooltip("캐릭터 이동 속도")]
        [SerializeField] float m_MoveSpeed = 20;

        protected float xAxis;
        protected float zAxis;

        void Awake() => Init();

        public virtual void Init()
        {
            xAxis = 0f;
            zAxis = 0f;
        }

        void Update() => Move();

        public virtual void Move()
        {
            xAxis = Input.GetAxisRaw("Horizontal");
            zAxis = Input.GetAxisRaw("Vertical");

            transform.Translate(new Vector3(xAxis * m_MoveSpeed * Time.deltaTime, 0, zAxis * m_MoveSpeed * Time.deltaTime), Space.World);
        }
    }
}