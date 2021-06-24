using IsolateIsland.Runtime.Combination;
using System;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace IsolateIsland.Runtime.TestCodes
{
    [System.Serializable]
    public struct CombinationTestUISet
    {
        public UnityEngine.UI.Image icon;
        public UnityEngine.UI.Text description;
        public UnityEngine.UI.Text combination;
    }
    public class CombinationTest : MonoBehaviour
    {
        [SerializeField] CombinationTestUISet _combinationTestUISet;

        internal CombinationNode _combinationNode;
        public void SelectNode()
        {
            var list = Managers.Managers.Instance.Combination.GetCombinationList();

            var nodes = list.Where(e => e == _combinationNode);
            bool existNode = false;

            StringBuilder @sb = new StringBuilder();

            foreach (var node in nodes)
            {
                _combinationTestUISet.icon.sprite = node.sprite;
                _combinationTestUISet.description.text = node.description;

                @sb.Append("재료 : \n");
                Array.ForEach(node.combinationNodes, e =>
                {
                    @sb.Append(e.Name);
                    @sb.Append(" : ");
                    @sb.Append(e.Count);
                    @sb.Append("개\n");
                });
                existNode = true;
            }

            if (!existNode)
                sb.Append("재료가 필요없는 오브젝트 입니다");

            _combinationTestUISet.combination.text = @sb.ToString();
        }
    }

    [CustomEditor(typeof(CombinationTest))]
    public class CombinationTestEditor : Editor
    {
        private CombinationTest combinationTest;
        public void OnEnable()
        {
            combinationTest = (CombinationTest)target;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.BeginVertical();

            Editor_InspectorPadding(5, 20);

            combinationTest._combinationNode
                = EditorGUILayout.ObjectField(nameof(CombinationNode)
                , combinationTest._combinationNode
                , typeof(CombinationNode), true) as CombinationNode;

            if (GUI.Button(new Rect(0, 100, 400, 50), "FindNode"))
                combinationTest.SelectNode();

            EditorGUILayout.EndVertical();
        }

        public void Editor_InspectorPadding(int height = 5, int width = 20)
        {
            for (int i = 0; i < height; i++)
                EditorGUILayout.Space(width);
        }
    }
}