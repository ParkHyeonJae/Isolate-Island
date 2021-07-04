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

    [SerializeField] private GameObject _object;
    [SerializeField] [Range(0, 50)] private int _startAmount;

    private void Start() => SpawnEnemyAll();

    private void SpawnEnemyAll()
    {
        for (int i = 0; i < _startAmount; i++)
        {
            SpawnEnemyOne();
        }
    }

    private void SpawnEnemyOne()
    {
        GameObject @object = Instantiate(_object);

        @object.transform.position = randomPos;
        @object.gameObject.SetActive(true);
    }
}

