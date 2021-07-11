using IsolateIsland.Runtime.Combination;
using IsolateIsland.Runtime.Stat;
using System.Reflection;
using System;
using UnityEditor;
using UnityEngine;
using System.Linq.Expressions;

#if UNITY_EDITOR
namespace IsolateIsland.Editor.Combination
{

    using Editor = UnityEditor.Editor;

    [CustomEditor(typeof(DressableCombinationNode))]
    public class DressableNodeEditor : StatNodeEditor
    {
        static DressableCombinationNode _combinationNode;
        static EAbilityDressableType eAbilityDressableType;

        protected override void OnEditorInitalize()
        {
            base.OnEditorInitalize();

            _combinationNode = (DressableCombinationNode)target;
        }

        //private T Editor_RenderFieldByType<T>(T value)
        //{
        //    switch (value)
        //    {
        //        case int @i: return (T)Activator.CreateInstance(typeof(int), EditorGUILayout.IntField(@i));
        //        case Enum @enum: return (T)Activator.CreateInstance(typeof(Enum), EditorGUILayout.EnumPopup(@enum));
        //        default:
        //            return default(T);
        //    }
        //}

        private void SetPropertyValue<T>(FieldInfo propertyInfo, T selected)
        {
            propertyInfo.SetValue(_combinationNode.DressableStat, selected);
            EditorUtility.SetDirty(_combinationNode);
            //AssetDatabase.Refresh();
        }

        private void SetPropertyValueByType<T>(FieldInfo propertyInfo, T value)
        {
            switch (value)
            {
                case int @i:

                    int selected_int = EditorGUILayout.IntField(@i);

                    if (selected_int != @i)
                        SetPropertyValue(propertyInfo, selected_int);
                    break;
                case Enum @enum:
                    Enum selected_Enum = EditorGUILayout.EnumPopup(@enum);
                    if (selected_Enum != @enum)
                        SetPropertyValue(propertyInfo, selected_Enum);
                    break;
                default:
                    break;
            }

        }



        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space(20, true);
            EditorGUILayout.Space(20, true);

            eAbilityDressableType
                = (EAbilityDressableType)EditorGUILayout.EnumPopup(
                    eAbilityDressableType, GUILayout.ExpandWidth(true));

            if (_combinationNode == null || _combinationNode.DressableStat == null)
                return;

            foreach (var propertyInfo in _combinationNode.DressableStat.GetType()?.GetFields(
                  BindingFlags.Public
                | BindingFlags.NonPublic
                | BindingFlags.Instance))
            {
                var value = propertyInfo.GetValue(_combinationNode.DressableStat);

                var splitProperty = propertyInfo.Name.Split('_');
                var enumNames = Enum.GetNames(typeof(EAbilityDressableType));

                foreach (var name in enumNames)
                {
                    if (splitProperty[0] != name)
                        continue;

                    if (eAbilityDressableType.ToString() != name)
                        continue;

                    EditorGUILayout.LabelField(propertyInfo.Name);

                    SetPropertyValueByType(propertyInfo, value);

                }


            }



        }

    }
}
#endif