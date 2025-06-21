using UnityEngine.Networking;

namespace EasyHelpers.Runtime.Tools.RestApi
{
    public static class ApiExtentions
    {
        public static bool HasError(this UnityWebRequest request)
        {
            return request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError || request.result == UnityWebRequest.Result.DataProcessingError;
        }
    }
}
