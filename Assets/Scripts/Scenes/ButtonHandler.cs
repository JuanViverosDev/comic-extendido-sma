using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public Button yourButton; // Asigna el botón desde el Inspector
    public string sceneToLoad; // Nombre de la escena a cargar

    void Start()
    {
        Initialize();  // Llama al método que contiene la lógica que antes estaba en Start
    }

   public void Initialize()
{
    if (yourButton != null && !string.IsNullOrEmpty(sceneToLoad))
    {
        Debug.Log("Adding listener to the button");
        yourButton.onClick.AddListener(() => SceneLoader.Instance.LoadSceneAsync(sceneToLoad));
    }
    else
    {
        Debug.LogError("Button or Scene name not set on " + gameObject.name);
    }
}

}
