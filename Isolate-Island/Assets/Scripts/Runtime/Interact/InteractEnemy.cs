using IsolateIsland.Runtime.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsolateIsland.Runtime.Ai;

namespace IsolateIsland.Runtime.Interact
{
    public class InteractEnemy : InteractDummy
    {
        private EnemyAI _aI = null;
        public EnemyAI AI => _aI = _aI ?? GetComponent<EnemyAI>();
        
        private AITest _testAI = null;
        public AITest testAI => _testAI = _testAI ?? GetComponent<AITest>();
        protected override void OnInteractHitEvent(Entity entity)
        {
            base.OnInteractHitEvent(entity);

            if (testAI != null)
            {
                testAI.enemyStatus.hp -= Managers.Managers.Instance.statManager.UserStat.ATK;
                Debug.Log("========= =========Enemy(Test) HP : " + testAI.enemyStatus.hp);
            }

            if (AI != null)
            {
                AI.hp -= Managers.Managers.Instance.statManager.UserStat.ATK;
                Debug.Log("========= =========Enemy(Test) HP : " + AI.hp);
            }
        }
    }
}