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