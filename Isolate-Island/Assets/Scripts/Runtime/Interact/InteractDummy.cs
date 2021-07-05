using IsolateIsland.Runtime.Character;
using IsolateIsland.Runtime.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Interact
{
    public class InteractDummy : Entity
    {
        private void Awake()

        {
            Managers.Managers.Instance.Event.GetListener<HitInteractEvent>().Subscribe((entity) =>
            {
                if (entity == this)
                {
                    Managers.Managers.Instance.Event.GetListener<HitInteractEvent>().OnInteract(entity);
                    OnInteractHitEvent(entity);
                }
            });
        }

        protected virtual void OnInteractHitEvent(Entity entity)
        {

        }
    }
}