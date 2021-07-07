using IsolateIsland.Runtime.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsolateIsland.Runtime.Ai;

namespace IsolateIsland.Runtime.Interact
{
    public class AnimalEntity : HitInteractEntity
    {
        private AnimalAI _aI = null;
        public AnimalAI AI => _aI = _aI ?? GetComponent<AnimalAI>();

        protected override void OnInteractHitEvent(Entity entity)
        {
            base.OnInteractHitEvent(entity);

            if (AI != null)
            {
                AI.ReduceHp(Managers.Managers.Instance.statManager.UserStat.ATK);
                Debug.Log("========= =========Animal(Test) HP : " + AI.hp);
            }
        }
    }
}