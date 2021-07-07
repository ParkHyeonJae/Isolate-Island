using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsolateIsland.Runtime.Ai;
using IsolateIsland.Runtime.Event;

namespace IsolateIsland.Runtime.Interact
{
    public class InteractEnemyWeapon : MonoBehaviour
    {
        [SerializeField] private EnemyAI _ai = null;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player"))
                return;

            /// 기존 구조를 흉내냈는데 그렇게 하면 
            /// 여기에서 피 깎이는 이벤트를 추가했을 때
            /// 에너미 숫자만큼 이벤트가 추가될뻔해서 
            /// 피 깎이는 코드는 이벤트에 포함하지 않은 상태로 만듦
            Managers.Managers.Instance.statManager.ReducePlayerHp(_ai.enemyData.attackDamage);
            Managers.Managers.Instance.Event.GetListener<HitInteractPlayerEvent>().Invoke();
        }

    }

}