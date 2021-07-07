using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace IsolateIsland.Runtime.Light
{
    public class DayNightConfigurator : MonoBehaviour
    {
        [SerializeField] Light2D _globalLight;
        // Start is called before the first frame update
        void Start()
        {
            Managers.Managers.Instance.Coroutine.RegisterRoutine(Updater());
        }

        IEnumerator Updater()
        {
            while (gameObject.activeInHierarchy)
            {
                //if (Managers.Managers.Instance.GameManager.flowDayTime >= 0)
                //{
                //    _globalLight.intensity = Managers.Managers.Instance.GameManager.flowDayTime;
                //}

                if (Managers.Managers.Instance.GameManager.isDay)
                    _globalLight.intensity = 1.0f;
                if (!Managers.Managers.Instance.GameManager.isDay)
                    _globalLight.intensity = 0.3f;

                yield return null;
            }
        }
    }
}