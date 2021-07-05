using IsolateIsland.Runtime.Event;
using UnityEngine;
using UnityEngine.Events;

namespace IsolateIsland.Runtime.Inventory
{
    public class ItemInvoker : MonoBehaviour
    {
        [System.Serializable]
        public class ItemEvent : UnityEvent { }
        [SerializeField] ItemEvent _onItemCollctEvent = new ItemEvent();

        private ItemBase @base = null;
        public ItemBase Base
        {
            get => @base = @base ?? GetComponent<ItemBase>();
        }

        private void Awake()
        {
            _onItemCollctEvent.AddListener(OnInvoke);
            _onItemCollctEvent.AddListener(() => Managers.Managers.Instance.Event.GetListener<OnCollectItemEvent>()?.Invoke());
        }

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
            if (Managers.Managers.Instance.Inventory.Game.Items.Count == 10)
                return;
            _onItemCollctEvent?.Invoke();
        }
    }
}