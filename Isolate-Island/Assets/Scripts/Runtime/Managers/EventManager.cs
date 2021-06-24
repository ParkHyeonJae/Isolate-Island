using IsolateIsland.Runtime.Stat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Managers
{

    public class EventListener
    {
        private Action _listener { get; set; }

        internal virtual void Set(Action action)
            => _listener = action;
        internal virtual void Add(Action action)
            => _listener += action;
    }

    public class GenericEventListener<T> : EventListener
    {
        private Action<T> _listener { get; set; }
        internal sealed override void Add(Action action)
            => base.Add(action);
        internal sealed override void Set(Action action)
            => base.Set(action);

        internal virtual void Add(Action<T> action)
            => _listener += action;

        internal virtual void Set(Action<T> action)
            => _listener = action;

    }

    public sealed class DressableEventListener : GenericEventListener<EParts>
    {

    }

    public class EventManager : IManagerInit
    {
        public DressableEventListener _dressableEventListener = new DressableEventListener();

        //EventListener HasListener<T>(T listener) where T : EventListener
        //{
        //    switch (typeof(T))
        //    {

        //        default:
        //            break;
        //    }
        //}

        //public void SetEventListener<T>(Action evt) where T : EventListener
        //{
        //    EventListener eventListener;
        //    if (!listeners.TryGetValue(typeof(T), out eventListener))
        //        return;
        //    eventListener.Set(evt);
        //}
        //public void SetEventListener<T>(Action<T> evt) where T : GenericEventListener<T>
        //{
        //    GenericEventListener<T> eventListener;
        //    if (!listeners.TryGetValue(typeof(T), out eventListener))
        //        return;
        //    eventListener.Add(evt);
        //}

        public void OnInit()
        {
            //OnDressableEvt = delegate { };

        }
    }

}