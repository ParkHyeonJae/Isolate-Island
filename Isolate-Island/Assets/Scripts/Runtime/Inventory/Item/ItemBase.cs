using IsolateIsland.Runtime.Combination;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace IsolateIsland.Runtime.Inventory
{
    [DisallowMultipleComponent]
    [RequireComponent(
          typeof(BoxCollider2D)
        , typeof(ItemInvoker)
        , typeof(SpriteRenderer))]
    public class ItemBase : MonoBehaviour
    {
        [SerializeField] protected CombinationNode _combinationNode;

        public CombinationNode GetCombinationNode => _combinationNode;

        public static explicit operator int(ItemBase @base) => @base.GetCombinationNode.name.GetHashCode();
        public static explicit operator string(ItemBase @base) => @base.GetCombinationNode.name;

        private SpriteRenderer _spriteRenderer;
        public SpriteRenderer SpriteRenderer => _spriteRenderer = _spriteRenderer ?? GetComponent<SpriteRenderer>();


        private void Awake()
        {
            SpriteRenderer.sprite = GetCombinationNode.sprite;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"=== {gameObject.name} ===\n");
            sb.Append("Require Combination Node List = \n");
            foreach (var combinationNode in GetCombinationNode.combinationNodes)
            {
                sb.Append($"{combinationNode.Name} : {combinationNode.Count}개\n");
            }
            return sb.ToString();
        }
    }
}