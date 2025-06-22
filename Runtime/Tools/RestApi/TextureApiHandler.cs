using System;
using UnityEngine;
using EasyHelpers.Runtime.Tools.RestApi;

internal class TextureApiHandler : RestApiHandler
{
    internal void GetTexture(string url, Action<Texture> onSuccess, Action<string> onFail)
    {
        RestApiConnector.Instance.GetTexture(url, onSuccess, onFail);
    }
}
