using IsolateIsland.Runtime.Combination;
using IsolateIsland.Runtime.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering.Universal;

namespace IsolateIsland.Runtime.Inventory
{
    public class ItemBuilder : IBuilder<ItemBase>
    {
        GameObject _itemObject;
        ItemBase _itemBase;
        CombinationNode _combinationNode;

        public ItemBuilder()
        {
        }



        public ItemBuilder SetCombinationNode(CombinationNode node)
        {
            _combinationNode = node;
            return this;
        }

        public ItemBuilder SetItemBase<T>() where T : ItemBase
        {
            if (_itemObject is null)
                _itemObject = new GameObject();
            _itemBase = _itemObject.GetOrAddComponent<T>();
            return this;
        }

        public ItemBuilder AddShadowCaster2D()
        {
            _itemObject.GetOrAddComponent<ShadowCaster2D>();
            return this;
        }

        public ItemBase Build()
        {
            if (_itemObject is null)
                _itemObject = new GameObject();
            if (_itemBase is null)
                _itemBase = _itemObject.GetOrAddComponent<ItemBase>();
            


            _itemObject.SetActive(false);
            
            if (!_combinationNode)
                return _itemBase;

            _itemObject.name = _combinationNode.name;
            _itemBase.CombinationNode = _combinationNode;
            _itemBase.SetSprite();

            return _itemBase;
        }
    }

    [DisallowMultipleComponent]
    [RequireComponent(
          typeof(BoxCollider2D)
        , typeof(ItemInvoker)
        , typeof(SpriteRenderer))]
    public class ItemBase : MonoBehaviour
    {
        [SerializeField] protected CombinationNode _combinationNode;

        public CombinationNode CombinationNode
        {
            get => _combinationNode;
            set => _combinationNode = value;
        }

        public static explicit operator int(ItemBase @base) => @base.CombinationNode.name.GetHashCode();
        public static explicit operator string(ItemBase @base) => @base.CombinationNode.name;

        private SpriteRenderer _spriteRenderer;
        public SpriteRenderer SpriteRenderer => _spriteRenderer = _spriteRenderer ?? GetComponent<SpriteRenderer>();


        private void Awake() {
            Initalize();
            SetSprite();
        }
        protected virtual void Initalize() { }

        public void SetSprite()
        {
            if (CombinationNode)
                SpriteRenderer.sprite = CombinationNode.sprite;
            SpriteRenderer.sortingLayerName = "Item";
            SpriteRenderer.sortingOrder = 1;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"=== {gameObject.name} ===\n");
            sb.Append("Require Combination Node List = \n");
            foreach (var combinationNode in CombinationNode.combinationNodes)
            {
                sb.Append($"{combinationNode.Name} : {combinationNode.Count}개\n");
            }
            return sb.ToString();
        }
    }
}