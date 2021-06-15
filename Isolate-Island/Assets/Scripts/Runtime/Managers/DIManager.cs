using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace IsolateIsland.Runtime.Managers
{
    public class DIManager : IManagerInit
    {
        private Dictionary<string, GameObject> _objects;
        public Dictionary<string, GameObject> Objects => _objects = _objects ?? new Dictionary<string, GameObject>(new StringComparer());
        

        private Dictionary<string, Component[]> _components;
        public Dictionary<string, Component[]> Components => _components = _components ?? new Dictionary<string, Component[]>(new StringComparer());

        class StringComparer : IEqualityComparer<string>
        {
            public bool Equals(string x, string y)
            {
                return x.GetHashCode() == y.GetHashCode();
            }

            public int GetHashCode(string obj)
            {
                return obj.GetHashCode();
            }
        }


        public void OnInit()
        {
            CachingObjectByEnumField<Utils.Defines.Load_Object>();
        }


        private void CachingObjectByEnumField<T>()
        {
            var elements = Enum.GetNames(typeof(T));
            foreach (var element in elements)
            {
                var findObject = GameObject.Find(element);

                if (findObject == null)
                    continue;

                if (Objects.ContainsKey(element))
                    continue;

                Objects.Add(element, findObject);
            }
        }

        public GameObject Get<T>(in T enumField) where T : Enum
        {
            var enumFieldName = enumField.ToString();
            GameObject go = null;
            if ((go = Get(enumFieldName)) != null)
                return go;

            this.CachingObjectByEnumField<T>();

            return Get(enumFieldName);
        }

        [Obsolete("WRONG METHOD")]
        public FROM Get<TO, FROM>(in TO enumField) 
            where TO : Enum 
            where FROM : MonoBehaviour
        {
            var enumFieldName = enumField.ToString();
            GameObject go = null;
            if ((go = Get(enumFieldName)) != null)
                return Get<FROM>(go);

            this.CachingObjectByEnumField<TO>();

            return Get<FROM>(go = Get(enumFieldName));
        }

        public GameObject Get(Utils.Defines.Load_Object objects)
        {
            return Objects[objects.ToString()];
        }

        public GameObject Get(in string findName)
        {
            GameObject findObject = null;
            if (!Objects.TryGetValue(findName, out findObject))
                return default(GameObject);

            return findObject;
        }

        public T Get<T>() where T : MonoBehaviour
        {
            T component;
            var name = typeof(T).ToString();
            if (Components.ContainsKey(name))
                return (Components[name] as T[])[0];

            if (((component = GameObject.FindObjectOfType<T>()) is null))
                return default(T);

            Components.Add(name, new[] { component });
            return component;
        }

        [Obsolete("WRONG METHOD")]
        public T Get<T>(GameObject go) where T : MonoBehaviour
        {
            T component;
            var name = typeof(T).ToString();

            if (Components.ContainsKey(name))
                return (Components[name] as T[])[0];

            if (((component = go.GetComponent<T>()) is null))
                return default(T);

            Components.Add(name, new[] { component });
            return component;
        }
        public T[] Gets<T>(in bool reload = false) where T : MonoBehaviour
        {
            T[] component;
            var name = typeof(T).ToString();

            switch (reload)
            {
                case true:
                    if (((component = GameObject.FindObjectsOfType<T>()) is null))
                        return default(T[]);


                    if (!Components.ContainsKey(name))
                    {
                        Components.Add(name, component);
                        return component;
                    }

                    Components[name] = component;

                    return component;
                case false:

                    if (Components.ContainsKey(name))
                        return Components[name] as T[];

                    if (((component = GameObject.FindObjectsOfType<T>()) is null))
                        return default(T[]);

                    Components.Add(name, component);

                    return component;
                default:
                    return default(T[]);
            }

            
        }



    }

}