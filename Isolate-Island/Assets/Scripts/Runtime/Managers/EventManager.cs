using IsolateIsland.Runtime.Event;
using IsolateIsland.Runtime.Inventory;
using IsolateIsland.Runtime.Stat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Managers
{


    public class EventManager : IManagerInit
    {
        public Dictionary<Type, EventListener> listeners = new Dictionary<Type, EventListener>();


        public T CreateListener<T>() where T : EventListener, new()
        {
            if (listeners.ContainsKey(typeof(T)))
                return default(T);
            var listener = new T();
            listeners.Add(typeof(T), listener);
            return listener;
        }

        public T GetListener<T>() where T : EventListener, new()
        {
            if (!listeners.ContainsKey(typeof(T)))
                return CreateListener<T>();

            EventListener listener;
            if (!listeners.TryGetValue(typeof(T), out listener))
                return default(T);

            var conv = listener as T;

            return conv;
        }

        public void OnInit()
        {
            

        }
    }

}