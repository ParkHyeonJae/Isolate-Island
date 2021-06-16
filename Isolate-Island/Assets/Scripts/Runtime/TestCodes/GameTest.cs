using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.TestCodes
{
    public class GameTest : MonoBehaviour
    {
        [ContextMenu("ShowUserStat")]
        public void ShowUserStat()
        {
            Debug.Log(Managers.Managers.Instance.statManager.UserStat.ToString());
        }

    }
}