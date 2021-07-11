using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace IsolateIsland.Runtime.Hud
{
    public class InventoryHud : MonoBehaviour
    {
        [SerializeField] private RectTransform _bag;
        [SerializeField] private GameObject _bagOutPanel;

        private bool _isOpen = false;
        public Ease openEase;
        public Ease closeEase;

        public void OpenInventory()
        {
            if (!_isOpen)
            {
                _bag.DOAnchorPosY(1000, 0.5f).SetEase(openEase);
                _isOpen = true;
            }
            else
            {
                _bag.DOAnchorPosY(0, 0.5f).SetEase(closeEase);
                _isOpen = false;
            }
            _bagOutPanel.SetActive(_isOpen);
        }
    }
}