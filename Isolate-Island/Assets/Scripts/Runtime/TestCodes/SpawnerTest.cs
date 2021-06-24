using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.TestCodes
{
    [ExecuteInEditMode]
    public class SpawnerTest : MonoBehaviour
    {
        public enum Selector
        {
            Enemy,
            Item,
        }

        public Selector nowSpawn;
    }
}
