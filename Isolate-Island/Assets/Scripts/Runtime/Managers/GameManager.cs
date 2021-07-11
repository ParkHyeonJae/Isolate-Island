using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsolateIsland.Runtime.Event;

namespace IsolateIsland.Runtime.Managers
{
    public class GameManager : IManagerInit, IManagerUpdate
    {
        private const float _timeCycle = 60;
        public const int hungryDamage = 5;
        public const int reduceHungryForMinute = 30;
        public const int hungryDamageDelay = 3;
        public const int debuffHungry = 30;

        public bool enableMove = true;
        public int survivalDate { get; private set; }
        public float flowDayTime { get; private set; }
        public bool isDay { get; private set; }
        public int killCount { get; private set; }

        private bool _onGame = true;
        public bool onGame { get { return _onGame; } set { _onGame = value; } }

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
            if (!onGame)
                return;
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
            PlayBgm();
            flowDayTime = 1;
        }

        public void PlayBgm()
        {
            if (isDay)
                Managers.Instance.Sound.Play("낮");
            else
                Managers.Instance.Sound.Play("밤");             
        }

        public void UpKillCount() => killCount++;
    }
}
