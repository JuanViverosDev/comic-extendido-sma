using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonHandler : MonoBehaviour
{
    public Button yourButton; // Asigna el botón desde el Inspector
    public string sceneToLoad; // Nombre de la escena a cargar

    void Start()
    {
        // Verifica que el botón y el nombre de la escena estén asignados
        if (yourButton != null && !string.IsNullOrEmpty(sceneToLoad))
        {
            yourButton.onClick.AddListener(() => SceneLoader.Instance.LoadSceneAsync(sceneToLoad));
        }
        else
        {
            Debug.LogError("Button or Scene name not set on " + gameObject.name);
        }
    }
}
