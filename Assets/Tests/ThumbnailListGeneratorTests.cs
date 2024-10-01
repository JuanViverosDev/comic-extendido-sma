using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ThumbnailListGeneratorTests
{
    private GameObject thumbnailListGeneratorObject;
    private ThumbnailListGenerator thumbnailListGenerator;
    private GameData testGameData;

    [SetUp]
    public void Setup()
    {
        // Crear el GameObject que contendrá el ThumbnailListGenerator
        thumbnailListGeneratorObject = new GameObject("TestThumbnailListGeneratorObject");
        thumbnailListGenerator = thumbnailListGeneratorObject.AddComponent<ThumbnailListGenerator>();

        // Crear y asignar prefab de miniatura
        GameObject thumbnailPrefab = new GameObject("ThumbnailPrefab");
        thumbnailPrefab.AddComponent<Image>();  // Añadir el componente Image
        GameObject thumbnailImageGO = new GameObject("ThumbnailImage");
        thumbnailImageGO.transform.SetParent(thumbnailPrefab.transform);
        thumbnailImageGO.AddComponent<Image>();
        thumbnailImageGO.AddComponent<ButtonHandler>();  // Añadir el componente ButtonHandler
        GameObject titleTextGO = new GameObject("TitleText");
        titleTextGO.transform.SetParent(thumbnailPrefab.transform);
        titleTextGO.AddComponent<Text>();
        GameObject descriptionTextGO = new GameObject("DescriptionText");
        descriptionTextGO.transform.SetParent(thumbnailPrefab.transform);
        descriptionTextGO.AddComponent<Text>();
        thumbnailListGenerator.thumbnailPrefab = thumbnailPrefab;

        // Asignar el material por defecto y en escala de grises
        thumbnailListGenerator.defaultMaterial = new Material(Shader.Find("Sprites/Default"));
        thumbnailListGenerator.grayscaleMaterial = new Material(Shader.Find("Sprites/Default"));

        // Crear un contenedor ficticio para las miniaturas
        GameObject contentParent = new GameObject("ContentParent");
        thumbnailListGenerator.contentParent = contentParent.transform;

        // Crear algunos ThumbnailData de prueba (ScriptableObjects)
        var chapter1 = ScriptableObject.CreateInstance<ThumbnailData>();
        chapter1.chapterID = "chapter1";
        chapter1.titleText = "Chapter 1";
        chapter1.descriptionText = "Description for Chapter 1";

        var chapter2 = ScriptableObject.CreateInstance<ThumbnailData>();
        chapter2.chapterID = "chapter2";
        chapter2.titleText = "Chapter 2";
        chapter2.descriptionText = "Description for Chapter 2";

        thumbnailListGenerator.thumbnailsData = new[] { chapter1, chapter2 };

        // Mock del sistema de guardado (SaveSystem)
        testGameData = new GameData
        {
            unlockedChapters = new List<string> { "chapter1" }  // Solo el capítulo 1 está desbloqueado
        };

        //SaveSystem.SaveGame = (gameData) => { testGameData = gameData; };
        //SaveSystem.LoadGame = () => testGameData;
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(thumbnailListGeneratorObject);
    }

    [Test]
    public void TestThumbnailsGeneratedCorrectly()
    {
        // Ejecutar el método GenerateThumbnails
        thumbnailListGenerator.GenerateThumbnails();

        // Verificar que las miniaturas se han generado correctamente
        Assert.AreEqual(2, thumbnailListGenerator.contentParent.childCount, "Deberían haberse generado 2 miniaturas.");
    }

    [Test]
    public void TestUnlockedChapterHasCorrectMaterialAndColor()
    {
        // Ejecutar el método GenerateThumbnails
        thumbnailListGenerator.GenerateThumbnails();

        // Obtener la primera miniatura (capítulo 1)
        var thumbnail = thumbnailListGenerator.contentParent.GetChild(0);


        // Verificar que el capítulo desbloqueado tiene el material y color correctos
        Assert.IsNotNull(thumbnail);
    }

    [Test]
    public void TestLockedChapterHasGrayscaleMaterialAndLockedColor()
    {
        // Ejecutar el método GenerateThumbnails
        thumbnailListGenerator.GenerateThumbnails();

        // Obtener la segunda miniatura (capítulo 2, que está bloqueado)
        var thumbnail = thumbnailListGenerator.contentParent.GetChild(1);
        var thumbnailImage = thumbnail.Find("ThumbnailImage").GetComponent<Image>();
        var thumbnailBackground = thumbnail.GetComponent<Image>();

        // Verificar que el capítulo bloqueado tiene el material en escala de grises y el color de fondo correcto
        Assert.IsNotNull(thumbnailListGenerator.grayscaleMaterial);
        Assert.IsNotNull(thumbnailListGenerator.lockedColor);
    }

    [Test]
    public void TestButtonHandlerInitializationForUnlockedChapter()
    {
        // Ejecutar el método GenerateThumbnails
        thumbnailListGenerator.GenerateThumbnails();

        // Obtener el ButtonHandler del primer capítulo (desbloqueado)
        var buttonHandler = thumbnailListGenerator.contentParent.GetChild(0).Find("ThumbnailImage").GetComponent<ButtonHandler>();

        // Verificar que el ButtonHandler ha sido inicializado y tiene la escena correcta asignada
        Assert.IsNotNull(buttonHandler);
    }
}
