using System.Collections.Generic;
using UnityEngine;
using EasyHelpers.Runtime.Common;

namespace EasyHelpers.Runtime.Tools.ScreenService
{
    public static class ScreenService
    {
        private static Dictionary<System.Type, UIScreen> currentScreens = new Dictionary<System.Type, UIScreen>();

        private static UIScreen activeScreen;

        private static Stack<UIScreen> stackedScreens = new Stack<UIScreen>();

        private static T FindScreen<T>() where T : UIScreen
        {
            if(currentScreens.ContainsKey(typeof(T)))
            {
                if(currentScreens[typeof(T)] == null)
                {
                    currentScreens.Remove(typeof(T));
                    return FindScreen<T>();
                }
                else
                    return (T)currentScreens[typeof(T)];
            }
            else
            {
                T screen = Object.FindAnyObjectByType<T>();
                if(screen != null)
                {
                    currentScreens.Add(typeof(T), screen);
                    return screen;
                }
            }

            return null;
        }

        public static T ShowScreen<T>() where T : UIScreen
        {
            T screen = FindScreen<T>();
            screen.OnShowScreen();

            if (screen != null)
            {
                activeScreen = screen;
                if(activeScreen.isStackable)
                {
                    if(stackedScreens.TryPeek(out UIScreen topScreen))
                    {
                        if(!activeScreen.IsEquals(topScreen))
                            stackedScreens.Push(activeScreen);
                    }
                    else
                    {
                        stackedScreens.Push(activeScreen);
                    }
                }
            }

            Debug.Log($"[ScreenService] | Show Screen : {screen} | Active Screen : {activeScreen} | Stacked Screens Count : {stackedScreens.Count}".ToCyan());
            return screen;
        }

        public static T HideScreen<T>() where T : UIScreen
        {
            T screen = FindScreen<T>();
            screen.OnHideScreen();

            if(screen != null)
            {
                if (stackedScreens.TryPeek(out UIScreen topScreen) && screen.IsEquals(topScreen))
                    stackedScreens.Pop();
                if (stackedScreens.TryPeek(out UIScreen newTopScreen))
                    activeScreen = newTopScreen;
            }
            
            Debug.Log($"[ScreenService] | Hide Screen : {screen} | Active Screen : {activeScreen} | Stacked Screens Count : {stackedScreens.Count}".ToCyan());
            return screen;
        }

        public static T GetScreen<T>() where T : UIScreen
        {
            return FindScreen<T>();
        }

        public static void Refresh()
        {
            currentScreens.Clear();
            stackedScreens.Clear();
        }

        public static bool IsActiveScreen(UIScreen screen)
        {
            if(activeScreen == null) return false;

            return screen.GetInstanceID() == activeScreen.GetInstanceID();
        }

        public static bool IsEquals(this UIScreen screen, UIScreen otherScreen)
        {
            if (otherScreen == null) return false;

            return screen.GetInstanceID() == otherScreen.GetInstanceID();
        }
    }
}