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
            var stick = Managers.Managers.Instance.Combination.GetCombinationNode("나뭇가지");

            var spawn = itemBuilder
                .SetCombinationNode(stick)
                .AddShadowCaster2D()
                .Build();
            spawn.gameObject.SetActive(true);
            spawn.transform.position = transform.position;
        }
        protected override void OnInteractableEvent(Entity entity)
        {
            base.OnInteractableEvent(entity);

            if (IsBreak == true)
                return;
            string key = Random.Range(0, 2) == 0 ? "stepwood_1" : "stepwood_2";
            Managers.Managers.Instance.Sound.PlayOneShot(key);
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