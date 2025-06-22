using Newtonsoft.Json;

namespace EasyHelpers.Runtime.Common
{
    public static class JsonExtension
    {
        public static string ToJson<T>(this T Object)
        {
            return JsonConvert.SerializeObject(Object);
        }

        public static T ToObject<T>(this string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}
