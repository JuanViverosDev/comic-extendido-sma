using UnityEngine;

public class SalirApp : MonoBehaviour
{
    public void Salir()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
