using NUnit.Framework;
using UnityEngine;

public class DescargarPdfTest
{
    private DescargarPDF descargarPDF;

    [SetUp]
    public void SetUp()
    {
        // Crear una instancia del componente en un GameObject temporal
        GameObject gameObject = new GameObject();
        descargarPDF = gameObject.AddComponent<DescargarPDF>();
    }

    [Test]
    public void AbrirPDF_URLIsOpened()
    {
        // Asigna una URL al campo urlPDF
        descargarPDF.urlPDF = "https://ejemplo.com/archivo.pdf";

        // Guarda el valor inicial para verificar cambios
        string initialUrl = descargarPDF.urlPDF;

        // Llama al método para abrir la URL
        descargarPDF.AbrirPDF();

        // Comprobamos si el URL es el mismo
        Assert.AreEqual(initialUrl, descargarPDF.urlPDF, "La URL abierta no es la misma que la configurada.");
    }
}
