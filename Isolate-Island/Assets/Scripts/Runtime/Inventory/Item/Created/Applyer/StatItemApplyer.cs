using IsolateIsland.Runtime.Combination;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Inventory
{
    internal class StatItemApplyer : ItemApplyer
    {
        
        internal override void Use<T>(in T item)
        {
            base.Use<T>(item);
            Debug.Log("StatItemApplyer : Use");
            var converted = item.CombinationNode as StatCombinationNode;
            
            if (converted)
            {
                var effectStat = Managers.Managers.Instance.statManager.UserStat;
                converted.Stat.ApplyEffect(ref effectStat);
                Managers.Managers.Instance.statManager.UserStat = effectStat;
                Debug.Log(Managers.Managers.Instance.statManager.UserStat.ToString());
                Managers.Managers.Instance.Sound.PlayOneShot("음식 아이템 사용");
            }
        }

        internal override void Drop<T>(in T item)
        {
            base.Drop<T>(item);
            Debug.Log("StatItemApplyer : Drop");
        }
    }
}