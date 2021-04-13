using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace IsolateIsland.Runtime.Inventory
{
    public class ItemInvoker : MonoBehaviour
    {
        [System.Serializable]
        public class ItemEvent : UnityEvent { }
        [SerializeField] protected ItemEvent _onItemCollect;


        private void OnTriggerEnter(Collider other)
        {
            _onItemCollect?.Invoke();
        }
    }
}