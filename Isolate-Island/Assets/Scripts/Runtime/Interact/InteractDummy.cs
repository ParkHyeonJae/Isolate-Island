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

    }

    public class HitInteractEvent : InteractEvent
    {

    }



    public class InteractDummy : MonoBehaviour, IInteractable
    {
        public void OnInteract(InteractEvent interactEvent)
        {
            switch (interactEvent)
            {
                case HitInteractEvent hit:
                    Debug.Log("HIt !");
                    break;
                default:
                    break;
            }
        }
    }
}