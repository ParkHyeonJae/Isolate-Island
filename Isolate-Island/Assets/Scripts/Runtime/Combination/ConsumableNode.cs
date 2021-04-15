using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Combination
{
    public interface IConsumable
    {
        void Consume();
    }

    [CreateAssetMenu(fileName = nameof(ConsumableNode)
    , menuName = "Combination/" + nameof(ConsumableNode)
    , order = int.MaxValue)]
    public class ConsumableNode : CombinationNode, IConsumable
    {


        public void Consume()
        {
            
        }
    }
}