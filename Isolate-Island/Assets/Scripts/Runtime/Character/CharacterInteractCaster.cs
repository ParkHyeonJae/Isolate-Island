using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Character
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class CharacterInteractCaster : MonoBehaviour
    {
        public bool IsInteractable { get; protected set; }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag(Utils.Defines.Tags.TAG_ENTITY))
                return;

            InteractableEntity entity;
            if (!collision.TryGetComponent<InteractableEntity>(out entity))
                return;
            //Debug.Log($"Interact Casting : {entity.name}");
            Managers.Managers.Instance.Event.GetListener<Event.OnDetectCasterEvent>()
                ?.Invoke(entity, IsInteractable = true);
        }


        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.CompareTag(Utils.Defines.Tags.TAG_ENTITY))
                return;

            InteractableEntity entity;
            if (!collision.TryGetComponent<InteractableEntity>(out entity))
                return;

            Managers.Managers.Instance.Event.GetListener<Event.OnDetectCasterEvent>()
                ?.Invoke(entity, IsInteractable = false);
        }
    }
}