using UnityEngine;
using UnityEngine.UI;

public class ThumbnailListGenerator : MonoBehaviour
{
    public GameObject thumbnailPrefab; // Prefab de la miniatura
    public Transform contentParent; // El contenedor que tendrá todas las miniaturas
    public ThumbnailData[] thumbnailsData; // Array de ScriptableObjects con los datos de las miniaturas

    void Start()
    {
        GenerateThumbnails();
    }

    void GenerateThumbnails()
    {
        foreach (ThumbnailData data in thumbnailsData)
        {
            // Instanciar una nueva miniatura
            GameObject newThumbnail = Instantiate(thumbnailPrefab, contentParent);

            // Obtener referencias a los elementos dentro de la miniatura (Imagen y Textos)
            Image thumbnailImage = newThumbnail.transform.Find("ThumbnailImage").GetComponent<Image>();
            Text titleText = newThumbnail.transform.Find("TitleText").GetComponent<Text>();
            Text descriptionText = newThumbnail.transform.Find("DescriptionText").GetComponent<Text>();

            // Ahora obtenemos el ButtonHandler que está en el GameObject ThumbnailImage
            ButtonHandler buttonHandler = newThumbnail.transform.Find("ThumbnailImage").GetComponent<ButtonHandler>();

            // Asignar los datos desde el ScriptableObject
            thumbnailImage.sprite = data.thumbnailImage;
            titleText.text = data.titleText;
            descriptionText.text = data.descriptionText;

            // Configurar el botón para cambiar de escena
            if (buttonHandler != null)
            {
                // Asignar el nombre de la escena desde el ScriptableObject al campo 'sceneToLoad'
                buttonHandler.sceneToLoad = data.sceneToLoad; // Aquí se cambia el valor automáticamente
                buttonHandler.Initialize(); // Configurar el botón
            }
            else
            {
                Debug.LogError("ButtonHandler no encontrado en el objeto ThumbnailImage en el prefab " + newThumbnail.name);
            }
        }
    }
}

