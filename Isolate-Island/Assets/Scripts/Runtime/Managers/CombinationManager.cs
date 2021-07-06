using IsolateIsland.Runtime.Combination;
using IsolateIsland.Runtime.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IsolateIsland.Runtime.Managers
{
    public class CombinationManager : IManagerInit
    {
        public string SOPath = "ScriptableObject";

        private Dictionary<string, CombinationNode> _craftingTable;
        public Dictionary<string, CombinationNode> CraftingTable
        {
            get
            {
                if (_craftingTable == null)
                    _craftingTable = new Dictionary<string, CombinationNode>(new Comparers.StringStateComparer());
                return _craftingTable;
            }
        }

        

        public void OnInit()
        {
            var SO = Resources.LoadAll<ScriptableObject>(SOPath);
            Array.ForEach(SO, e => AppendTable(e.name, e as CombinationNode));
        }

        public void AppendTable(string Key, CombinationNode value)
        {
            if (CraftingTable.ContainsKey(Key))
            {
                Debug.LogWarning($"중복된 ScriptableObject 가 존재합니다 ({value.name})");
                return;
            }

            CraftingTable.Add(Key, value);
        }
        public CombinationNode GetCombinationNode(string name)
        {
            if (!CraftingTable.ContainsKey(name))
                return default(CombinationNode);
            return CraftingTable[name];
        }
        public CombinationNode[] GetCombinationList() => CraftingTable.Values.ToArray();

    }
}