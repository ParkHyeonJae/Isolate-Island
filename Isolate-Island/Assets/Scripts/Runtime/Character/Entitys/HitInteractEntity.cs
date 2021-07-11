using IsolateIsland.Runtime.Character;
using IsolateIsland.Runtime.Event;
using IsolateIsland.Runtime.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Character
{
    public class HitInteractEntity : Entity
    {
        public override void Initalize()
        {
            Managers.Managers.Instance.Event.GetListener<OnHitInteractEvent>().Subscribe((entity) =>
            {
                if (entity == this)
                {
                    Managers.Managers.Instance.Event.GetListener<OnHitInteractEvent>().OnInteract(entity);
                    OnInteractHitEvent(entity);
                }
            });
            Managers.Managers.Instance.Event.GetListener<OnRangedHitInteractEvent>().Subscribe((dir, item, entity) =>
            {
                if (entity == this)
                {
                    Managers.Managers.Instance.Event.GetListener<OnRangedHitInteractEvent>().OnInteract(dir, entity);
                    OnArrowInteractHitEvent(item, entity);
                }
            });
        }

        /// <summary>
        /// 타격했을 때 발생하는 함수
        /// </summary>
        /// <param name="entity">타격 받은 엔티티</param>
        protected virtual void OnInteractHitEvent(Entity entity)
        {

        }


        protected virtual void OnArrowInteractHitEvent(DressableItem item, Entity entity)
        {

        }
    }
}