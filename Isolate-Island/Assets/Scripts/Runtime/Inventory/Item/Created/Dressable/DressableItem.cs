using IsolateIsland.Runtime.Combination;
using IsolateIsland.Runtime.Utils;
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
    public class DressableItem : ItemBase
    {
        public DressableCombinationNode DressableCombinationNode => CombinationNode as DressableCombinationNode;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"=== {gameObject.name} ===\n");
            sb.Append("Require Combination Node List = \n");
            foreach (var combinationNode in CombinationNode.combinationNodes)
            {
                sb.Append($"{combinationNode.Name} : {combinationNode.Count}개\n");
            }
            return sb.ToString();
        }


        public virtual void OnEnterDressable()
        {
        }


        public virtual void OnExitDressable()
        {
        }

    }
}