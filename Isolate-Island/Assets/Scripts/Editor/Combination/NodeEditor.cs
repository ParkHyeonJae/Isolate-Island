using IsolateIsland.Runtime.Combination;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

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
            _combinationNode.invetorySprite = EditorGUILayout.ObjectField("Invetory Sprite Info", _combinationNode.invetorySprite, typeof(Sprite), true) as Sprite;
            EditorUtility.SetDirty(_combinationNode);
        }

    }
}
#endif