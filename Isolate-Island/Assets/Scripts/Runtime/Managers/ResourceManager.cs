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
        public Dictionary<string, AudioClip> MemcachedSounds = new Dictionary<string, AudioClip>(new Comparers.StringStateComparer());

        public void OnInit()
        {
            CachingAll("Prefabs", ref MemcachedObjects);
            CachingAll("Sprites", ref MemcachedSprites);
            CachingAll("Sounds", ref MemcachedSounds);
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

        public T Compare_And_Add_Dictionary<T, COMPARE>(ref Dictionary<string, COMPARE> memcached, string name) 
            where T : Object
        {
            if (typeof(T) == typeof(COMPARE))
            {
                if (memcached.TryGetValue(name, out COMPARE obj))
                    return obj as T;
            }
            return default(T);
        }

        public T Load<T>(string path) where T : Object
        {
            if (string.IsNullOrEmpty(path))
                return default(T);

            int idx = path.LastIndexOf("/");
            string name = path.Substring(idx + 1);

            T check = default(T);
            check = Compare_And_Add_Dictionary<T, GameObject>(ref MemcachedObjects, name);
            if (check) return check;
            check = Compare_And_Add_Dictionary<T, Sprite>(ref MemcachedSprites, name);
            if (check) return check;
            check = Compare_And_Add_Dictionary<T, AudioClip>(ref MemcachedSounds, name);
            if (check) return check;

            //if (typeof(T) == typeof(GameObject))
            //{
            //    if (MemcachedObjects.TryGetValue(name, out GameObject obj))
            //        return obj as T;

            //    return default(T);
            //}

            //if (typeof(T) == typeof(Sprite))
            //{
            //    if (MemcachedSprites.TryGetValue(name, out Sprite sprite))
            //        return sprite as T;

            //    return default(T);
            //}

            //if (typeof(T) == typeof(AudioClip))
            //{
            //    if (MemcachedSounds.TryGetValue(name, out AudioClip audioClip))
            //        return audioClip as T;

            //    return default(T);
            //}


            return Resources.Load<T>(path);
        }

    }
}