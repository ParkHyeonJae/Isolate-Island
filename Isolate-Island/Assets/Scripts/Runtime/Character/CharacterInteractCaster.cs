using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Character
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class CharacterInteractCaster : MonoBehaviour
    {
        public bool IsInteractable { get; protected set; }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!(collision.collider.CompareTag(Utils.Defines.Tags.TAG_ENTITY)))
                return;

            InteractableEntity entity;
            if (!collision.collider.TryGetComponent<InteractableEntity>(out entity))
                return;

            entity.OnInteractableEvent(entity);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!(collision.CompareTag(Utils.Defines.Tags.TAG_ENTITY)))
                return;

            InteractableEntity entity;
            if (!collision.TryGetComponent<InteractableEntity>(out entity))
                return;

            entity.OnInteractableEvent(entity);
        }

    }
}