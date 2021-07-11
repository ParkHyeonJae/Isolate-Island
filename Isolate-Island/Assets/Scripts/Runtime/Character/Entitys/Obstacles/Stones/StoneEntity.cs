using IsolateIsland.Runtime.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Character
{
    public class StoneInteractType : InteractType
    {
        public static readonly InteractType HurbsInteract = new InteractType("Animation@GrassInteract");
        public static readonly InteractType BreakGrass = new InteractType("Animation@BreakGrass");

        public StoneInteractType(string value) : base(value) { }
    }

    public class StoneEntity : BreakableEntity
    {
        public override void Initalize()
        {
            base.Initalize();
        }

        protected override void Reward()
        {
            base.Reward();

            if (Random.Range(0, 101) <= 30)
            {
                var obj = Managers.Managers.Instance.Pool.Instantiate("돌멩이");
                obj.gameObject.SetActive(true);
                obj.transform.position = transform.position;
            }
            else
            {
                var obj = Managers.Managers.Instance.Pool.Instantiate("부싯돌");
                obj.gameObject.SetActive(true);
                obj.transform.position = transform.position;
            }
        }
        public override void OnInteractableEvent(Entity entity)
        {
            base.OnInteractableEvent(entity);

            if (IsBreak == true)
                return;
            string key = Random.Range(0, 2) == 0 ? "stepwood_1" : "stepwood_2";
            Managers.Managers.Instance.Sound.PlayOneShot(key);
            AnimatePlayFactory(StoneInteractType.HurbsInteract);
        }

        

        protected override void OnDamaged(int damage)
        {
            HP -= damage;

            if (HP <= 0)
            {
                //TODO : DEAD ENTITY
                AnimatePlayFactory(StoneInteractType.BreakGrass);
                Reward();
                IsBreak = true;
            }
        }

        protected override bool AnimatePlayFactory(InteractType interactType)
        {
            if (interactType == StoneInteractType.HurbsInteract)
            {
                animator.Play(interactType.Value);
                return true;
            }
            if (interactType == StoneInteractType.BreakGrass)
            {
                animator.Play(interactType.Value);
                return true;
            }

            base.AnimatePlayFactory(interactType);
            return false;
        }
    }
}