using IsolateIsland.Runtime.Combination;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace IsolateIsland.Editor.Combination
{
    public class ItemSettingEditor : EditorWindow
    {
        [MenuItem("Custom/ItemSettings")]
        static void Init()
        {
            ItemSettingEditor itemSettingEditor = GetWindow<ItemSettingEditor>(typeof(ItemSettingEditor));
            itemSettingEditor.Show();
        }

        DressableCombinationNode scriptableObject;
        Transform baseTransform;
        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();

            EditorGUILayout.LabelField("Input Item Scritpable Object");
            baseTransform = EditorGUILayout.ObjectField("Input Base Setting Transform", baseTransform, typeof(Transform), true) as Transform;
            scriptableObject = EditorGUILayout.ObjectField("Input Item Scritpable Object", scriptableObject, typeof(DressableCombinationNode), true) as DressableCombinationNode;
            if (GUILayout.Button("Move To Setting Transform"))
            {
                scriptableObject.OnDressableSetting.Position = baseTransform.localPosition;
                scriptableObject.OnDressableSetting.Rotation = baseTransform.localRotation.eulerAngles;
                scriptableObject.OnDressableSetting.Scale    = baseTransform.localScale;

                scriptableObject.OnDressableSetting.ColliderOffset = baseTransform.GetComponent<BoxCollider2D>().offset;
                scriptableObject.OnDressableSetting.ColliderSize   = baseTransform.GetComponent<BoxCollider2D>().size;
            }
            
            EditorGUILayout.Space(20, true);

            EditorGUILayout.EndVertical();
        }

    }
}
#endif