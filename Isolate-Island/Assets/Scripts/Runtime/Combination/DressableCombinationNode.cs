using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Combination
{
    [CreateAssetMenu(fileName = nameof(DressableCombinationNode)
        , menuName = "Combination/" + nameof(DressableCombinationNode)
        , order = int.MaxValue)]
    public class DressableCombinationNode : StatCombinationNode
    {
        [HideInInspector]
        public Stat.DressableStat DressableStat;


    }
}