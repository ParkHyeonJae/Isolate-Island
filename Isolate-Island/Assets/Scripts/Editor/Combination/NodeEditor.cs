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

        protected void OnEnable() => OnEditorInitalize();

        protected virtual void OnEditorInitalize()
        {
            _combinationNode = (CombinationNode)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (_combinationNode == null)
                return;
            
            _combinationNode.sprite = EditorGUILayout.ObjectField("Sprite Info", _combinationNode.sprite, typeof(Sprite), true) as Sprite;
            EditorUtility.SetDirty(_combinationNode);
        }

    }
}