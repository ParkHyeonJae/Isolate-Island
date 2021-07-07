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
    public class DressableItem : ItemBase, IStatAble
    {
        public DressableCombinationNode DressableCombinationNode => CombinationNode as DressableCombinationNode;

        public string GetStatInfo
        {
            get
            {
                var node = DressableCombinationNode;
                if (node == null)
                    return string.Empty;

                StringBuilder sb = new StringBuilder();

                var atk = node.DressableStat.DRESSABLE_ATK;
                var range = node.DressableStat.DRESSABLE_RANGE;
                var def = node.DressableStat.DRESSABLE_DEF;
                var max_health = node.DressableStat.DRESSABLE_MAX_HEALTH;
                var health = node.DressableStat.DRESSABLE_HEALTH;
                var hungry = node.DressableStat.DRESSABLE_HUNGRY;
                var parts = node.DressableStat.DRESSABLE_Parts;


                if (parts != Stat.EParts.PARTS_NONE)
                    sb.Append($"장비 부위 : <color=red>{parts}</color>\t");
                if (atk != 0)
                    sb.Append($"공격력 : <color=red>{atk}</color>\t");
                if (range != 0)
                    sb.Append($"사거리 : <color=red>{range}</color>\t");
                if (def != 0)
                    sb.Append($"방어력 : <color=red>{def}</color>\t");
                if (max_health != 0)
                    sb.Append($"최대 체력 : <color=red>{max_health}</color>\t");
                if (health != 0)
                    sb.Append($"체력 : <color=red>{health}</color>\t");
                if (hungry != 0)
                    sb.Append($"허기 : <color=red>{hungry}</color>\t");

                return sb.ToString();
            }
        }

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