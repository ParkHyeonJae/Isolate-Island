using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemyTest : MonoBehaviour
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
            return new Vector3(spawnRange + gameObject.transform.position.x, spawnRange + gameObject.transform.position.y, -1);
        }
    }

    private static Queue<GameObject> _poolQueue;

    [SerializeField] private GameObject _object;
    [SerializeField] [Range(0, 50)] private int _startAmount;

    private void Start()
    {
        _poolQueue = new Queue<GameObject>();

        GameObject obj = Instantiate(_object);
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        _poolQueue.Enqueue(obj);

        SpawnEnemyAll();
    }

    private void SpawnEnemyAll()
    {
        for (int i = 0; i < _startAmount; i++)
        {
            SpawnEnemyOne();
        }
    }

    private void SpawnEnemyOne()
    {
        SpawnPool(randomPos);
    }

    public static GameObject SpawnPool(Vector3 pos)
    {
        foreach (GameObject obj in _poolQueue)
        {
            if (!obj.activeSelf)
            {
                GameObject objectSpawn = _poolQueue.Dequeue();

                objectSpawn.SetActive(true);
                objectSpawn.transform.position = pos;
                objectSpawn.name = "Enemy";

                _poolQueue.Enqueue(objectSpawn);

                return objectSpawn;
            }
        }

        //print("Is full");
        GameObject newObject = Instantiate(_poolQueue.Peek());

        newObject.transform.SetParent(_poolQueue.Peek().transform.parent);

        newObject.SetActive(true);

        newObject.transform.position = pos;
        newObject.name = "Enemy";

        _poolQueue.Enqueue(newObject);

        return newObject;
    }
}

