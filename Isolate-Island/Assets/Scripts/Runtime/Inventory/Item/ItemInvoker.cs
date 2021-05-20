using UnityEngine;
using UnityEngine.Events;

namespace IsolateIsland.Runtime.Inventory
{
    public class ItemInvoker : MonoBehaviour
    {
        [System.Serializable]
        public class ItemEvent : UnityEvent { }
        [SerializeField] protected ItemEvent _onItemCollect;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _onItemCollect?.Invoke();
        }
    }
}