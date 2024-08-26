using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    // La instancia Singleton
    private static SceneLoader instance;

    // Evento para informar del progreso de la carga
    public UnityEvent<float> onProgress;

    // Propiedad para acceder a la instancia desde otras clases
    public static SceneLoader Instance
    {
        get
        {
            // Si la instancia es nula, la busca en la escena
            if (instance == null)
            {
                instance = FindObjectOfType<SceneLoader>();

                // Si sigue siendo nula, crea un nuevo GameObject con el SceneLoader
                if (instance == null)
                {
                    GameObject singletonObj = new GameObject("SceneLoader");
                    instance = singletonObj.AddComponent<SceneLoader>();
                    DontDestroyOnLoad(singletonObj);  // No destruir al cargar nuevas escenas
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        // Asegurar que solo hay una instancia del SceneLoader
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // No destruir al cargar nuevas escenas
        }
        else if (instance != this)
        {
            Destroy(gameObject);  // Destruir duplicados
        }
    }

    // Método para cargar la escena asíncronamente
    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    // Corutina para manejar la carga de la escena
    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            // Normaliza el progreso (va de 0 a 0.9)
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            onProgress?.Invoke(progress);  // Invoca el evento de progreso

            // Si la escena está cargada en un 90% o más
            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;  // Permite la activación de la escena
            }

            yield return null;
        }
    }
}