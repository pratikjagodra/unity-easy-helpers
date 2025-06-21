using System;
using System.Collections.Generic;
using EasyHelpers.Runtime.Common;

namespace EasyHelpers.Runtime.Tools.RestApi
{
    internal abstract class RestApiHandler
    {
        protected Dictionary<string, object> parameters = new Dictionary<string, object>();
        protected string methodName;

        protected void GetRequest<T>(Action<T> onSuccess, Action<string> onFail)
        {
            RestApiConnector.Instance.GetRequest(methodName, parameters,
                (response) =>
                {
                    T responseObject = response.ToObject<T>();
                    onSuccess?.Invoke(responseObject);
                },
                (response) =>
                {
                    onFail?.Invoke(response);
                }
            );
        }

        protected void PostRequest<T>(Action<T> onSuccess, Action<string> onFail)
        {
            RestApiConnector.Instance.PostRequest(methodName, parameters,
                (response) =>
                {
                    T responseObject = response.ToObject<T>();
                    onSuccess?.Invoke(responseObject);
                },
                (response) =>
                {
                    onFail?.Invoke(response);
                }
            );
        }
    }
}