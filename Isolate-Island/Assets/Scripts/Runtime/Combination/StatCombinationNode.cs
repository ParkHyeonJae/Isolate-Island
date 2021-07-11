using IsolateIsland.Runtime.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace IsolateIsland.Runtime.Combination
{
    [CreateAssetMenu(fileName = nameof(StatCombinationNode)
        , menuName = "Combination/" + nameof(StatCombinationNode)
        , order = int.MaxValue)]
    public class StatCombinationNode : CombinationNode, IStatAble
    {
        [HideInInspector]
        public Stat.EffectStat Stat;

        public virtual string GetStatInfo()
        {
            var statNode = this;
            if (statNode == null)
                return string.Empty;

            StringBuilder sb = new StringBuilder();

            var atk = statNode.Stat.EFFECT_ATK;
            var range = statNode.Stat.EFFECT_RANGE;
            var def = statNode.Stat.EFFECT_DEF;
            var max_health = statNode.Stat.EFFECT_MAX_HEALTH;
            var health = statNode.Stat.EFFECT_HEALTH;
            var hungry = statNode.Stat.EFFECT_HUNGRY;


            if (atk != 0)
                sb.Append($"공격력 : <color=red>{atk}</color>\t");
            if (range != 0)
                sb.Append($"사거리 : <color=red>{range}</color>\t");
            if (atk != 0)
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
}