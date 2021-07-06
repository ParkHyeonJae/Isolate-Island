using IsolateIsland.Runtime.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Character
{
    public class InteractableEntity : Entity
    {
        public override void Initalize()
        {
            Managers.Managers.Instance.Event.GetListener<OnInteractCasterEvent>().Subscribe((entity) =>
            {
                if (entity == this)
                {
                    Managers.Managers.Instance.Event.GetListener<HitInteractEvent>().OnInteract(entity);
                    OnInteractableEvent(entity);
                }
            });
        }

        protected virtual void OnInteractableEvent(Entity entity)
        {

        }

    }
}