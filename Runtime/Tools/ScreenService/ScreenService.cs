using System.Collections.Generic;
using UnityEngine;
using EasyHelpers.Runtime.Common;

namespace EasyHelpers.Runtime.Tools.ScreenService
{
    public class ScreenService : SingletonMonoBehaviour<ScreenService>
    {
        [SerializeField] private UIScreen[] screens;

        private Dictionary<System.Type, UIScreen> currentScreens = new Dictionary<System.Type, UIScreen>();
        private Stack<UIScreen> stackedScreens = new Stack<UIScreen>();
        private UIScreen activeScreen;

        private T FindScreen<T>() where T : UIScreen
        {
            if (currentScreens.ContainsKey(typeof(T)))
            {
                if (currentScreens[typeof(T)] == null)
                {
                    currentScreens.Remove(typeof(T));
                    return FindScreen<T>();
                }
                else
                    return (T)currentScreens[typeof(T)];
            }
            else
            {
                for (int i = 0; i < screens.Length; i++)
                {
                    if (screens[i] != null && screens[i].GetType() == typeof(T))
                    {
                        currentScreens.Add(typeof(T), (T)screens[i]);
                        return (T)screens[i];
                    }
                }
            }
            Debug.Log($"[ScreenService][FindScreen] : Can not find screen of type {typeof(T)}. Make sure it's scene object is reference");
            return null;
        }

        public T ShowScreen<T>() where T : UIScreen
        {
            T screen = FindScreen<T>();
            screen.OnShowScreen();

            if (screen != null)
            {
                activeScreen = screen;
                if (activeScreen.IsStackable)
                {
                    if (stackedScreens.TryPeek(out UIScreen topScreen))
                    {
                        if (!activeScreen.IsEquals(topScreen))
                            stackedScreens.Push(activeScreen);
                    }
                    else
                    {
                        stackedScreens.Push(activeScreen);
                    }
                }
            }

            Debug.Log($"[ScreenService][ShowScreen] : {screen} | Active Screen : {activeScreen} | Stacked Screens Count : {stackedScreens.Count}".ToCyan());
            return screen;
        }

        public T HideScreen<T>() where T : UIScreen
        {
            T screen = FindScreen<T>();
            screen.OnHideScreen();

            if (screen != null)
            {
                if (stackedScreens.TryPeek(out UIScreen topScreen) && screen.IsEquals(topScreen))
                    stackedScreens.Pop();
                if (stackedScreens.TryPeek(out UIScreen newTopScreen))
                    activeScreen = newTopScreen;
            }

            Debug.Log($"[ScreenService][HideScreen] : {screen} | Active Screen : {activeScreen} | Stacked Screens Count : {stackedScreens.Count}".ToCyan());
            return screen;
        }

        public T GetScreen<T>() where T : UIScreen
        {
            return FindScreen<T>();
        }

        public bool IsActiveScreen(UIScreen screen)
        {
            if (activeScreen == null) return false;

            return screen.GetInstanceID() == activeScreen.GetInstanceID();
        }
    }
}