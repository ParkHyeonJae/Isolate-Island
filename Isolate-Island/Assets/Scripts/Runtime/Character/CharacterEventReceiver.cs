using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Character
{
    public class CharacterEventReceiver : MonoBehaviour
    {



        public void OnEnterAttack()
        {
            var itemBase = Managers.Managers.Instance.Inventory.Dressable.GetParts(Stat.EParts.PARTS_LEFT_HAND);
            Debug.Log("Enter Attack");
        }

        public void OnExitAttack()
        {
            Debug.Log("Exit Attack");
        }


    }
}
