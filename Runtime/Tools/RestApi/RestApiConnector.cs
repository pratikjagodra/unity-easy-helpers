using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using EasyHelpers.Runtime.Common;

namespace EasyHelpers.Runtime.Tools.RestApi
{
    public class RestApiConnector : SelfPersistentSingletonMonoBehaviour<RestApiConnector>
    {
        protected WWWForm GetFormFields(Dictionary<string, object> parameters)
        {
            WWWForm formFields = new WWWForm();
            foreach (var parameter in parameters)
            {
                formFields.AddField(parameter.Key, parameter.Value.ToString());
            }
            return formFields;
        }
        
        internal void GetRequest(string methodName, Dictionary<string, object> parameters, Action<string> onSuccess, Action<string> onFail)
        {
            StartCoroutine(GetRequestCoroutine(methodName, parameters, onSuccess, onFail));
        }

        private IEnumerator GetRequestCoroutine(string methodName, Dictionary<string, object> parameters, Action<string> onSuccess, Action<string> onFail)
        {
            if (RestApiConfig.Instance == null)
            {
                Debug.Log($"[RestApiConnector] No RestApiConfig found in Resources folder");
                onFail?.Invoke("RestApiConfig not found");
                yield break;
            }
            string url = RestApiConfig.Instance.BaseUrl + methodName;
            Debug.Log($"[RestApiConnector][Call] GET: {url}\nParameters : {parameters.ToJson()}".ToYellow());
            var formFields = GetFormFields(parameters);
            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                foreach (var formField in formFields.headers)
                {
                    request.SetRequestHeader(formField.Key, formField.Value);
                }

                yield return request.SendWebRequest();

                if (request.HasError())
                {
                    Debug.Log($"[RestApiConnector][Response] : {request.error}\n{url}".ToRed());
                    onFail?.Invoke(request.error);
                }
                else
                {
                    Debug.Log($"[RestApiConnector][Response] : {request.downloadHandler.text}\n{url}".ToGreen());
                    onSuccess?.Invoke(request.downloadHandler.text);
                }
            }
        }

        internal void PostRequest(string methodName, Dictionary<string, object> parameters, Action<string> onSuccess, Action<string> onFail)
        {
            StartCoroutine(PostRequestCoroutine(methodName, parameters, onSuccess,onFail));
        }

        private IEnumerator PostRequestCoroutine(string methodName, Dictionary<string, object> parameters, Action<string> onSuccess, Action<string> onFail)
        {
            if (RestApiConfig.Instance == null)
            {
                Debug.Log($"[RestApiConnector] RestApiConfig not found in Resources folder");
                onFail?.Invoke("RestApiConfig not found");
                yield break;
            }
            string url = RestApiConfig.Instance.BaseUrl + methodName;
            Debug.Log($"[RestApiConnector][Call] POST: {url}\nParameters : {parameters.ToJson()}".ToYellow());
            var formFields = GetFormFields(parameters);
            using(UnityWebRequest request = UnityWebRequest.Post(url, formFields))
            {
                yield return request.SendWebRequest();

                if (request.HasError())
                {
                    Debug.Log($"[RestApiConnector][Response] : {request.error}\n{url}".ToRed());
                    onFail?.Invoke(request.error);
                }
                else
                {
                    Debug.Log($"[RestApiConnector][Response] : {request.downloadHandler.text}\n{url}".ToGreen());
                    onSuccess?.Invoke(request.downloadHandler.text);
                }
            }
        }

        internal void GetTexture(string url, Action<Texture> onSuccess, Action<string> onFail)
        {
            StartCoroutine(GetTextureCoroutine(url, onSuccess, onFail));
        }

        private IEnumerator GetTextureCoroutine(string url, Action<Texture> onSuccess, Action<string> onFail)
        {
            Debug.Log($"[RestApiConnector][Call] GetTexture: {url}".ToYellow());
            using(UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
            {
                yield return request.SendWebRequest();

                if(request.HasError())
                {
                    Debug.Log($"[RestApiConnector][Response] : {request.error}\n{url}".ToRed());
                    onFail?.Invoke(request.error);
                }
                else
                {
                    Debug.Log($"[RestApiConnector][Response] : Texture Download Success\n{url}".ToGreen());
                    onSuccess?.Invoke(((DownloadHandlerTexture)request.downloadHandler).texture);
                }
            }
        }
    }
}
