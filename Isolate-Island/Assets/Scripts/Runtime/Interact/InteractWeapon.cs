using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Interact
{
    public class InteractWeapon : MonoBehaviour, IInteractable
    {
        HitInteractEvent hitInteractEvent = new HitInteractEvent();

        public void OnInteract(InteractEvent interactEvent)
        {

        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            IInteractable interactable;
            if (!collision.TryGetComponent<IInteractable>(out interactable))
                return;

            interactable.OnInteract(hitInteractEvent);
        }
    }
}