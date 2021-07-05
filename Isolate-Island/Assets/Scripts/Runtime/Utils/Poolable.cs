using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Utils
{
    public class Poolable : MonoBehaviour
    {
        private GameObject _prefab;
        public GameObject Prefab
        {
            get
            {
                if (_prefab is null)
                    Debug.LogError($"Cannot Have Poolable Prefab : {this.transform.name}");

                return _prefab;
            }

            set => _prefab = value;
        }
    }
}
