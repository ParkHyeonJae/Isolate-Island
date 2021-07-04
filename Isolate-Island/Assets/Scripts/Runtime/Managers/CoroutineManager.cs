using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Managers
{
    public class CoroutineManager : MonoBehaviour, IManagerInit
    {

        private Dictionary<IEnumerator, Coroutine> Coroutines = new Dictionary<IEnumerator, Coroutine>();

        private CoroutineManager()
        {
        }

        public Coroutine coroutine;
        private GameObject routineObject;

        

        public Coroutine RegisterRoutine(IEnumerator enumerator)
            => coroutine = StartCoroutine(enumerator);

        public void OnInit() => (routineObject = new GameObject("@Coroutine")).transform.SetParent(Managers.Instance.transform);
    }
}