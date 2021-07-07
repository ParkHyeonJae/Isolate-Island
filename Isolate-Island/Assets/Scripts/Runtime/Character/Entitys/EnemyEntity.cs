using IsolateIsland.Runtime.Ai;
using IsolateIsland.Runtime.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Interact
{
    public class EnemyEntity : HitInteractEntity
    {
        private Enemy _aI = null;
        public Enemy AI => _aI = _aI ?? GetComponent<Enemy>();
        protected override void OnInteractHitEvent(Entity entity)
        {
            base.OnInteractHitEvent(entity);

            AI.hp -= Managers.Managers.Instance.statManager.UserStat.ATK;
            Debug.Log("========= =========Enemy HP : " + AI.hp);
        }
    }
}