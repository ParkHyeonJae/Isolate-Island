using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace IsolateIsland.Runtime.Inventory
{
    [RequireComponent(
        typeof(Button)
        , typeof(Image))]
    public class UI_InventoryAttributeSetter : MonoBehaviour
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

        //private void OnEnable() => OnReset();

        public void OnReset()
        {
            image.sprite = null;
            button.onClick.RemoveAllListeners();
        }

        public void SetAttribute(ItemBase @base, Action<ItemBase> onClickHandler)
        {
            image.sprite = @base.CombinationNode.sprite;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => onClickHandler?.Invoke(@base));
        }



    }
}