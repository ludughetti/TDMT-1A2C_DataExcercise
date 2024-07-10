using System;
using UnityEngine;

namespace Events
{
    public class EventChannel<T> : ScriptableObject
    {
        private Action<T> _event = delegate { };

        public void Subscribe(Action<T> action)
        {
            _event += action;
            Debug.Log($"{name}: New event ({action}) subscribed to Event delegate");
        }

        public void Unsubscribe(Action<T> action)
        {
            _event -= action;
            Debug.Log($"{name}: New event ({action}) unsubscribed from Event delegate");
        }

        public void Invoke(T data)
        {
            _event?.Invoke(data);
            Debug.Log($"{name}: Event with type {data} was invoked");
        }
    }
}
