using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Character
{
    public class CharacterInteractSimulator
    {
        private Animator _animator;
        public CharacterInteractSimulator(Animator animator)
        {
            _animator = animator;

            Init();
        }

        public void Init()
        {
            Managers.Managers.Instance.Input.OnMouseAction += Input_OnMouseAction;
        }

        private void Input_OnMouseAction(Utils.Defines.MouseEvent evt)
        {
            
            _animator.Play("Attack");
        }

        public void Simulate()
        {

        }


    }
}