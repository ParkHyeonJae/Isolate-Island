using IsolateIsland.Runtime.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Character
{
    public class HurbsGrassInteractType : InteractType
    {
        public static readonly InteractType HurbsInteract = new InteractType("Animation@GrassInteract");
        public static readonly InteractType BreakGrass = new InteractType("Animation@BreakGrass");

        public HurbsGrassInteractType(string value) : base(value) { }
    }

    public class HurbsGrassEntity : BreakableEntity
    {
        public override void Initalize()
        {
            base.Initalize();
        }

        protected override void Reward()
        {
            base.Reward();
            
            var stick = Managers.Managers.Instance.Pool.Instantiate("약초");
            stick.gameObject.SetActive(true);
            stick.transform.position = transform.position;
        }
        public override void OnInteractableEvent(Entity entity)
        {
            base.OnInteractableEvent(entity);

            if (IsBreak == true)
                return;
            string key = Random.Range(0, 2) == 0 ? "stepwood_1" : "stepwood_2";
            Managers.Managers.Instance.Sound.PlayOneShot(key);
            AnimatePlayFactory(HurbsGrassInteractType.HurbsInteract);
        }

        

        protected override void OnDamaged(int damage)
        {
            HP -= damage;

            if (HP <= 0)
            {
                //TODO : DEAD ENTITY
                AnimatePlayFactory(HurbsGrassInteractType.BreakGrass);
                Reward();
                IsBreak = true;
            }
        }

        protected override bool AnimatePlayFactory(InteractType interactType)
        {
            if (interactType == HurbsGrassInteractType.HurbsInteract)
            {
                animator.Play(interactType.Value);
                return true;
            }
            if (interactType == HurbsGrassInteractType.BreakGrass)
            {
                animator.Play(interactType.Value);
                return true;
            }

            base.AnimatePlayFactory(interactType);
            return false;
        }
    }
}