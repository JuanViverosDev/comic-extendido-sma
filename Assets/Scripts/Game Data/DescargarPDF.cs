using System.IO;
using UnityEngine;

public class DescargarPDF : MonoBehaviour
{
    [Header("URL del PDF en línea")]
    public string urlPDF = "";

    public void AbrirPDF()
    {
        Application.OpenURL(urlPDF);
    }
}
