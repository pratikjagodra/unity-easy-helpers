
namespace EasyHelpers.Runtime.Tools.ScreenService
{
    public static class UIExtension
    {
        public static bool IsEquals(this UIScreen screen, UIScreen otherScreen)
        {
            if (otherScreen == null) return false;

            return screen.GetInstanceID() == otherScreen.GetInstanceID();
        }
    }
}
