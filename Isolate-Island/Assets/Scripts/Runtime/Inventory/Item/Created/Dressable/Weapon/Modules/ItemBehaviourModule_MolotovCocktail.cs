using IsolateIsland.Runtime.Character;
using IsolateIsland.Runtime.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Inventory
{
    public class ItemBehaviourModule_MolotovCocktail : ItemBehaviourModule_MoveTo
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

        protected override void DestroyTiming()
        {
            Managers.Managers.Instance.Util.AddTimer(0.6f, OnDestroyObject);
        }

        protected void OnDestroyObject()
        {
            Managers.Managers.Instance.Coroutine.StartRoutine(Explosion());
            
        }

        protected IEnumerator Explosion()
        {
            var particle = Managers.Managers.Instance.Pool.ParticleInstantiate("FX_Hit_01", 0.5f);
            particle.transform.position = transform.position;
            yield return null;
            Managers.Managers.Instance.Pool.Destroy(gameObject);
        }

    }
}