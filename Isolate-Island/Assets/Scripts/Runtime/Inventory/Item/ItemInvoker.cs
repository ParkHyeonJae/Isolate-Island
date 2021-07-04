using UnityEngine;
using UnityEngine.Events;

namespace IsolateIsland.Runtime.Inventory
{
    public class ItemInvoker : MonoBehaviour
    {
        [System.Serializable]
        public class ItemEvent : UnityEvent { }
        [SerializeField] ItemEvent _onItemCollctEvent;

        private System.Action _onItemCollect;

        private ItemBase @base = null;
        public ItemBase Base
        {
            get => @base = @base ?? GetComponent<ItemBase>();
        }
        private void Awake()
        {
            _onItemCollect += OnInvoke;
            _onItemCollect += () => _onItemCollctEvent.Invoke();
        }

        protected virtual void OnInvoke()
        {
            Managers.Managers.Instance.Inventory.Game.AddItem(Base);
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player"))
                return;
            _onItemCollect?.Invoke();
        }
    }
}