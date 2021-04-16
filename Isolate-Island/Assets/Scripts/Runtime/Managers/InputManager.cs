using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Managers
{
    public class InputManager : IManagerInit, IManagerUpdate
    {

        // 키가 눌렀을 때마다 호출
        private event System.Action OnInputKey;

        public void AddKeyEvent(System.Action action) => OnInputKey += action;

        public void OnInit()
        {
            OnInputKey = delegate { };
        }

        public void OnUpdate()
        {
            if (Input.anyKey)
            {
                OnInputKey?.Invoke();
            }
        }
    }
}