using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsolateIsland.Runtime.Stat;

namespace IsolateIsland.Runtime.Managers
{
    using Stat = Stat.Stat;

    public class StatManager : IManagerInit
    {
        public Stat UserStat { get; set; }

        public void OnInit()
        {
            StatBuilder stat = new StatBuilder();
            UserStat = stat
                .SetMAXHP(100)
                .SetHP(100)
                .SetHungry(100)
                .SetMoveSpeed(20)
                .SetAttackSpeed(10)
                .Build();




        }




    }
}