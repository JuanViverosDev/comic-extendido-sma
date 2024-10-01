using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using System.Collections;
public class BookTests
{
    private GameObject bookGameObject;
    private Book book;

    [SetUp]
    public void Setup()
    {
        // Crear el objeto y los componentes necesarios para la prueba
        bookGameObject = new GameObject("TestBook");
        var canvasGO = new GameObject("TestCanvas");
        var canvas = canvasGO.AddComponent<Canvas>();
        var bookPanelGO = new GameObject("BookPanel");
        //var bookPanelRect = bookPanelGO.AddComponent<RectTransform>();
        var clippingPlane = new GameObject("ClippingPlane").AddComponent<Image>();
        var nextPageClip = new GameObject("NextPageClip").AddComponent<Image>();
        var shadow = new GameObject("Shadow").AddComponent<Image>();
        var shadowLTR = new GameObject("ShadowLTR").AddComponent<Image>();
        var left = new GameObject("Left").AddComponent<Image>();
        var right = new GameObject("Right").AddComponent<Image>();
        var leftNext = new GameObject("LeftNext").AddComponent<Image>();
        var rightNext = new GameObject("RightNext").AddComponent<Image>();

        // Asignar componentes al script Book
        book = bookGameObject.AddComponent<Book>();
        book.canvas = canvas;
        //book.BookPanel = bookPanelRect;
        book.ClippingPlane = clippingPlane;
        book.NextPageClip = nextPageClip;
        book.Shadow = shadow;
        book.ShadowLTR = shadowLTR;
        book.Left = left;
        book.Right = right;
        book.LeftNext = leftNext;
        book.RightNext = rightNext;

        // Asignar otros valores iniciales si es necesario
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(bookGameObject); // Destruir el objeto después de cada prueba
    }

    [Test]
    public void TestPage()
    {
        Assert.AreEqual(0, book.currentPage, "La página inicial debe ser 0.");
    }
}
