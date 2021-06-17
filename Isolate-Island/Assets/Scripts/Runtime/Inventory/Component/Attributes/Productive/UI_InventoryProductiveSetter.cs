using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IsolateIsland.Runtime.Inventory
{
    [RequireComponent(typeof(Button))]
    public class UI_InventoryProductiveSetter : UI_InventorySetter
    {
        public virtual void SetProductiveAttribute(Combination.CombinationNode node, Action<Combination.CombinationNode> onClickHandler)
        {
            image.sprite = node.sprite;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => onClickHandler?.Invoke(node));
        }
    }
}