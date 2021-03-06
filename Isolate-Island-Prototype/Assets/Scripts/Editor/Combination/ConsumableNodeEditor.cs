using IsolateIsland.Runtime.Combination;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace IsolateIsland.Editor.Combination
{
    using Editor = UnityEditor.Editor;

    [CustomEditor(typeof(ConsumableNode))]
    public class ConsumableNodeEditor : Editor
    {
        static ConsumableNode _consumableNode;

        private void OnEnable()
        {
            _consumableNode = (ConsumableNode)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
        }

    }
}