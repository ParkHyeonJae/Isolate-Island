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
        , typeof(ItemInvoker))]
    public class ItemBase : MonoBehaviour
    {
        [SerializeField] protected CombinationNode _combinationNode;

        public CombinationNode GetCombinationNode => _combinationNode;

        class ItemBuilder
        {
            
        }

        public static explicit operator int(ItemBase @base) => @base.GetCombinationNode.name.GetHashCode();
        public static explicit operator string(ItemBase @base) => @base.GetCombinationNode.name;

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