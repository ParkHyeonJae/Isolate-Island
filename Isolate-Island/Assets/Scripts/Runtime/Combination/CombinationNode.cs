using UnityEngine;

namespace IsolateIsland.Runtime.Combination
{
    [CreateAssetMenu(fileName = nameof(CombinationNode)
        , menuName = "Combination/" + nameof(CombinationNode)
        , order = int.MaxValue)]
    public class CombinationNode : ScriptableObject
    {
        public Sprite sprite;
        public Sprite invetorySprite;

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

        [Space(20)]
        [Tooltip("조합했을 시 생성될 수 있는 아이템 개수")]
        public int ProductCount = 1;
    }

}