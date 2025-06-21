using UnityEngine;

namespace EasyHelpers.Runtime.Common
{
    public static class SetRenderResolution
    {
        private static void Set(int resolution)
        {
            int height = Screen.currentResolution.height;
            int width = Screen.currentResolution.width;

            if (width > height) // landscape
            {
                float aspectRatio = (float)width / (float)height;
                height = resolution;
                width = (int)(height * aspectRatio);

                Screen.SetResolution(width, height, true);
            }
            else // portrait
            {
                float aspectRatio = (float)height / (float)width;
                width = resolution;
                height = (int)(width * aspectRatio);

                Screen.SetResolution(width, height, true);
            }
        }
    }
}
