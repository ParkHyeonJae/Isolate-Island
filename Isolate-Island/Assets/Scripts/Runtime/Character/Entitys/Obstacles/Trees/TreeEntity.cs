using IsolateIsland.Runtime.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Character
{
    public class TreeInteractType : InteractType
    {
        public static readonly InteractType TreeInteract = new InteractType("Animation@TreeInteract");

        public TreeInteractType(string value) : base(value) { }
    }

    public class TreeEntity : BreakableEntity
    {
        public override void Initalize()
        {
            base.Initalize();
        }

        protected override void Reward()
        {
            base.Reward();

            ItemBuilder itemBuilder = new ItemBuilder();
            var stick = Managers.Managers.Instance.Pool.Instantiate("나뭇가지");
            stick.gameObject.SetActive(true);
            stick.transform.position = transform.position;
        }
        public override void OnInteractableEvent(Entity entity)
        {
            base.OnInteractableEvent(entity);

            if (IsBreak == true)
                return;
            Managers.Managers.Instance.Sound.PlayOneShot("나무 상호작용");
            AnimatePlayFactory(TreeInteractType.TreeInteract);
        }

        protected override bool AnimatePlayFactory(InteractType interactType)
        {
            if (interactType == TreeInteractType.TreeInteract)
            {
                animator.Play(interactType.Value);
                return true;
            }

            base.AnimatePlayFactory(interactType);
            return false;
        }
    }
}