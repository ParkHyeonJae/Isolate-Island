using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsolateIsland.Runtime.Event;

namespace IsolateIsland.Runtime.Managers
{
    public class GameManager : IManagerInit, IManagerUpdate
    {
        private const float _timeCycle = 60;

        public int survivalDate { get; private set; }
        public float flowDayTime { get; private set; }
        public bool isDay { get; private set; }
        public int killCount { get; private set; }

        public void OnInit()
        {
            isDay = true; 
            flowDayTime = 1;
            survivalDate = 1;
            killCount = 0;

            Managers.Instance.Event.GetListener<OnChangeDayOrNightEvent>().Subscribe(ChangeDay);
        }

        public void OnUpdate()
        {
            flowDayTime -= Time.deltaTime / _timeCycle;
            if (flowDayTime <= 0)
            {
                Managers.Instance.Event.GetListener<OnChangeDayOrNightEvent>().Invoke();
            }
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

        public void UpKillCount() => killCount++;
    }
}
