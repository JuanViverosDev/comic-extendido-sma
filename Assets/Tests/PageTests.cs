using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PageTests
{
    private Page testPage;
    private Decision testDecision;
    private Dialog testDialog;

    [SetUp]
    public void Setup()
    {
        // Crear instancias de prueba para cada clase
        testPage = ScriptableObject.CreateInstance<Page>();
        testPage.resultID = " ";
        testPage.dialogs = new();
        testPage.decisions = new();
        testDecision = new Decision();
        testDecision.id = "";
        testDecision.nextId = "";
        testDecision.options = new();
        testDecision.text = "";

        testDialog = new Dialog();
        testDialog.id = "";
        testDialog.nextId = "";
        testDialog.DialogCharacter = Characters.Narrador;
        testDialog.text = "";
        testDialog.screenCharacters = new();
    }

    [TearDown]
    public void Teardown()
    {
        // Limpiar objetos después de cada prueba
        Object.DestroyImmediate(testPage);
    }

    [Test]
    public void TestPageInitialization()
    {
        // Verificar que la página se inicializa correctamente con valores predeterminados
        Assert.IsNotNull(testPage, "La página debería estar inicializada.");
        Assert.IsNull(testPage.background, "El fondo debería ser nulo al inicio.");
        Assert.IsFalse(testPage.isInteractive, "El valor de isInteractive debería ser false por defecto.");
        Assert.IsFalse(testPage.isFinal, "El valor de isFinal debería ser false por defecto.");
        Assert.AreEqual(" ", testPage.resultID, "El resultID debería estar vacío por defecto.");
        Assert.AreEqual(0, testPage.dialogs.Count, "La lista de diálogos debería estar vacía.");
        Assert.AreEqual(0, testPage.decisions.Count, "La lista de decisiones debería estar vacía.");
    }

    [Test]
    public void TestDecisionInitialization()
    {
        // Verificar que la decisión se inicializa correctamente con valores predeterminados
        Assert.IsNotNull(testDecision, "La decisión debería estar inicializada.");
        Assert.AreEqual("", testDecision.id, "El id de la decisión debería estar vacío por defecto.");
        Assert.AreEqual("", testDecision.nextId, "El nextId de la decisión debería estar vacío por defecto.");
        Assert.AreEqual("", testDecision.text, "El texto de la decisión debería estar vacío.");
        Assert.AreEqual(0, testDecision.options.Count, "La lista de opciones debería estar vacía.");
    }

    [Test]
    public void TestDialogInitialization()
    {
        // Verificar que el diálogo se inicializa correctamente con valores predeterminados
        Assert.IsNotNull(testDialog, "El diálogo debería estar inicializado.");
        Assert.AreEqual("", testDialog.id, "El id del diálogo debería estar vacío por defecto.");
        Assert.AreEqual("", testDialog.nextId, "El nextId del diálogo debería estar vacío por defecto.");
        Assert.AreEqual(Characters.Narrador, testDialog.DialogCharacter, "El personaje por defecto debería ser el Narrador.");
        Assert.AreEqual("", testDialog.text, "El texto del diálogo debería estar vacío.");
        Assert.AreEqual(0, testDialog.screenCharacters.Count, "La lista de personajes en pantalla debería estar vacía.");
        Assert.IsNull(testDialog.otherImage, "La imagen 'otherImage' debería ser nula por defecto.");
    }

    [Test]
    public void TestAssignDecisionToPage()
    {
        // Asignar una decisión a una página y verificar si se asigna correctamente
        DecisionOption option = new DecisionOption
        {
            OptionID = "opt1",
            optionLabel = "Opción 1",
            contentLabel = "Contenido de opción 1",
            retroalimentation = "Feedback de la opción 1",
            pages = new List<Page> { testPage } // La página actual como una de las opciones
        };

        testDecision.id = "decision1";
        testDecision.text = "¿Qué quieres hacer?";
        testDecision.options.Add(option);

        testPage.decisions.Add(testDecision);

        Assert.AreEqual(1, testPage.decisions.Count, "Debería haber una decisión asignada a la página.");
        Assert.AreEqual(testDecision, testPage.decisions[0], "La decisión asignada debería coincidir con la esperada.");
        Assert.AreEqual(1, testPage.decisions[0].options.Count, "Debería haber una opción en la decisión.");
        Assert.AreEqual(option, testPage.decisions[0].options[0], "La opción debería coincidir con la opción asignada.");
    }

    [Test]
    public void TestAssignDialogToPage()
    {
        // Asignar un diálogo a una página y verificar si se asigna correctamente
        testDialog.id = "dialog1";
        testDialog.text = "Hola, soy un diálogo de prueba.";
        testDialog.DialogCharacter = Characters.Jhon;

        testPage.dialogs.Add(testDialog);

        Assert.AreEqual(1, testPage.dialogs.Count, "Debería haber un diálogo asignado a la página.");
        Assert.AreEqual(testDialog, testPage.dialogs[0], "El diálogo asignado debería coincidir con el esperado.");
        Assert.AreEqual(Characters.Jhon, testPage.dialogs[0].DialogCharacter, "El personaje del diálogo debería ser Jhon.");
    }

    [Test]
    public void TestDecisionOptionContainsPage()
    {
        // Verificar que una opción de decisión contiene una página de forma correcta
        DecisionOption option = new DecisionOption
        {
            OptionID = "opt1",
            optionLabel = "Opción 1",
            contentLabel = "Contenido de opción 1",
            retroalimentation = "Feedback de la opción 1",
            pages = new List<Page> { testPage }
        };

        Assert.AreEqual(1, option.pages.Count, "La opción debería contener una página.");
        Assert.AreEqual(testPage, option.pages[0], "La página contenida en la opción debería coincidir con la esperada.");
    }
}
