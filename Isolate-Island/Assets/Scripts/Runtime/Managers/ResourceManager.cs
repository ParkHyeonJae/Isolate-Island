using IsolateIsland.Runtime.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Managers
{
    // Store Prefabs, Sprites ..etc
    public class ResourceManager : IManagerInit
    {

        public Dictionary<string, GameObject> MemcachedObjects = new Dictionary<string, GameObject>(new Comparers.StringStateComparer());
        public Dictionary<string, Sprite> MemcachedSprites = new Dictionary<string, Sprite>(new Comparers.StringStateComparer());

        public void OnInit()
        {
            CachingAll("Prefabs", ref MemcachedObjects);
            CachingAll("Sprites", ref MemcachedSprites);
        }


        void CachingAll<T>(string path, ref Dictionary<string, T> dict) where T : Object
        {
            if (string.IsNullOrEmpty(path))
                return;

            var result = Resources.LoadAll<T>(path);

            if (result is null)
            {
                Debug.LogWarning($"Cannot Found Path : {path}");
                return;
            }

            foreach (var item in result)
                dict.Add(item.name, item);

            Debug.Log($"LoadCompleted : {path}");
        }


        public T Load<T>(string path) where T : Object
        {
            if (string.IsNullOrEmpty(path))
                return default(T);

            int idx = path.LastIndexOf("/");
            string name = path.Substring(idx + 1);

            if (typeof(T) == typeof(GameObject))
            {
                if (MemcachedObjects.TryGetValue(name, out GameObject obj))
                    return obj as T;

                return default(T);
            }

            if (typeof(T) == typeof(Sprite))
            {
                if (MemcachedSprites.TryGetValue(name, out Sprite sprite))
                    return sprite as T;

                return default(T);
            }


            return Resources.Load<T>(path);
        }

    }
}