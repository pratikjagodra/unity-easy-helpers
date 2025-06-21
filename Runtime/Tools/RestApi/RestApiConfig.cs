using UnityEngine;
using EasyHelpers.Runtime.Common;

namespace EasyHelpers.Runtime.Tools.RestApi
{
    [CreateAssetMenu(fileName = "RestApiConfig", menuName = "ScriptableObjects/RestApiConfig")]
    public class RestApiConfig : SingletonScriptableObject<RestApiConfig>
    {
        [SerializeField] private string baseUrl;

        public string BaseUrl => baseUrl;

        protected override string GetAssetPath()
        {
            return "RestApiConfig";
        }
    }
}
