using IsolateIsland.Runtime.Combination;
using UnityEditor;
using UnityEngine;

namespace IsolateIsland.Editor.Combination
{
    using Editor = UnityEditor.Editor;

    [CustomEditor(typeof(CombinationNode))]
    public class NodeEditor : Editor
    {
        static CombinationNode _combinationNode;

        private void OnEnable()
        {
            _combinationNode = (CombinationNode)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            _combinationNode.sprite = EditorGUILayout.ObjectField("Sprite", _combinationNode.sprite, typeof(Sprite), true) as Sprite;
        }

    }
}