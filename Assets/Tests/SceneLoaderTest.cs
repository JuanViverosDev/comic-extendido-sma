using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using System.Collections;

public class SceneLoaderTests
{
    private GameObject _gameObject;
    private SceneLoader _sceneLoader;
    private bool _progressCalled;
    private float _progressReported;

    [SetUp]
    public void Setup()
    {
        // Crear un GameObject y agregarle el script SceneLoader
        _gameObject = new GameObject();
        _sceneLoader = _gameObject.AddComponent<SceneLoader>();

        // Inicializar variables de prueba
        _progressCalled = false;
        _progressReported = 0f;

        // Suscribirse al evento de progreso
        _sceneLoader.onProgress = new UnityEngine.Events.UnityEvent<float>();
        _sceneLoader.onProgress.AddListener(OnProgress);
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(_gameObject);
    }

    // Método invocado cuando el progreso cambia
    private void OnProgress(float progress)
    {
        _progressCalled = true;
        _progressReported = progress;
    }

    [UnityTest]
    public IEnumerator LoadSceneAsync_ShouldInvokeOnProgress()
    {
        // Actuar: Llamar al método LoadSceneAsync
        _sceneLoader.LoadSceneAsync("TestScene");

        // Verificar que el progreso se reporte antes de que se complete la carga
        while (_progressReported < 0.9f)
        {
            yield return null;  // Esperar hasta que el progreso llegue al 90%
        }

        // Assert: Verificar que el progreso ha sido reportado
        Assert.IsTrue(_progressCalled, "El evento onProgress no fue invocado.");
        Assert.IsTrue(_progressReported >= 0.9f, "El progreso no alcanzó el 90%.");
    }

    [UnityTest]
    public IEnumerator LoadSceneAsync_ShouldCompleteAndActivateScene()
    {
        // Actuar: Llamar al método LoadSceneAsync
        _sceneLoader.LoadSceneAsync("TestScene");

        // Esperar a que la escena alcance el 90% de progreso
        yield return new WaitUntil(() => _progressReported >= 0.9f);

        // Asegurarse de que la escena sea activada manualmente (si es necesario)
        yield return new WaitForSeconds(0.1f); // Simulamos un pequeño retraso

        // Activar la escena manualmente si no se ha activado automáticamente
        if (!SceneManager.GetActiveScene().isLoaded)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("TestScene"));
        }

        // Assert: Verificar que la escena se ha activado correctamente
        Assert.AreEqual("TestScene", SceneManager.GetActiveScene().name, "La escena no se activó correctamente.");
    }
}
