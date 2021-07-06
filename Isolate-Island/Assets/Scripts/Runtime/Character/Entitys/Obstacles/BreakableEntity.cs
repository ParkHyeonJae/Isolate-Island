using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace IsolateIsland.Runtime.Character
{
    public class BreakableInteractType : InteractType
    {
        public static readonly InteractType BreakTree = new InteractType("Animation@BreakTree");

        public BreakableInteractType(in string value) : base(value) { }
    }

    public class BreakableEntity : InteractableEntity
    {
        [SerializeField] int HP = 10;
        [SerializeField] int DMG = 2;

        public bool IsBreak { get; set; } = false;

        public override void Initalize()
        {
            base.Initalize();
            //AnimationInitialize(BreakableInteractType.BreakTree);
        }


        protected virtual void Reward()
        {

        }

        protected override void OnInteractableEvent(Entity entity)
        {
            base.OnInteractableEvent(entity);

            if (IsBreak == true)
                return;

            OnDamaged(DMG);
        }

        protected void OnDamaged(int damage)
        {
            HP -= damage;

            if (HP <= 0)
            {
                //TODO : DEAD ENTITY
                AnimatePlayFactory(BreakableInteractType.BreakTree);
                Reward();
                IsBreak = true;
            }
        }
        protected override bool AnimatePlayFactory(InteractType interactType)
        {
            if (interactType == BreakableInteractType.BreakTree)
            {
                animator.Play(interactType.Value);
                return true;
            }

            base.AnimatePlayFactory(interactType);
            return false;
        }

    }
}
