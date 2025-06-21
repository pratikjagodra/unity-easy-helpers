using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EasyHelpers.Runtime.Common
{
    public class SceneLoader : PersistentSingletonMonoBehaviour<SceneLoader>
    {
        private IEnumerator LoadSceneCoroutine(string sceneName, Action onComplete = null, Action<float> onUpdate = null)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            operation.allowSceneActivation = true;

            while (!operation.isDone)
            {
                yield return null;
                onUpdate?.Invoke(operation.progress);
            }

            onComplete?.Invoke();
        }

        public void LoadSceneAsync(string sceneName, Action onComplete = null, Action<float> onUpdate = null)
        {
            StartCoroutine(LoadSceneCoroutine(sceneName, onComplete, onUpdate));
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public int CurrentSceneIndex => SceneManager.GetActiveScene().buildIndex;
        public string CurrentSceneName => SceneManager.GetActiveScene().name;
    }
}
