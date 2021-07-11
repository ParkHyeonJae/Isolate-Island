using IsolateIsland.Runtime.Character;
using IsolateIsland.Runtime.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Inventory
{
    public class ItemBehaviourModule_Arrow : ItemBehaviourModule_MoveTo
    {

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag(Utils.Defines.Tags.TAG_ENTITY))
                return;

            HitInteractEntity entity;
            if (!collision.TryGetComponent<HitInteractEntity>(out entity))
                return;

            Managers.Managers.Instance.Event.GetListener<OnRangedHitInteractEvent>().Invoke(dir, this.GetComponent<DressableItem>(), entity);
        }

    }
}