using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsolateIsland.Runtime.Inventory;
using IsolateIsland.Runtime.Combination;

namespace IsolateIsland.Runtime.TestCodes
{
    //[ExecuteInEditMode]
    public class SpawnerItemTest : MonoBehaviour
    {
        [SerializeField] private Transform spawnRangeObject;
        private float spawnRange
        {
            get
            {
                float temp = Vector3.Distance(spawnRangeObject.position, gameObject.transform.position);
                return Random.Range(-temp, temp);
            }
        }
        private Vector3 randomPos
        { 
            get
            {
                return new Vector3(spawnRange + gameObject.transform.position.x, spawnRange + gameObject.transform.position.y, 0);
            }
        }
        

        public List<CombinationNode> spawnObject = new List<CombinationNode>();

        private void Start() => SpawnItemAll();

        private void SpawnItemAll()
        {
            foreach (var item in spawnObject)
            {
                for (int i = 0; i < 2/*ComvinationNode에 아이템의 최대 드랍 개수가 지정되면, 그 값을 넣음*/; i++)
                {
                    SpawnItemOne(item);
                }
            }
        }

        private void SpawnItemOne(CombinationNode @base)
        {
            ItemBuilder itemBuilder = new ItemBuilder();
            ItemBase itemBase = itemBuilder.
                SetCombinationNode(@base).
                Build();

            itemBase.transform.position = randomPos;
            itemBase.gameObject.SetActive(true);
        }
        
        private void SpawnItemOne(int index)
        {
            ItemBuilder itemBuilder = new ItemBuilder();
            ItemBase itemBase = itemBuilder.
                SetCombinationNode(spawnObject[index]).
                Build();

            itemBase.transform.position = randomPos;
            itemBase.gameObject.SetActive(true);
        }
    }
}
