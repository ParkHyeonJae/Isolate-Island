using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IsolateIsland.Runtime.Managers
{
    public class CoroutineManager : MonoManagerInit
    {

        private Dictionary<int, IEnumerator> coroutines = new Dictionary<int, IEnumerator>();

        private Dictionary<double, WaitForSeconds> _getWaitForSeconds = new Dictionary<double, WaitForSeconds>();
        private Dictionary<double, WaitForSecondsRealtime> _getWaitForSecondsRealtime = new Dictionary<double, WaitForSecondsRealtime>();

        public WaitForSeconds GetWaitForSeconds(float seconds)
        {
            WaitForSeconds waitForSeconds;
            if (!_getWaitForSeconds.ContainsKey(seconds)) {
                _getWaitForSeconds.Add(seconds, waitForSeconds = new WaitForSeconds(seconds));
                return waitForSeconds;
            }
            waitForSeconds = _getWaitForSeconds[seconds];
            return waitForSeconds;
        }
        public WaitForSecondsRealtime GetWaitForSecondsRealtime(float seconds)
        {
            WaitForSecondsRealtime waitForSeconds;
            if (!_getWaitForSecondsRealtime.ContainsKey(seconds)) {
                _getWaitForSecondsRealtime.Add(seconds, waitForSeconds = new WaitForSecondsRealtime(seconds));
                return waitForSeconds;
            }
            waitForSeconds = _getWaitForSecondsRealtime[seconds];
            return waitForSeconds;
        }

        private CoroutineManager()
        {
        }

        public Coroutine coroutine;

        

        public void RegisterRoutine(IEnumerator enumerator)
        {
            var key = enumerator.GetHashCode();
            if (coroutines.ContainsKey(key))
                return;

            coroutines.Add(key, enumerator);
        }

        public void StartRoutine(IEnumerator enumerator)
            => StartCoroutine(enumerator);
        public void StopRoutine(IEnumerator enumerator)
            => StopCoroutine(enumerator);

        public void UnRegisterRoutine(IEnumerator enumerator)
        {
            var key = enumerator.GetHashCode();
            if (!coroutines.ContainsKey(key))
                return;

            coroutines.Remove(enumerator.GetHashCode());
        }

        public override void OnInit()
        {
            StartCoroutine(UpdateRoutine());
        }

        private IEnumerator UpdateRoutine()
        {
            while (gameObject.activeInHierarchy)
            {
                var endRoutine = coroutines.Values.Where((item) => item.MoveNext() == false).FirstOrDefault();
                if (endRoutine != null)
                    coroutines.Remove(endRoutine.GetHashCode());

                yield return null;
            }
        }
    }
}