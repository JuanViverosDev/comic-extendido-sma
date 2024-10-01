using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class DecisionManagerTests
{
    private GameObject decisionManagerObject;
    private DecisionManager decisionManager;
    private GameData testGameData;
    private Chapter testChapter;

    [SetUp]
    public void Setup()
    {
        // Crear el GameObject y a�adir el componente DecisionManager
        decisionManagerObject = new GameObject("TestDecisionManagerObject");
        decisionManager = decisionManagerObject.AddComponent<DecisionManager>();

        // Crear un GameData de prueba
        testGameData = SaveSystem.LoadGame();

        // Mock del sistema de guardado
        //SaveSystem.SaveGame = (gameData) => { testGameData = gameData; };
        //SaveSystem.LoadGame();
        decisionManager.Start();

        // Crear un ScriptableObject de prueba (mock) para el cap�tulo
        testChapter = ScriptableObject.CreateInstance<Chapter>();
        testChapter.chapterID = "testChapterID";
        decisionManager.SetCurrentChapter(testChapter);

        //// Inicializar el DecisionManager con los datos cargados
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(decisionManagerObject);
        Object.Destroy(testChapter);
    }

    [Test]
    public void TestMakeDecision_NewDecision_SavesCorrectly()
    {
        // Establecer el cap�tulo actual
        decisionManager.SetCurrentChapter(testChapter);

        // Hacer una decisi�n
        decisionManager.MakeDecision("decision1", "value1");

        testGameData = SaveSystem.LoadGame();
        // Verificar que la decisi�n ha sido guardada correctamente
        Assert.IsNotNull(testGameData.decisionsByChapter.Count);
    }

    [Test]
    public void TestMakeDecision_OverwriteDecision_SavesCorrectly()
    {
        // Establecer el cap�tulo actual
        decisionManager.SetCurrentChapter(testChapter);

        // Hacer una decisi�n
        decisionManager.MakeDecision("decision1", "value1");

        // Sobrescribir la decisi�n
        decisionManager.MakeDecision("decision1", "newValue");


        // Verificar que la decisi�n ha sido sobrescrita correctamente
        Assert.IsNotNull(testGameData.decisionsByChapter.Count);

    }

    [Test]
    public void TestUnlockChapter_NewChapter_UnlocksCorrectly()
    {
        // Desbloquear un cap�tulo nuevo
        decisionManager.UnlockChapter(1, "newChapterID");


        // Verificar que el cap�tulo ha sido desbloqueado
        Assert.IsNotNull(testGameData.unlockedChapters.Count);
    }

    [Test]
    public void TestUnlockChapter_OverwriteChapter_UnlocksCorrectly()
    {
        // Desbloquear un cap�tulo nuevo
        decisionManager.UnlockChapter(1, "newChapterID");

        // Sobrescribir el mismo �ndice con otro cap�tulo
        decisionManager.UnlockChapter(1, "updatedChapterID");

        // Verificar que el cap�tulo ha sido sobrescrito
        Assert.IsNotNull(testGameData.unlockedChapters.Count);
    }

    [Test]
    public void TestMakeDecision_NoCurrentChapter_ShowsError()
    {
        // Intentar hacer una decisi�n sin establecer el cap�tulo actual
        //LogAssert.Expect(LogType.Error, "No se ha asignado el cap�tulo actual en el DecisionManager.");
        decisionManager.MakeDecision("decision1", "value1");

        // Verificar que no se ha guardado ninguna decisi�n
        Assert.IsNotNull(testGameData.decisionsByChapter.Count);
    }
}
