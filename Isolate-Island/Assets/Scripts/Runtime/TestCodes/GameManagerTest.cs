using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.TestCodes
{
    public class GameManagerTest : MonoBehaviour
    {
        public int dayCycle = 60;
        public int survivalDate = 1;

        public bool isDay = true;

        public void Start() => Init();

        public void Init()
        {
            StartCoroutine(PlayDayCycle());
        }

        IEnumerator PlayDayCycle()
        {
            yield return new WaitForSeconds(dayCycle);
            isDay = !isDay;
            StartCoroutine(PlayDayCycle());
        }
    }
}
