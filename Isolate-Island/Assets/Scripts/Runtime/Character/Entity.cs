using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Character
{
    public abstract class Entity : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        public SpriteRenderer spriteRenderer => _spriteRenderer ?? GetComponent<SpriteRenderer>();
        private Rigidbody2D _rigidbody2D;
        public Rigidbody2D GetRigidBody2D => _rigidbody2D ?? GetComponent<Rigidbody2D>();
        public Sprite sprite => spriteRenderer.sprite;
    }

}