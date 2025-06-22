using UnityEngine;

namespace EasyHelpers.Runtime.Common
{
    public static class SetFPS
    {
        internal static void Set(int fPSNumber)
        {
            if (fPSNumber != 0)
                Application.targetFrameRate = (int)fPSNumber;
        }
    }
}
