using IsolateIsland.Runtime.Character;
using IsolateIsland.Runtime.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IsolateIsland.Runtime.Utils.Defines;

namespace IsolateIsland.Runtime.Event
{
    public sealed class DressableEventListener : GenericEventListener<EDressableState, DressableItem> { }
    public sealed class OnClickConfigButtonEventListener : GenericEventListener<EDressableState> { }

    /// <summary>
    /// 플레이어에게 적용되어 있는 캐스팅 콜라이더가 감지되었을 때 발생하는 이벤트
    /// </summary>
    public sealed class OnDetectCasterEvent : GenericEventListener<Entity /*Target Entity*/, bool /*IsInteractable*/> { }
    /// <summary>
    /// 플레이어랑 캐스팅 가능한 물체하고 상호작용을 했을 시 발생하는 이벤트
    /// </summary>
    public sealed class OnInteractCasterEvent : GenericEventListener<Entity> { }
    public abstract class InteractEvent : GenericEventListener<Entity>
    {
        public abstract void OnInteract(Entity entity);
    }
    public sealed class OnInteractEvent : InteractEvent
    {
        public override void OnInteract(Entity entity)
        {
            //Managers.Managers.Instance.Sound.PlayOneShot("stepwood_1");
        }
    }

    public class OnHitInteractEvent : InteractEvent
    {

        public override void OnInteract(Entity entity)
        {
            Managers.Managers.Instance.Sound.PlayOneShot("타격_근접");
            var obj = Managers.Managers.Instance.Pool.ParticleInstantiate("FX_Hit_01", 1.5f);
            obj.transform.position = entity.transform.position;

            var dir = Managers.Managers.Instance.DI.Get<CharacterAnimController>().MoveNormalDir;
            Managers.Managers.Instance.Util.AddTimer(0.1f, () => entity.GetRigidBody2D.velocity = Vector2.zero);
            entity.GetRigidBody2D.velocity = dir * 20;

            Managers.Managers.Instance.Camera.CameraShake(2.0f, 0.5f);
            Time.timeScale = 0.3f;
            Managers.Managers.Instance.Util.AddTimer(0.09f, () => Time.timeScale = 1.0f, true);
        }
    }
    public class OnRangedHitInteractEvent : GenericEventListener<Vector2, DressableItem, Entity>
    {

        public void OnInteract(Vector2 arrowDir, Entity entity)
        {
            Managers.Managers.Instance.Sound.PlayOneShot("타격_근접");
            var obj = Managers.Managers.Instance.Pool.ParticleInstantiate("FX_Hit_01", 1.5f);
            obj.transform.position = entity.transform.position;

            Managers.Managers.Instance.Util.AddTimer(0.1f, () => entity.GetRigidBody2D.velocity = Vector2.zero);
            entity.GetRigidBody2D.velocity = arrowDir * 20;

            Managers.Managers.Instance.Camera.CameraShake(0.6f, 0.5f);
            Time.timeScale = 0.5f;
            Managers.Managers.Instance.Util.AddTimer(0.09f, () => Time.timeScale = 1.0f, true);

            Managers.Managers.Instance.Pool.Destroy(entity.gameObject);
        }
    }

    // 클래스 이름, 상속 부모 등의 상세정보는 알아서 수정하셈
    public class HitInteractPlayerEvent : EventListener
    {
    
    }

    public class OnCollectItemEvent : GenericEventListener<ItemBase>
    {

    }

    public class OnUIUpdateEvent : EventListener { }

    public class OnGameoverEvent : EventListener
    { 
    
    }


    public class OnAttackAnimationEvent : GenericEventListener<CharacterAttackController, EAttackAnimationKeyState> { }
}
