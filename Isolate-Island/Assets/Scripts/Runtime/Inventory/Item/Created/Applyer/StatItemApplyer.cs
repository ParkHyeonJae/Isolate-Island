﻿using IsolateIsland.Runtime.Combination;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Inventory
{
    internal class StatItemApplyer : ItemApplyer
    {
        protected bool IsConsumable<T>(in T item) where T : ItemBase
        {
            var converted = item.CombinationNode as StatCombinationNode;
            return converted.Stat.IsConsumable;
        }
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
            }
        }

        internal override void Drop<T>(in T item)
        {
            base.Drop<T>(item);
            Debug.Log("StatItemApplyer : Drop");
        }
    }
}