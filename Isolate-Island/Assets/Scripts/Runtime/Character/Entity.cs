using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Character
{
    public abstract class Entity : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        public SpriteRenderer spriteRenderer => _spriteRenderer ?? GetComponent<SpriteRenderer>();
        public Sprite sprite => spriteRenderer.sprite;
    }

}