using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Events;
using System.Collections;

public class TempDialogTests
{
    private GameObject tempDialogObject;
    private TempDialog tempDialog;
    private bool eventCalled;

    [SetUp]
    public void Setup()
    {
        // Crear un GameObject para el TempDialog
        tempDialogObject = new GameObject("TempDialogTestObject");
        tempDialog = tempDialogObject.AddComponent<TempDialog>();

        // Inicializar el tiempo de espera
        tempDialog.time = 1.0f; // 1 segundo

        // Inicializar el evento y conectar un listener
        eventCalled = false;
        tempDialog.onFinish = new UnityEvent();
        tempDialog.onFinish.AddListener(OnEventFinish);
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(tempDialogObject); // Destruir el objeto después de cada prueba
    }

    // Este es el método que será llamado cuando se invoque el evento
    private void OnEventFinish()
    {
        eventCalled = true;
    }

    [UnityTest]
    public IEnumerator TestTempDialogDisablesAfterTime()
    {
        // Inicializar el objeto y ejecutar la corrutina
        tempDialogObject.SetActive(true);
        tempDialog.StartCoroutine(tempDialog.showTemp());

        // Verificar que el objeto esté activo antes del tiempo
        yield return new WaitForSeconds(0.5f);
        Assert.IsTrue(tempDialogObject.activeSelf, "El objeto debería estar activo antes de que el tiempo haya pasado.");

        // Esperar el tiempo completo para que el objeto se desactive
        yield return new WaitForSeconds(0.6f); // 1 segundo en total
        Assert.IsFalse(tempDialogObject.activeSelf, "El objeto debería estar desactivado después de que el tiempo haya pasado.");
    }

    [UnityTest]
    public IEnumerator TestEventOnFinishIsCalled()
    {
        // Inicializar el objeto y ejecutar la corrutina
        tempDialogObject.SetActive(true);
        tempDialog.StartCoroutine(tempDialog.showTemp());

        // Esperar el tiempo completo para que el evento se invoque
        yield return new WaitForSeconds(1.1f);

        // Verificar que el evento haya sido llamado
        Assert.IsTrue(eventCalled, "El evento 'onFinish' debería haberse invocado después de que haya pasado el tiempo.");
    }
}
