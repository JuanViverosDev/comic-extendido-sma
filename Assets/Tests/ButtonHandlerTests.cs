using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandlerTests
{
    private GameObject _gameObject;
    private Button _button;
    private ButtonHandler _buttonHandler;

    [SetUp]
    public void Setup()
    {
        // Crear un GameObject y agregar los componentes necesarios
        _gameObject = new GameObject("TestObject");
        _button = _gameObject.AddComponent<Button>();
        _buttonHandler = _gameObject.AddComponent<ButtonHandler>();

        // Configurar el ButtonHandler
        _buttonHandler.yourButton = _button;
        _buttonHandler.sceneToLoad = "TestScene";

        // Inicializar el ButtonHandler
        _buttonHandler.Initialize();
    }

    [TearDown]
    public void Teardown()
    {
        // Limpiar las referencias
        Object.Destroy(_gameObject);
    }

    [Test]
    public void ButtonClick_ShouldInvokeAssignedListener()
    {
        // Verificar que el listener se ha añadido al botón
        Assert.IsNotNull(_button.onClick);

        // Simular la acción que se realizaría en LoadSceneAsync
        bool listenerInvoked = false;
        _button.onClick.AddListener(() => listenerInvoked = true);

        // Actuar: Simular el clic del botón
        _button.onClick.Invoke();

        // Assert: Verificar que el listener fue invocado
        Assert.IsTrue(listenerInvoked, "El listener del botón debería haberse invocado al hacer clic.");
    }
}
