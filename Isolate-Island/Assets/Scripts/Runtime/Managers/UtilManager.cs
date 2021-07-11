using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Managers
{
    public class UtilManager : IManagerInit
    {
        public void OnInit()
        {
        }


        #region TIMER
        private IEnumerator timer(float Time, Action onTimeout, bool isRealtime = false)
        {
            switch (isRealtime)
            {
                case true:
                    yield return Managers.Instance.Coroutine.GetWaitForSecondsRealtime(Time);
                    break;
                case false:
                    yield return Managers.Instance.Coroutine.GetWaitForSeconds(Time);
                    break;
            }
            onTimeout?.Invoke();
        }
        public void AddTimer(float time, Action onTimeout)
            => Managers.Instance.Coroutine.StartRoutine(timer(time, onTimeout));
        public void AddTimer(float time, Action onTimeout, bool isRealtime = false)
            => Managers.Instance.Coroutine.StartRoutine(timer(time, onTimeout, isRealtime));
        #endregion

    }
}