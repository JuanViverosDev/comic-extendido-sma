using NUnit.Framework;
using System.IO;
using UnityEngine;

public class SaveSystemTests
{
    private string testFilePath;
    private GameData testGameData;

    [SetUp]
    public void SetUp()
    {
        // Define la ruta de archivo de prueba
        testFilePath = Path.Combine(Application.persistentDataPath, "testSaveData.json");

        // Crea una instancia de datos de juego para pruebas
        testGameData = new GameData
        {
            decisionsByChapter = new(),
            unlockedChapters = new(),
        };
    }

    [Test]
    public void SaveGame_FileIsCreated()
    {
        // Llama al método de guardar juego
        SaveSystem.SaveGame(testGameData);

        // Verifica que el archivo de guardado fue creado
        Assert.IsNotNull(File.Exists(testFilePath), "El archivo de guardado no fue creado.");
    }

    [Test]
    public void LoadGame_ReturnsSavedData()
    {
        // Guarda los datos de prueba en el archivo
        SaveSystem.SaveGame(testGameData);

        // Carga los datos desde el archivo
        GameData loadedData = SaveSystem.LoadGame();

        // Verifica que los datos cargados coinciden con los guardados
        Assert.IsNotNull(testGameData.decisionsByChapter);
        Assert.IsNotNull(testGameData.unlockedChapters);
    }

    [TearDown]
    public void TearDown()
    {
        // Elimina el archivo de prueba después de cada prueba
        if (File.Exists(testFilePath))
        {
            File.Delete(testFilePath);
        }
    }
}
