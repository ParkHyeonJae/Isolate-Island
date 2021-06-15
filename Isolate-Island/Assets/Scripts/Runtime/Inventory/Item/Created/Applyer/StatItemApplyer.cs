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
            var converted = item as StatItem;

            var effectStat = Managers.Managers.Instance.statManager.UserStat;
            converted.StatCombinationNode.Stat.Apply(ref effectStat);
            Managers.Managers.Instance.statManager.UserStat = effectStat;
            Debug.Log(Managers.Managers.Instance.statManager.UserStat.ToString());
        }

        internal override void Drop<T>(in T item)
        {
            base.Drop<T>(item);
            Debug.Log("StatItemApplyer : Drop");
        }
    }
}