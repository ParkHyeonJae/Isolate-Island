using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Combination
{
    [CreateAssetMenu(fileName = nameof(CombinationNode)
        , menuName = "Combination/" + nameof(CombinationNode)
        , order = int.MaxValue)]
    public class CombinationNode : ScriptableObject
    {
        public Sprite sprite;

        [TextArea]
        public string description = "";
        [System.Serializable]
        public struct Node
        {
            public string Name;
            public int Count;
            public CombinationNode combinationNode;
        }
        public Node[] combinationNodes;

    }

}