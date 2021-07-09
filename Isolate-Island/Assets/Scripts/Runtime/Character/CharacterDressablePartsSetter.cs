using IsolateIsland.Runtime.Event;
using IsolateIsland.Runtime.Inventory;
using IsolateIsland.Runtime.Stat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IsolateIsland.Runtime.Utils.Defines;

namespace IsolateIsland.Runtime.Character
{
    public class CharacterDressablePartsSetter : MonoBehaviour
    {
        [SerializeField] EParts eParts = EParts.PARTS_NONE;
        public EParts Parts => eParts;
        private SpriteRenderer _spriteRenderer;
        public SpriteRenderer SpriteRenderer
            => _spriteRenderer = _spriteRenderer ?? GetComponent<SpriteRenderer>();
        private BoxCollider2D _boxCollider;
        public BoxCollider2D BoxCollider
            => _boxCollider = _boxCollider ?? GetComponent<BoxCollider2D>();


        private void Awake()
        {
            Managers.Managers.Instance.Event.GetListener<DressableEventListener>().Subscribe(OnDressable);
        }

        void OnDressable(EDressableState eDressableState, DressableItem dressableItem)
        {
            if (eParts == EParts.PARTS_NONE) return;

            var node = dressableItem.DressableCombinationNode;
            var stat = node.DressableStat;

            if (this.eParts != stat.DRESSABLE_Parts) return;

            switch (eDressableState)
            {
                case EDressableState.Use:
                    SpriteRenderer.sprite = node.sprite;

                    if (node.OnDressableSetting.Position != Vector3.zero)
                        transform.localPosition = node.OnDressableSetting.Position;
                    if (node.OnDressableSetting.Rotation != Vector3.zero)
                        transform.localEulerAngles = node.OnDressableSetting.Rotation;

                    if (node.OnDressableSetting.Scale != Vector3.one)
                        transform.localScale = node.OnDressableSetting.Scale;

                    if (BoxCollider)
                    {
                        BoxCollider.offset = node.OnDressableSetting.ColliderOffset;
                        BoxCollider.size = node.OnDressableSetting.ColliderSize;
                    }
                    break;
                case EDressableState.Drop:
                    SpriteRenderer.sprite = null;
                    break;
                default:
                    break;
            }
        }

    }
}