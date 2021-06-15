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
    public class StatItem : ItemBase, IStatAble
    {
        public StatCombinationNode StatCombinationNode => CombinationNode as StatCombinationNode;
        public string GetStatInfo
        {
            get
            {
                var statNode = StatCombinationNode;
                if (statNode == null)
                    return string.Empty;

                StringBuilder sb = new StringBuilder();

                var atk = statNode.Stat.EFFECT_ATK;
                var range = statNode.Stat.EFFECT_RANGE;
                var def = statNode.Stat.EFFECT_DEF;
                var health = statNode.Stat.EFFECT_HEALTH;
                var hungry = statNode.Stat.EFFECT_HUNGRY;


                if (atk != 0)
                    sb.Append($"공격력 : <color=red>{atk}</color>\t");
                if (range != 0)
                    sb.Append($"사거리 : <color=red>{range}</color>\t");
                if (atk != 0)
                    sb.Append($"방어력 : <color=red>{def}</color>\t");
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
    }
}