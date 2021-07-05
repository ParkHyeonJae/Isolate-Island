using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Managers
{
    public class GameManager : IManagerInit, IManagerUpdate
    {
        private const float _timeCycle = 60;

        public int survivalDate { get; private set; }
        public float flowDayTime { get; private set; }
        public bool isDay { get; private set; }

        public void OnInit()
        {
            isDay = true; 
            flowDayTime = 1;
            survivalDate = 1;
        }

        public void OnUpdate()
        {
            flowDayTime -= Time.deltaTime / _timeCycle;
        }

        public void ChangeDay()
        {
            if (isDay)
            {
                isDay = false;
            }

            else
            {
                isDay = true;
                survivalDate++;
            }
            flowDayTime = 1;
        }
    }
}
