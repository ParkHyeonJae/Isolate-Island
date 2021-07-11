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
                for (int i = 0; i < Random.Range(1, item.DropCount); i++)
                {
                    SpawnItemOne(item);
                }
            }
        }
        
        private void SpawnItemOne(CombinationNode @base)
        {
            //ItemBuilder itemBuilder = new ItemBuilder();
            //ItemBase itemBase = itemBuilder.
            //    SetCombinationNode(@base).
            //    Build();
            var itemBase = Managers.Managers.Instance.Pool.Instantiate(@base.name);

            itemBase.transform.position = randomPos;
            itemBase.transform.SetParent(transform);
            itemBase.gameObject.SetActive(true);
        }
        
        private void SpawnItemOne(int index)
        {
            //ItemBuilder itemBuilder = new ItemBuilder();
            //ItemBase itemBase = itemBuilder.
            //    SetCombinationNode(spawnObject[index]).
            //    Build();
            var itemBase = Managers.Managers.Instance.Pool.Instantiate(spawnObject[index].name);

            itemBase.transform.position = randomPos;
            itemBase.transform.SetParent(transform);
            itemBase.gameObject.SetActive(true);
        }
    }
}
