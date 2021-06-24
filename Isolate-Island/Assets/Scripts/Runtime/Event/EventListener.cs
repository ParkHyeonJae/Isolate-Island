using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Event
{
    public class EventListener
    {
        private Action _listener { get; set; }

        internal virtual void Set(Action action)
            => _listener = action;
        internal virtual void Subscribe(Action action)
            => _listener += action;

        internal virtual void Describe(Action action)
            => _listener -= action;

        internal virtual void Invoke()
            => _listener?.Invoke();
    }

    public class GenericEventListener<T> : EventListener
    {
        private Action<T> _listener { get; set; }
        internal sealed override void Subscribe(Action action)
            => base.Subscribe(action);
        internal sealed override void Describe(Action action)
            => base.Describe(action);
        internal sealed override void Set(Action action)
            => base.Set(action);

        internal sealed override void Invoke()
            => base.Invoke();

        internal virtual void Subscribe(Action<T> action)
            => _listener += action;

        internal virtual void Describe(Action<T> action)
            => _listener -= action;

        internal virtual void Set(Action<T> action)
            => _listener = action;

        internal virtual void Invoke(T args1)
            => _listener?.Invoke(args1);

    }
    public class GenericEventListener<T1, T2> : EventListener
    {
        private Action<T1,T2> _listener { get; set; }
        internal sealed override void Subscribe(Action action)
            => base.Subscribe(action);
        internal sealed override void Describe(Action action)
            => base.Describe(action);
        internal sealed override void Set(Action action)
            => base.Set(action);

        internal sealed override void Invoke()
            => base.Invoke();

        internal virtual void Subscribe(Action<T1, T2> action)
            => _listener += action;

        internal virtual void Describe(Action<T1, T2> action)
            => _listener -= action;

        internal virtual void Set(Action<T1, T2> action)
            => _listener = action;

        internal virtual void Invoke(T1 args1, T2 args2)
            => _listener?.Invoke(args1, args2);

    }
    public class GenericEventListener<T1, T2, T3> : EventListener
    {
        private Action<T1, T2, T3> _listener { get; set; }
        internal sealed override void Subscribe(Action action)
            => base.Subscribe(action);
        internal sealed override void Describe(Action action)
            => base.Describe(action);
        internal sealed override void Set(Action action)
            => base.Set(action);

        internal sealed override void Invoke()
            => base.Invoke();

        internal virtual void Subscribe(Action<T1, T2, T3> action)
            => _listener += action;

        internal virtual void Describe(Action<T1, T2, T3> action)
            => _listener -= action;

        internal virtual void Set(Action<T1, T2, T3> action)
            => _listener = action;

        internal virtual void Invoke(T1 args1, T2 args2, T3 args3)
            => _listener?.Invoke(args1, args2, args3);

    }
    public class GenericEventListener<T1, T2, T3, T4> : EventListener
    {
        private Action<T1, T2, T3, T4> _listener { get; set; }
        internal sealed override void Subscribe(Action action)
            => base.Subscribe(action);
        internal sealed override void Describe(Action action)
            => base.Describe(action);
        internal sealed override void Set(Action action)
            => base.Set(action);

        internal sealed override void Invoke()
            => base.Invoke();

        internal virtual void Subscribe(Action<T1, T2, T3, T4> action)
            => _listener += action;

        internal virtual void Describe(Action<T1, T2, T3, T4> action)
            => _listener -= action;

        internal virtual void Set(Action<T1, T2, T3, T4> action)
            => _listener = action;

        internal virtual void Invoke(T1 args1, T2 args2, T3 args3, T4 args4)
            => _listener?.Invoke(args1, args2, args3, args4);

    }
    public class GenericEventListener<T1, T2, T3, T4, T5> : EventListener
    {
        private Action<T1, T2, T3, T4, T5> _listener { get; set; }
        internal sealed override void Subscribe(Action action)
            => base.Subscribe(action);
        internal sealed override void Describe(Action action)
            => base.Describe(action);
        internal sealed override void Set(Action action)
            => base.Set(action);

        internal sealed override void Invoke()
            => base.Invoke();

        internal virtual void Subscribe(Action<T1, T2, T3, T4, T5> action)
            => _listener += action;

        internal virtual void Describe(Action<T1, T2, T3, T4, T5> action)
            => _listener -= action;

        internal virtual void Set(Action<T1, T2, T3, T4, T5> action)
            => _listener = action;

        internal virtual void Invoke(T1 args1, T2 args2, T3 args3, T4 args4, T5 args5)
            => _listener?.Invoke(args1, args2, args3, args4, args5);

    }

}