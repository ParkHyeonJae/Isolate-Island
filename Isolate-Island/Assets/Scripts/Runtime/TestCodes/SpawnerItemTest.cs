using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsolateIsland.Runtime.Inventory;
using IsolateIsland.Runtime.Combination;

namespace IsolateIsland.Runtime.TestCodes
{
    [ExecuteInEditMode]
    public class SpawnerItemTest : MonoBehaviour
    {
        public Transform spawnRangeObject;
        public float spawnRange
        {
            get
            {
                return Vector3.Distance(spawnRangeObject.position, gameObject.transform.position);
            }
        }

        public List<CombinationNode> spawnObject = new List<CombinationNode>();

        private void Start()
        {
            ItemBuilder itemBuilder = new ItemBuilder();
            GameObject itemBase = itemBuilder.
                SetCombinationNode(spawnObject[0]).
                Build().
                gameObject;

            itemBase.transform.position = new Vector3(5, 0, 0);
        }
    }
}
