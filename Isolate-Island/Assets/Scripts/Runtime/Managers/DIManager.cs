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
            var elements = Enum.GetNames(typeof(Utils.Defines.Load_Object));
            foreach (var element in elements)
            {
                var findObject = GameObject.Find(element);

                if (findObject == null)
                    continue;

                Objects.Add(element, findObject);
            }
        }

        public GameObject Get(Utils.Defines.Load_Object objects)
        {
            return Objects[objects.ToString()];
        }

        public T Get<T>() where T : MonoBehaviour
        {
            T component;
            if (Components.ContainsKey(nameof(T)))
                return (Components[nameof(T)] as T[])[0];

            if (((component = GameObject.FindObjectOfType<T>()) is null))
                return default(T);

            Components.Add(nameof(T), new[] { component });
            return component;
        }
        public T[] Gets<T>(in bool reload = false) where T : MonoBehaviour
        {
            T[] component;
            
            switch (reload)
            {
                case true when reload == true:
                    if (((component = GameObject.FindObjectsOfType<T>()) is null))
                        return default(T[]);


                    if (!Components.ContainsKey(nameof(T)))
                    {
                        Components.Add(nameof(T), component);
                        return component;
                    }

                    Components[nameof(T)] = component;

                    return component;
                case true when reload == false:

                    if (Components.ContainsKey(nameof(T)))
                        return Components[nameof(T)] as T[];

                    if (((component = GameObject.FindObjectsOfType<T>()) is null))
                        return default(T[]);

                    Components.Add(nameof(T), component);

                    return component;
                default:
                    return default(T[]);
            }

            
        }



    }

}