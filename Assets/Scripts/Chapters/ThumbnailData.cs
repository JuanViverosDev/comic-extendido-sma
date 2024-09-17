using UnityEngine;

[CreateAssetMenu(fileName = "NewThumbnailData", menuName = "Thumbnail Data", order = 1)]
public class ThumbnailData : ScriptableObject
{
    public Sprite thumbnailImage;
    public string titleText;
    public string descriptionText;

    public string sceneToLoad;
}
