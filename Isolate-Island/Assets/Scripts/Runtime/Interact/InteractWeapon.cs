using IsolateIsland.Runtime.Character;
using IsolateIsland.Runtime.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Interact
{
    public class InteractWeapon : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Entity entity;
            if (!collision.TryGetComponent<Entity>(out entity))
                return;

            Managers.Managers.Instance.Event.GetListener<HitInteractEvent>().Invoke(entity);
        }
    }
}