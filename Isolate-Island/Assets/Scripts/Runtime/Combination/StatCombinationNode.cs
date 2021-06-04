using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Combination
{
    [CreateAssetMenu(fileName = nameof(StatCombinationNode)
        , menuName = "Combination/" + nameof(StatCombinationNode)
        , order = int.MaxValue)]
    public class StatCombinationNode : CombinationNode
    {
        [HideInInspector]
        public Stat.Stat Stat;

    }
}