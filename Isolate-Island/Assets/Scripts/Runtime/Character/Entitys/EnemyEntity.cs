using IsolateIsland.Runtime.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsolateIsland.Runtime.Ai;
using IsolateIsland.Runtime.Inventory;

namespace IsolateIsland.Runtime.Interact
{
    public class EnemyEntity : HitInteractEntity
    {
        private EnemyAI _aI = null;
        public EnemyAI AI => _aI = _aI ?? GetComponent<Enemy>();

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
                AI.ReduceHp(Managers.Managers.Instance.statManager.UserStat.ATK);
                AI.gameObject.GetOrAddComponent<EnemySoundController>().PlayHitSound();
                Debug.Log("========= =========Enemy(Test) HP : " + AI.hp);
            }
        }

        protected override void OnArrowInteractHitEvent(DressableItem item, Entity entity)
        {
            base.OnArrowInteractHitEvent(item, entity);
            var arrowDmg = item.DressableCombinationNode.DressableStat.DRESSABLE_ATK;
            var userDmg = Managers.Managers.Instance.statManager.UserStat.ATK;
            if (testAI != null)
            {
                testAI.enemyStatus.hp -= userDmg + arrowDmg;
                Debug.Log("========= =========Enemy(Test) HP : " + testAI.enemyStatus.hp);
            }

            if (AI != null)
            {
                Debug.Log($"현재 데미지 {userDmg }, 화살 데미지 : {arrowDmg}, 누적 : {userDmg + arrowDmg}");
                AI.ReduceHp(userDmg + arrowDmg);
                AI.gameObject.GetOrAddComponent<EnemySoundController>().PlayHitSound();
                Debug.Log("========= =========Enemy(Test) HP : " + AI.hp);
            }
        }
    }
}