namespace EasyHelpers.Runtime.Common
{
    public static class StringExtentions
    {
        public const string GREEN_CODE = "#00ff00";
        public static string ToGreen(this string stringObject)
        {
            return $"<color={GREEN_CODE}>{stringObject}</color>";
        }

        public const string RED_CODE = "#ff0000";
        public static string ToRed(this string stringObject)
        {
            return $"<color={RED_CODE}>{stringObject}</color>";
        }

        public const string YELLOW_CODE = "#ffff00";
        public static string ToYellow(this string stringObject)
        {
            return $"<color={YELLOW_CODE}>{stringObject}</color>";
        }

        public const string CYAN_CODE = "#00ffff";
        public static string ToCyan(this string stringObject)
        {
            return $"<color={CYAN_CODE}>{stringObject}</color>";
        }
    }
}
