
using System.Collections.Generic;

namespace EasyHelpers.Runtime.Tools.EventService
{
    public abstract class EventBase { }

    public static class EventManager
    {
        private static bool loggingEnabled = true;
        private static Dictionary<System.Type, List<IEventListener>> eventMap = new();

        private static void Log(object message)
        {
            if (!loggingEnabled) return;
            UnityEngine.Debug.Log($"[{nameof(EventManager)}] {message}");
        }

        public static void AddListener<T>(IEventListener<T> listener) where T : EventBase
        {
            if (listener == null)
            {
                Log($"listener is null");
                return;
            }

            if (!eventMap.ContainsKey(typeof(T)))
            {
                Log($"Adding event of type {typeof(T)}");
                eventMap.Add(typeof(T), new());
            }

            if (!eventMap[typeof(T)].Contains(listener))
                eventMap[typeof(T)].Add(listener);
            else
                Log($"Listener already exist for type {typeof(T)}");
        }

        public static void RemoveListener<T>(IEventListener<T> listener) where T : EventBase
        {
            if (listener == null)
            {
                Log($"listener is null");
                return;
            }

            if (!eventMap.ContainsKey(typeof(T)))
            {
                Log($"No event is mapped of type {typeof(T)}");
                return;
            }

            if (eventMap[typeof(T)].Contains(listener))
                eventMap[typeof(T)].Remove(listener);
            else
                Log($"No listener found for type {typeof(T)}");
        }

        public static void CallEvent<T>(T eventData) where T : EventBase
        {
            if (!eventMap.ContainsKey(typeof(T)))
            {
                Log($"No event is mapped of type {typeof(T)}");
                return;
            }

            int listenersCount = eventMap[typeof(T)].Count;
            if (listenersCount == 0)
            {
                Log($"No listeners found for type {typeof(T)}");
                return;
            }

            Log($"Calling event of type {typeof(T)} on {listenersCount} listeners");
            IEventListener<T> listener;
            for (int i = 0; i < listenersCount; i++)
            {
                listener = eventMap[typeof(T)][i] as IEventListener<T>;
                if (listener == null) continue;
                listener.OnTrigger(eventData);
            }
        }

        public static void SetLogging(bool enabled)
        {
            loggingEnabled = enabled;
        }
    }
}
