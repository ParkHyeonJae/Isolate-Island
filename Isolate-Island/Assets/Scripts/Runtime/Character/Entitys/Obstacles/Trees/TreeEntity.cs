using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Character
{
    public class TreeEntity : InteractableEntity
    {
        protected override void OnInteractableEvent(Entity entity)
        {
            base.OnInteractableEvent(entity);

            Debug.Log("Interact Tree");
        }
    }
}