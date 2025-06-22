using System;
using UnityEngine;

namespace EasyHelpers.Runtime.Common
{
    public abstract class Singleton<T> where T : class, new()
    {
        private static Lazy<T> instance = new(() => new T());
        public static T Instance { get { return instance.Value; } }
    }

    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        public static T Instance { get { return instance; } }

        protected virtual void Awake()
        {
            if (instance != null)
                Destroy(gameObject);
            else
                instance = this as T;
        }
    }

    public abstract class PersistentSingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        public static T Instance { get { return instance; } }

        protected virtual void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
        }
    }

    public abstract class SelfSingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance != null) return instance;
                else
                {
                    GameObject newObject = new GameObject(typeof(T).ToString());
                    instance = newObject.AddComponent<T>();
                }

                return instance;
            }
        }
    }

    public abstract class SelfPersistentSingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance != null) return instance;
                else
                {
                    GameObject newObject = new GameObject(typeof(T).ToString());
                    instance = newObject.AddComponent<T>();
                    DontDestroyOnLoad(newObject);
                }

                return instance;
            }
        }
    }

    public abstract class SingletonScriptableObject<T>: ScriptableObject where T : SingletonScriptableObject<T>
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance != null) return instance;

                T temp = ScriptableObject.CreateInstance<T>();
                string path = temp.GetAssetPath();
                ScriptableObject.DestroyImmediate(temp);

                instance = Resources.Load<T>(path);
                return instance;
            }
        }

        protected abstract string GetAssetPath();
    }
}
