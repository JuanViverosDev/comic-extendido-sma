using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader instance;

    public static SceneLoader Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SceneLoader>();

                if (instance == null)
                {
                    GameObject singletonObj = new GameObject("SceneLoader");
                    instance = singletonObj.AddComponent<SceneLoader>();
                    DontDestroyOnLoad(singletonObj);
                }
            }
            return instance;
        }
    }

    public UnityEvent<float> onProgress;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public static void SetInstanceForTesting(SceneLoader testInstance)
    {
        instance = testInstance;
    }

    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            onProgress?.Invoke(progress);

            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
