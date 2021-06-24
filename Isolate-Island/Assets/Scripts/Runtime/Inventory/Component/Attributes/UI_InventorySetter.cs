using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IsolateIsland.Runtime.Inventory
{
    public abstract class UI_InventorySetter : MonoBehaviour
    {
        public int Index
        {
            get
            {
                var parent = transform.parent;
                int i = 0;
                for (; i < parent.childCount; i++)
                    if (parent.GetChild(i) == transform)
                        break;
                return i;
            }
        }

        private Image _image;
        public Image image => _image = _image ?? GetComponent<Image>();

        private Button _button;
        public Button button => _button = _button ?? GetComponent<Button>();


        public virtual void OnReset()
        {
            image.sprite = null;
            button.onClick.RemoveAllListeners();
        }

        public virtual void SetAttribute(ItemBase @base, Action<ItemBase> onClickHandler)
        {
            image.sprite = @base.CombinationNode.sprite;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => onClickHandler?.Invoke(@base));
        }
    }
}