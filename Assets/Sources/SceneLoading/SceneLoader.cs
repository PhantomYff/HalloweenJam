using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour, ISceneLoader
{
    public void Load(string scene, Action<float> onProgress = null, Action completed = null)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene);

        StartCoroutine(Load_Internal());

        IEnumerator Load_Internal()
        {
            while (asyncOperation.isDone == false)
            {
                onProgress?.Invoke(asyncOperation.progress);
                yield return null;
            }
            completed?.Invoke();
        }
    }
}
