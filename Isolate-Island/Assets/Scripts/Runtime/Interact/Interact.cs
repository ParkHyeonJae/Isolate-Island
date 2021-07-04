using IsolateIsland.Runtime.Character;
using IsolateIsland.Runtime.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Interact
{
    internal interface IInteractable
    {
        void OnInteract(InteractEvent interactEvent);
    }

    public abstract class InteractEvent : EventListener
    {
        public abstract void OnInteract();
    }

    public class HitInteractEvent : InteractEvent
    {
        public sealed override void OnInteract()
        {
        }

        public void OnInteract(Entity entity)
        {
            var obj = Managers.Managers.Instance.Pool.Instantiate("FX_Hit_01");
            obj.transform.position = entity.transform.position;
        }


        //IEnumerator HitInteract(Entity entity)
        //{
        //    var color = entity.spriteRenderer.color;

        //    color.a


        //    yield return null;
        //}
    }
}