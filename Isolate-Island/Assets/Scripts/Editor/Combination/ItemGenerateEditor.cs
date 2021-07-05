using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using IsolateIsland.Runtime.Inventory;
using IsolateIsland.Runtime.Combination;

#if UNITY_EDITOR
namespace IsolateIsland.Editor.Utils
{
    using EditorWindow = UnityEditor.EditorWindow;
    public class ItemGenerateEditor : EditorWindow
    {

        [MenuItem("Custom/ItemGenerator")]
        static void Init()
        {
            ItemGenerateEditor itemGenerateEditor = GetWindow<ItemGenerateEditor>(typeof(ItemGenerateEditor));
            itemGenerateEditor.Show();
        }

        CombinationNode scriptableObject;
        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            
            EditorGUILayout.LabelField("Input Item Scritpable Object");
            scriptableObject = EditorGUILayout.ObjectField("Input Item Scritpable Object", scriptableObject, typeof(CombinationNode), true) as CombinationNode;
            if (GUILayout.Button("Generate Default"))
            {
                ItemBuilder itemBuilder = new ItemBuilder();
                itemBuilder.
                    SetCombinationNode(scriptableObject)
                    .AddShadowCaster2D()
                    .Build();

            }if (GUILayout.Button("Generate Stat"))
            {
                ItemBuilder itemBuilder = new ItemBuilder();
                itemBuilder.
                    SetCombinationNode(scriptableObject)
                    .SetItemBase<StatItem>()
                    .AddShadowCaster2D()
                    .Build();

            }if (GUILayout.Button("Generate Dressable"))
            {
                ItemBuilder itemBuilder = new ItemBuilder();
                itemBuilder.
                    SetCombinationNode(scriptableObject)
                    .SetItemBase<DressableItem>()
                    .AddShadowCaster2D()
                    .Build();

            }if (GUILayout.Button("Generate Weapon"))
            {
                ItemBuilder itemBuilder = new ItemBuilder();
                itemBuilder.
                    SetCombinationNode(scriptableObject)
                    .SetItemBase<WeaponItem>()
                    .AddShadowCaster2D()
                    .Build();

            }if (GUILayout.Button("Generate Proximity"))
            {
                ItemBuilder itemBuilder = new ItemBuilder();
                itemBuilder.
                    SetCombinationNode(scriptableObject)
                    .SetItemBase<ProximityWeaponItem>()
                    .AddShadowCaster2D()
                    .Build();

            }

            EditorGUILayout.Space(20, true);

            EditorGUILayout.EndVertical();
        }

    }
}
#endif