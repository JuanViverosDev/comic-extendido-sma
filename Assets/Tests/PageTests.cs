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
        // Limpiar objetos despu�s de cada prueba
        Object.DestroyImmediate(testPage);
    }

    [Test]
    public void TestPageInitialization()
    {
        // Verificar que la p�gina se inicializa correctamente con valores predeterminados
        Assert.IsNotNull(testPage, "La p�gina deber�a estar inicializada.");
        Assert.IsNull(testPage.background, "El fondo deber�a ser nulo al inicio.");
        Assert.IsFalse(testPage.isInteractive, "El valor de isInteractive deber�a ser false por defecto.");
        Assert.IsFalse(testPage.isFinal, "El valor de isFinal deber�a ser false por defecto.");
        Assert.AreEqual(" ", testPage.resultID, "El resultID deber�a estar vac�o por defecto.");
        Assert.AreEqual(0, testPage.dialogs.Count, "La lista de di�logos deber�a estar vac�a.");
        Assert.AreEqual(0, testPage.decisions.Count, "La lista de decisiones deber�a estar vac�a.");
    }

    [Test]
    public void TestDecisionInitialization()
    {
        // Verificar que la decisi�n se inicializa correctamente con valores predeterminados
        Assert.IsNotNull(testDecision, "La decisi�n deber�a estar inicializada.");
        Assert.AreEqual("", testDecision.id, "El id de la decisi�n deber�a estar vac�o por defecto.");
        Assert.AreEqual("", testDecision.nextId, "El nextId de la decisi�n deber�a estar vac�o por defecto.");
        Assert.AreEqual("", testDecision.text, "El texto de la decisi�n deber�a estar vac�o.");
        Assert.AreEqual(0, testDecision.options.Count, "La lista de opciones deber�a estar vac�a.");
    }

    [Test]
    public void TestDialogInitialization()
    {
        // Verificar que el di�logo se inicializa correctamente con valores predeterminados
        Assert.IsNotNull(testDialog, "El di�logo deber�a estar inicializado.");
        Assert.AreEqual("", testDialog.id, "El id del di�logo deber�a estar vac�o por defecto.");
        Assert.AreEqual("", testDialog.nextId, "El nextId del di�logo deber�a estar vac�o por defecto.");
        Assert.AreEqual(Characters.Narrador, testDialog.DialogCharacter, "El personaje por defecto deber�a ser el Narrador.");
        Assert.AreEqual("", testDialog.text, "El texto del di�logo deber�a estar vac�o.");
        Assert.AreEqual(0, testDialog.screenCharacters.Count, "La lista de personajes en pantalla deber�a estar vac�a.");
        Assert.IsNull(testDialog.otherImage, "La imagen 'otherImage' deber�a ser nula por defecto.");
    }

    [Test]
    public void TestAssignDecisionToPage()
    {
        // Asignar una decisi�n a una p�gina y verificar si se asigna correctamente
        DecisionOption option = new DecisionOption
        {
            OptionID = "opt1",
            optionLabel = "Opci�n 1",
            contentLabel = "Contenido de opci�n 1",
            retroalimentation = "Feedback de la opci�n 1",
            pages = new List<Page> { testPage } // La p�gina actual como una de las opciones
        };

        testDecision.id = "decision1";
        testDecision.text = "�Qu� quieres hacer?";
        testDecision.options.Add(option);

        testPage.decisions.Add(testDecision);

        Assert.AreEqual(1, testPage.decisions.Count, "Deber�a haber una decisi�n asignada a la p�gina.");
        Assert.AreEqual(testDecision, testPage.decisions[0], "La decisi�n asignada deber�a coincidir con la esperada.");
        Assert.AreEqual(1, testPage.decisions[0].options.Count, "Deber�a haber una opci�n en la decisi�n.");
        Assert.AreEqual(option, testPage.decisions[0].options[0], "La opci�n deber�a coincidir con la opci�n asignada.");
    }

    [Test]
    public void TestAssignDialogToPage()
    {
        // Asignar un di�logo a una p�gina y verificar si se asigna correctamente
        testDialog.id = "dialog1";
        testDialog.text = "Hola, soy un di�logo de prueba.";
        testDialog.DialogCharacter = Characters.Jhon;

        testPage.dialogs.Add(testDialog);

        Assert.AreEqual(1, testPage.dialogs.Count, "Deber�a haber un di�logo asignado a la p�gina.");
        Assert.AreEqual(testDialog, testPage.dialogs[0], "El di�logo asignado deber�a coincidir con el esperado.");
        Assert.AreEqual(Characters.Jhon, testPage.dialogs[0].DialogCharacter, "El personaje del di�logo deber�a ser Jhon.");
    }

    [Test]
    public void TestDecisionOptionContainsPage()
    {
        // Verificar que una opci�n de decisi�n contiene una p�gina de forma correcta
        DecisionOption option = new DecisionOption
        {
            OptionID = "opt1",
            optionLabel = "Opci�n 1",
            contentLabel = "Contenido de opci�n 1",
            retroalimentation = "Feedback de la opci�n 1",
            pages = new List<Page> { testPage }
        };

        Assert.AreEqual(1, option.pages.Count, "La opci�n deber�a contener una p�gina.");
        Assert.AreEqual(testPage, option.pages[0], "La p�gina contenida en la opci�n deber�a coincidir con la esperada.");
    }
}
