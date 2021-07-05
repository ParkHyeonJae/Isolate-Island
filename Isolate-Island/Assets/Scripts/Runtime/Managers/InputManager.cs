using IsolateIsland.Runtime.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace IsolateIsland.Runtime.Managers
{
    public class InputManager : IManagerInit, IManagerUpdate
    {

        // 키가 눌렀을 때마다 호출
        public event System.Action OnInputKey;
        public event System.Action<Defines.MouseEvent> OnMouseAction;
        private bool _pressed = false;
        public void OnInit()
        {
            OnInputKey = delegate { };
        }

        public void OnUpdate()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (Input.anyKey && OnInputKey != null)
                OnInputKey?.Invoke();

            if (Input.GetMouseButton(0) && OnMouseAction != null)
            {
                OnMouseAction?.Invoke(Defines.MouseEvent.Press);
                _pressed = true;
            }
            else
            {
                if (_pressed)
                    OnMouseAction?.Invoke(Defines.MouseEvent.Click);
                _pressed = false;
            }
            

        }
    }
}