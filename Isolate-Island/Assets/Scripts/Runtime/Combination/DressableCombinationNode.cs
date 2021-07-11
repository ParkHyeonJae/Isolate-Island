using IsolateIsland.Runtime.Stat;
using IsolateIsland.Runtime.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace IsolateIsland.Runtime.Combination
{
    [CreateAssetMenu(fileName = nameof(DressableCombinationNode)
        , menuName = "Combination/" + nameof(DressableCombinationNode)
        , order = int.MaxValue)]
    public class DressableCombinationNode : StatCombinationNode, IStatAble
    {
        [HideInInspector]
        public Stat.DressableStat DressableStat;

        [System.Serializable]
        public class DressableFormattingForm
        {
            public Vector3 Position;
            public Vector3 Rotation;
            public Vector3 Scale = Vector3.one;

            [Space(20)]
            public Vector2 ColliderOffset = Vector2.zero;
            public Vector2 ColliderSize = Vector2.one;
        }

        public DressableFormattingForm OnDressableSetting;

        [Space(20)]
        public bool IsComsumable = false;
        
        public override string GetStatInfo()
        {
            var node = this;
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


            if (parts != EParts.PARTS_NONE)
                sb.Append($"장비 부위 : <color=red>{parts}</color>\t");
            if (atk != 0 || atk == -1)
                sb.Append($"공격력 : <color=red>{(atk == -1 ? 0 : atk)}</color>\t");
            if (range != 0 || atk == -1)
                sb.Append($"사거리 : <color=red>{(range == -1 ? 0 : range)}</color>\t");
            if (def != 0 || atk == -1)
                sb.Append($"방어력 : <color=red>{(def == -1 ? 0 : def)}</color>\t");
            if (max_health != 0 || atk == -1)
                sb.Append($"최대 체력 : <color=red>{(max_health == -1 ? 0 : max_health)}</color>\t");
            if (health != 0 || atk == -1)
                sb.Append($"체력 : <color=red>{(health == -1 ? 0 : health)}</color>\t");
            if (hungry != 0 || atk == -1)
                sb.Append($"허기 : <color=red>{(hungry == -1 ? 0 : hungry)}</color>\t");
            
            return sb.ToString();
        }

    }
}