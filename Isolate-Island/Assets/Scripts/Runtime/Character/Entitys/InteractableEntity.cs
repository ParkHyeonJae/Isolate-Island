using IsolateIsland.Runtime.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace IsolateIsland.Runtime.Character
{
    /// <summary>
    /// 상속구조를 통해 정의하는 방법
    /// </summary>
    public class InteractType
    {
        public static readonly InteractType Interact = new InteractType("Interact");

        public override string ToString()
        {
            return this.Value;
        }

        public InteractType(in string value) => this.Value = value;

        public string Value { get; private set; }


    }
    public class InteractableEntity : Entity
    {
        public override void Initalize()
        {
            Managers.Managers.Instance.Event.GetListener<OnInteractCasterEvent>().Subscribe((entity) =>
            {
                if (entity == this)
                {
                    var breakable = entity as BreakableEntity;

                    if (breakable && !breakable.IsBreak)
                        Managers.Managers.Instance.Event.GetListener<OnInteractEvent>().OnInteract(entity);
                    OnInteractableEvent(entity);
                }
            });
        }

        protected virtual void OnInteractableEvent(Entity entity)
        {
            
        }


        protected virtual bool AnimatePlayFactory(InteractType interactType)
        {
            if (interactType == InteractType.Interact)
            {
                animator.Play(interactType.Value);
                return true;
            }

            return false;
        }
    }
}