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

    public abstract class InteractEvent : EventListener
    {
        public abstract void OnInteract();
    }

    public class HitInteractEvent : GenericEventListener<Entity>
    {

        public void OnInteract(Entity entity)
        {
            Managers.Managers.Instance.Sound.PlayOneShot("타격_근접");
            var obj = Managers.Managers.Instance.Pool.ParticleInstantiate("FX_Hit_01", 1.5f);
            obj.transform.position = entity.transform.position;
            Managers.Managers.Instance.Camera.CameraShake(2.0f, 0.5f);
            Time.timeScale = 0.3f;
            Managers.Managers.Instance.Util.AddTimer(0.09f, () => Time.timeScale = 1.0f, true);
        }
    }

    public class OnCollectItemEvent : GenericEventListener<ItemBase>
    {

    }

}
