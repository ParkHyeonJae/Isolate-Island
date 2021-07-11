using IsolateIsland.Runtime.Event;
using UnityEngine;
using UnityEngine.Events;

namespace IsolateIsland.Runtime.Inventory
{
    public class ItemInvoker : MonoBehaviour
    {
        [Header("먹었을 때 얻어지는 아이템 개수")]
        [SerializeField] private int DefaultAddedItemCount = 1;

        [System.Serializable]
        public class ItemEvent : UnityEvent { }
        [SerializeField] ItemEvent _onItemCollctEvent = new ItemEvent();

        private ItemBase @base = null;
        public ItemBase Base
        {
            get => @base = @base ?? GetComponent<ItemBase>();
        }

        public bool _isCollisionCheck = true;

        private void Awake()
        {
            _onItemCollctEvent.AddListener(OnInvoke);
            _onItemCollctEvent.AddListener(() => Managers.Managers.Instance.Event.GetListener<OnCollectItemEvent>()?.Invoke());
        }

        protected virtual void OnInvoke()
        {
            for (int i = 0; i < DefaultAddedItemCount; i++)
                Managers.Managers.Instance.Inventory.Game.AddItem(Base);

            Debug.Log(Base);
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!_isCollisionCheck)
                return;
            if (!collision.CompareTag("Player"))
                return;
            if (Managers.Managers.Instance.Inventory.Game.Items.Count == 10 
                && !Managers.Managers.Instance.Inventory.Game.IsContain(Base))
                return;
            _onItemCollctEvent?.Invoke();
        }
    }
}