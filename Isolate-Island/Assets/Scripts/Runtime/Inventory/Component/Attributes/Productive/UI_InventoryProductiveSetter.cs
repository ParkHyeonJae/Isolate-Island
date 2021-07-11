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
            image.sprite = node.invetorySprite;
            image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => onClickHandler?.Invoke(node));
        }
    }
}