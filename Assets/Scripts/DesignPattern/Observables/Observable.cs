using System;
using System.Collections.Generic;

public class Observable<T> : Notify
{

    public readonly List<Action<T>> eventListeners = new List<Action<T>>();
    public virtual void SetData(params object[] data)
    { }
    public void Notify(T subject)
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
        {
            eventListeners[i].Invoke(subject);
        }
    }
    public void Register(Action<T> observer)
    {
        if (!eventListeners.Contains(observer))
        {
            eventListeners.Add(observer);
        }
    }
    public void Unregister(Action<T> observer)
    {
        if (eventListeners.Contains(observer))
        {
            eventListeners.Remove(observer);
        }
    }

    public override void NotifyMySelf()
    {
    }
}
