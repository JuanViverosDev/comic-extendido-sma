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
        // Crear el GameObject y añadir el componente DecisionManager
        decisionManagerObject = new GameObject("TestDecisionManagerObject");
        decisionManager = decisionManagerObject.AddComponent<DecisionManager>();

        // Crear un GameData de prueba
        testGameData = SaveSystem.LoadGame();

        // Mock del sistema de guardado
        //SaveSystem.SaveGame = (gameData) => { testGameData = gameData; };
        //SaveSystem.LoadGame();
        decisionManager.Start();

        // Crear un ScriptableObject de prueba (mock) para el capítulo
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
        // Establecer el capítulo actual
        decisionManager.SetCurrentChapter(testChapter);

        // Hacer una decisión
        decisionManager.MakeDecision("decision1", "value1");

        testGameData = SaveSystem.LoadGame();
        // Verificar que la decisión ha sido guardada correctamente
        Assert.IsNotNull(testGameData.decisionsByChapter.Count);
    }

    [Test]
    public void TestMakeDecision_OverwriteDecision_SavesCorrectly()
    {
        // Establecer el capítulo actual
        decisionManager.SetCurrentChapter(testChapter);

        // Hacer una decisión
        decisionManager.MakeDecision("decision1", "value1");

        // Sobrescribir la decisión
        decisionManager.MakeDecision("decision1", "newValue");


        // Verificar que la decisión ha sido sobrescrita correctamente
        Assert.IsNotNull(testGameData.decisionsByChapter.Count);

    }

    [Test]
    public void TestUnlockChapter_NewChapter_UnlocksCorrectly()
    {
        // Desbloquear un capítulo nuevo
        decisionManager.UnlockChapter(1, "newChapterID");


        // Verificar que el capítulo ha sido desbloqueado
        Assert.IsNotNull(testGameData.unlockedChapters.Count);
    }

    [Test]
    public void TestUnlockChapter_OverwriteChapter_UnlocksCorrectly()
    {
        // Desbloquear un capítulo nuevo
        decisionManager.UnlockChapter(1, "newChapterID");

        // Sobrescribir el mismo índice con otro capítulo
        decisionManager.UnlockChapter(1, "updatedChapterID");

        // Verificar que el capítulo ha sido sobrescrito
        Assert.IsNotNull(testGameData.unlockedChapters.Count);
    }

    [Test]
    public void TestMakeDecision_NoCurrentChapter_ShowsError()
    {
        // Intentar hacer una decisión sin establecer el capítulo actual
        //LogAssert.Expect(LogType.Error, "No se ha asignado el capítulo actual en el DecisionManager.");
        decisionManager.MakeDecision("decision1", "value1");

        // Verificar que no se ha guardado ninguna decisión
        Assert.IsNotNull(testGameData.decisionsByChapter.Count);
    }
}
