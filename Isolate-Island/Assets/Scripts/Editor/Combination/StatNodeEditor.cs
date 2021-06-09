using IsolateIsland.Runtime.Combination;
using IsolateIsland.Runtime.Stat;
using System.Reflection;
using System;
using UnityEditor;
using UnityEngine;
using System.Linq.Expressions;

namespace IsolateIsland.Editor.Combination
{

    using Editor = UnityEditor.Editor;

    [CustomEditor(typeof(StatCombinationNode))]
    public class StatNodeEditor : NodeEditor
    {
        static StatCombinationNode _combinationNode;
        static EAbilityStatType eAbilityStatType;

        protected override void OnEditorInitalize()
        {
            base.OnEditorInitalize();

            _combinationNode = (StatCombinationNode)target;
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
            propertyInfo.SetValue(_combinationNode.Stat, selected);
            EditorUtility.SetDirty(_combinationNode);
            //AssetDatabase.Refresh();
        }

        private void SetPropertyValueByType<T>(FieldInfo propertyInfo,  T value)
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

            eAbilityStatType 
                = (EAbilityStatType)EditorGUILayout.EnumPopup(
                    eAbilityStatType, GUILayout.ExpandWidth(true));

            if (_combinationNode == null || _combinationNode.Stat == null)
                return;

            foreach (var propertyInfo in _combinationNode.Stat.GetType()?.GetFields(
                  BindingFlags.Public 
                | BindingFlags.NonPublic
                | BindingFlags.Instance))
            {
                var value = propertyInfo.GetValue(_combinationNode.Stat);
                
                var splitProperty = propertyInfo.Name.Split('_');
                var enumNames = Enum.GetNames(typeof(EAbilityStatType));

                foreach (var name in enumNames)
                {
                    if (splitProperty[0] != name)
                        continue;

                    if (eAbilityStatType.ToString() != name)
                        continue;

                    EditorGUILayout.LabelField(propertyInfo.Name);

                    SetPropertyValueByType(propertyInfo, value);

                }

                
            }

            

        }

    }
}