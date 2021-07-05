using IsolateIsland.Runtime.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Managers
{
    public class PoolManager : IManagerInit
    {
        public Dictionary<GameObject, ObjectPool> PoolObjects = new Dictionary<GameObject, ObjectPool>(new Comparers.ObjectComparer());
        
        private Transform parentTransform = null;
        public readonly int defaultInstaniateCount = 5;

        public void OnInit()
        {
            parentTransform = new GameObject("@Pool").transform;
        }

        GameObject AddPoolable(GameObject pooledObject, GameObject prefab)
        {
            pooledObject.GetOrAddComponent<Poolable>().Prefab = prefab;
            return pooledObject;
        }

        public GameObject Instantiate(GameObject prefab)
        {
            if (prefab is null)
                return default(GameObject);

            ObjectPool pool;

            if (PoolObjects.TryGetValue(prefab, out pool))
                return AddPoolable(pool.pop(), prefab);

            PoolObjects.Add(prefab, pool = new ObjectPool(prefab, defaultInstaniateCount, parentTransform));

            return AddPoolable(pool.pop(), prefab);
        }

        public GameObject Instantiate(string poolPrefabName)
        {
            if (string.IsNullOrEmpty(poolPrefabName))
                return default(GameObject);

            var prefab = Managers.Instance.Resource.Load<GameObject>(poolPrefabName);

            if (prefab is null)
                return default(GameObject);

            return Instantiate(prefab);
        }

        public GameObject ParticleInstantiate(GameObject prefab, float Time)
        {
            var newObject = Instantiate(prefab);
            Managers.Instance.Util.AddTimer(Time, () => Destroy(newObject));
            return newObject;
        }
        
        public GameObject ParticleInstantiate(string poolPrefabName, float Time)
        {
            var newObject = Instantiate(poolPrefabName);
            Managers.Instance.Util.AddTimer(Time, () => Destroy(newObject));
            return newObject;
        }

        

        public void Destroy(GameObject gameObject)
        {
            if (gameObject is null)
                return;

            ObjectPool pool;

            var poolable = gameObject.GetComponent<Poolable>();

            if (poolable is null)
                return;

            if (PoolObjects.TryGetValue(poolable.Prefab, out pool))
                pool.push(gameObject);

        }
    }
}