using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Inventory
{
    public abstract class ItemModule : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        public Rigidbody2D GetRigidBody
            => _rigidbody2D = _rigidbody2D ?? gameObject.GetOrAddComponent<Rigidbody2D>();

        private SpriteRenderer _spriteRenderer;
        public SpriteRenderer SpriteRenderer
            => _spriteRenderer = _spriteRenderer ?? GetComponent<SpriteRenderer>();

        private BoxCollider2D _boxCollider2D;
        public BoxCollider2D BoxCollider2D
            => _boxCollider2D = _boxCollider2D ?? GetComponent<BoxCollider2D>();

        private ItemInvoker _itemInvoker;
        public ItemInvoker ItemInvoker
            => _itemInvoker = _itemInvoker ?? GetComponent<ItemInvoker>();

        public abstract void Do();

    }
}