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
                .SetMoveSpeed(20)
                .SetAttackSpeed(10)
                .SetAttack(5)
                .Build();
        }

        public void ReducePlayerHp(int value)
        {
            float defend = value * UserStat.DEF * 0.05f;
            int damage = value - Mathf.RoundToInt(defend);
            UserStat.HP -= value;
            if (UserStat.HP <= 0)
                Managers.Instance.Event.GetListener<OnGameoverEvent>().Invoke();
        }


    }
}