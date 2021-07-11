using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsolateIsland.Runtime.Event;

namespace IsolateIsland.Runtime.TestCodes
{

    public class SpawnerObject : MonoBehaviour
    {
        [SerializeField] private Transform spawnRangeObject;
        [SerializeField] private bool isPlayStartSpawn = false;
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

        [SerializeField] private List<GameObject> _object = new List<GameObject>();
        [SerializeField] [Range(0, 50)] private int _startAmount;
        [SerializeField] [Range(0, 50)] private int _reFillAmount;

        private void Start()
        {
            _reFillAmount = Mathf.RoundToInt(_startAmount * 0.5f);

            if (isPlayStartSpawn)
                SpawnObjectAll();

            Managers.Managers.Instance.Event.GetListener<OnChangeDayOrNightEvent>().Subscribe(RefillObjectAll);
        }

        private void SpawnObjectAll()
        {
            for (int i = 0; i < _startAmount; i++)
            {
                var item = _object[Random.Range(0, _object.Count)];
                SpawnObjectOne(item);
            }
        }

        private void RefillObjectAll()
        {
            for (int i = 0; i < _reFillAmount; i++)
            {
                var item = _object[Random.Range(0, _object.Count)];
                SpawnObjectOne(item);
            }

            _reFillAmount = Mathf.RoundToInt(_reFillAmount * 1.1f);
        }

        private void SpawnObjectOne(GameObject @object)
        {
            Managers.Managers.Instance.Pool.Instantiate(@object).transform.position = randomPos;
        }
    }
}

