using IsolateIsland.Runtime.Character;
using IsolateIsland.Runtime.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Interact
{
    public class InteractDummy : Entity, IInteractable
    {
        public void OnInteract(InteractEvent interactEvent)
        {
            switch (interactEvent)
            {
                case HitInteractEvent hit:
                    
                    Debug.Log("HIt !");
                    hit.OnInteract(this);
                    break;
                default:
                    break;
            }
        }
    }
}