using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsolateIsland.Runtime.Stat;
using IsolateIsland.Runtime.Event;

namespace IsolateIsland.Runtime.Managers
{
    using Stat = Stat.Stat;

    public class StatManager : IManagerInit
    {
        public Stat UserStat { get; set; }

        public enum PlayerStatus
        {
            maxHp,
            hp,
            hungry,
            moveSpeed,
            attackSpeed,
            attack
        }

        public void OnInit()
        {
            StatBuilder stat = new StatBuilder();
            UserStat = stat
                .SetMAXHP(100)
                .SetHP(100)
                .SetHungry(100)
                .SetMoveSpeed(5)
                .SetAttackSpeed(10)
                .SetAttack(5)
                .Build();

            Managers.Instance.Coroutine.StartRoutine(ReducePlayerHungry());
        }

        public void ReducePlayerHp(int value)
        {
            float defend = value * UserStat.DEF * 0.05f;
            int damage = value - Mathf.RoundToInt(defend);
            UserStat.HP -= value;
            if (UserStat.HP <= 0)
                Managers.Instance.Event.GetListener<OnGameoverEvent>().Invoke();
            else
                Managers.Instance.Event.GetListener<OnPlayerHitEvent>().Invoke();
        }

        public void ReducePlayerHp(int value, bool isTrueDamage)
        {
            if (isTrueDamage)
            {
                UserStat.HP -= value;
            }
            else
            {
                float defend = value * UserStat.DEF * 0.05f;
                int damage = value - Mathf.RoundToInt(defend);
                UserStat.HP -= value;
            }

            if (UserStat.HP <= 0)
                Managers.Instance.Event.GetListener<OnGameoverEvent>().Invoke();
            else
                Managers.Instance.Event.GetListener<OnPlayerHitEvent>().Invoke();
        }

        public IEnumerator ReducePlayerHungry()
        {

            if (UserStat.Hungry > 0)
            {
                yield return Managers.Instance.Coroutine.GetWaitForSeconds(5);
                UserStat.Hungry -= 100;
                if (UserStat.Hungry < 0)
                    UserStat.Hungry = 0;
            }
            else
            {
                yield return Managers.Instance.Coroutine.GetWaitForSeconds(2);
                if (UserStat.HP > 5)
                    ReducePlayerHp(5, true);
                else
                {
                    ReducePlayerHp(UserStat.HP - 1, true);
                }
            }

            Managers.Instance.Coroutine.StartRoutine(ReducePlayerHungry());
        }


    }
}