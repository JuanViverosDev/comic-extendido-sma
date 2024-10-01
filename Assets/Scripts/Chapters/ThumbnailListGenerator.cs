using UnityEngine;
using UnityEngine.UI;
using System.Linq;  


public class ThumbnailListGenerator : MonoBehaviour
{
    public GameObject thumbnailPrefab; // Prefab de la miniatura
    public Transform contentParent; // El contenedor que tendrá todas las miniaturas
    public ThumbnailData[] thumbnailsData; // Array de ScriptableObjects con los datos de las miniaturas

    public Material grayscaleMaterial; // Material en escala de grises para capítulos bloqueados
    public Material defaultMaterial;   // Material por defecto para capítulos desbloqueados
    public Color32 lockedColor = new Color32(60, 57, 55, 255);
    public Color32 defaultColor = new Color32(190, 91, 0, 254);

    void Start()
    {
        GenerateThumbnails();
    }

    void GenerateThumbnails()
    {
        // Cargar el progreso del jugador (capítulos desbloqueados)
        GameData data = SaveSystem.LoadGame();

        foreach (ThumbnailData chapterData in thumbnailsData)
        {
            // Instanciar una nueva miniatura
            GameObject newThumbnail = Instantiate(thumbnailPrefab, contentParent);

            // Obtener referencias a los elementos dentro de la miniatura (Imagen y Textos)
            Image thumbnailImage = newThumbnail.transform.Find("ThumbnailImage").GetComponent<Image>();
            Image thumbnailBackground = newThumbnail.GetComponent<Image>();
            Text titleText = newThumbnail.transform.Find("TitleText").GetComponent<Text>();
            Text descriptionText = newThumbnail.transform.Find("DescriptionText").GetComponent<Text>();

            // Ahora obtenemos el ButtonHandler que está en el GameObject ThumbnailImage
            ButtonHandler buttonHandler = newThumbnail.transform.Find("ThumbnailImage").GetComponent<ButtonHandler>();

            // Asignar los datos desde el ScriptableObject
            thumbnailImage.sprite = chapterData.thumbnailImage;
            titleText.text = chapterData.titleText;
            descriptionText.text = chapterData.descriptionText;

            // Verificar si el capítulo está desbloqueado
            if (data.unlockedChapters.Any(id => id.Length >= 3 && id.Substring(0, 4) == chapterData.chapterID.Substring(0, 4)))
            {
                // Capítulo desbloqueado: Usar material normal y activar el botón
                thumbnailImage.material = defaultMaterial;
                thumbnailBackground.color = defaultColor;

                if (buttonHandler != null)
                {
                    // Asignar el nombre de la escena desde el ScriptableObject al campo 'sceneToLoad'
                    buttonHandler.sceneToLoad = chapterData.sceneToLoad; // Aquí se cambia el valor automáticamente
                    buttonHandler.Initialize(); // Configurar el botón
                }
                else
                {
                    Debug.LogError("ButtonHandler no encontrado en el objeto ThumbnailImage en el prefab " + newThumbnail.name);
                }
            }
            else
            {
                // Capítulo bloqueado: Aplicar escala de grises y desactivar botón
                thumbnailImage.material = grayscaleMaterial;
                thumbnailBackground.color = lockedColor;

                
            }
        }
    }
}
