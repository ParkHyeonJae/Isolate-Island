﻿using IsolateIsland.Runtime.Combination;
using IsolateIsland.Runtime.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.TestCodes
{
    public class ObjectManagerTest : MonoBehaviour
    {

        [SerializeField] CombinationNode combinationNode;
        // Start is called before the first frame update
        void Start()
        {
            //Managers.Managers.Instance.DI.Get(Defines.Load_Object.Player).SetActive(false);

            var inventory = Managers.Managers.Instance.Inventory.Game;
            inventory.PrintItemList();

            //Managers.Managers.Instance.DI.Get<IsolateIsland.Runtime.Inventory.Inventory>().PrintItemList();
            //Managers.Managers.Instance.DI.Get<IsolateIsland.Runtime.Inventory.Inventory>().PrintItemList();
            //Managers.Managers.Instance.DI.Get<IsolateIsland.Runtime.Inventory.Inventory>().PrintItemList();

            

        }


        [ContextMenu("Build")]
        void Invoke()
        {
            ItemBuilder itemBuilder = new ItemBuilder();
            itemBuilder.SetCombinationNode(combinationNode)
                .Build().gameObject.SetActive(true);

            
        }


        [ContextMenu("ResourcesTest")]
        void ResourceLoadTest()
        {
            var prefabs = Resources.LoadAll<GameObject>("Prefabs");
            var sprites = Resources.LoadAll<Sprite>("Sprites");

            
        }


        Stack<GameObject> _onMemeoryObjects = new Stack<GameObject>();

        [ContextMenu("SpawnEnemy")]
        void SpawnEnemy()
        {
            var enemyPrefab = Managers.Managers.Instance.Resource.Load<GameObject>("Enemy");
            var spawn = Managers.Managers.Instance.Pool.Instantiate("Enemy");

            _onMemeoryObjects.Push(spawn);
        }


        [ContextMenu("DeleteEnemy")]
        void DeleteEnemy()
        {
            var popEnemy = _onMemeoryObjects.Pop();
            Managers.Managers.Instance.Pool.Destroy(popEnemy);
        }
        
    }
}