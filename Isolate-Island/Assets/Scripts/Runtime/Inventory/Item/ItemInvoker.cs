using UnityEngine;
using UnityEngine.Events;

namespace IsolateIsland.Runtime.Inventory
{
    public class ItemInvoker : MonoBehaviour
    {
        [System.Serializable]
        public class ItemEvent : UnityEvent { }
        [SerializeField] protected ItemEvent _onItemCollect = new ItemEvent();

        private ItemBase @base = null;
        public ItemBase Base
        {
            get => @base = @base ?? GetComponent<ItemBase>();
        }

        private void Start() => _onItemCollect?.AddListener(OnInvoke);

        protected virtual void OnInvoke()
        {
            Managers.Managers.Instance.Inventory.Game.AddItem(Base);
            Debug.Log(Base);
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