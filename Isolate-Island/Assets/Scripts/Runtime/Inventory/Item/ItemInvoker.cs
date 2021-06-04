using UnityEngine;
using UnityEngine.Events;

namespace IsolateIsland.Runtime.Inventory
{
    public class ItemInvoker : MonoBehaviour
    {
        [System.Serializable]
        public class ItemEvent : UnityEvent { }
        [SerializeField] protected ItemEvent _onItemCollect;

        private ItemBase @base = null;
        public ItemBase Base
        {
            get => @base = @base ?? GetComponent<ItemBase>();
        }
        private void Start()
        {
            _onItemCollect?.AddListener(() =>
            {
                Managers.Managers.Instance.Inventory.AddItem(Base);
                gameObject.SetActive(false);
            });
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _onItemCollect?.Invoke();
        }
    }
}