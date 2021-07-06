using IsolateIsland.Runtime.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Interact
{
    public class EnemyEntity : HitInteractEntity
    {
        private AITest _aI = null;
        public AITest AI => _aI = _aI ?? GetComponent<AITest>();
        protected override void OnInteractHitEvent(Entity entity)
        {
            base.OnInteractHitEvent(entity);

            AI.enemyStatus.hp -= Managers.Managers.Instance.statManager.UserStat.ATK;
            Debug.Log("========= =========Enemy HP : " + AI.enemyStatus.hp);
        }
    }
}