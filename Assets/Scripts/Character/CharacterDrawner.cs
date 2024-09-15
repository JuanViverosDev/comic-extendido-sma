using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Character))]
public class CharacterDrawer : PropertyDrawer
{
    private const float spriteHeight = 100f;
    private const float padding = 5f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var nameRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("name"), new GUIContent("Name"));

        var iconRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + padding, spriteHeight/2, spriteHeight/2);
        var iconProp = property.FindPropertyRelative("icon");

        EditorGUI.ObjectField(iconRect, iconProp, typeof(Sprite), GUIContent.none);

        if (iconProp.objectReferenceValue != null)
        {
            Sprite iconSprite = iconProp.objectReferenceValue as Sprite;
            if (iconSprite != null)
            {
                Rect textureRect = iconSprite.textureRect;
                textureRect.x /= iconSprite.texture.width;
                textureRect.width /= iconSprite.texture.width;
                textureRect.y /= iconSprite.texture.height;
                textureRect.height /= iconSprite.texture.height;

                GUI.DrawTextureWithTexCoords(iconRect, iconSprite.texture, textureRect);
            }
        }

        var imageRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + padding + spriteHeight/2 + padding, spriteHeight, spriteHeight);
        var imageProp = property.FindPropertyRelative("image");

        EditorGUI.ObjectField(imageRect, imageProp, typeof(Sprite), GUIContent.none);

        if (imageProp.objectReferenceValue != null)
        {
            Sprite imageSprite = imageProp.objectReferenceValue as Sprite;
            if (imageSprite != null)
            {
                Rect textureRect = imageSprite.textureRect;
                textureRect.x /= imageSprite.texture.width;
                textureRect.width /= imageSprite.texture.width;
                textureRect.y /= imageSprite.texture.height;
                textureRect.height /= imageSprite.texture.height;

                GUI.DrawTextureWithTexCoords(imageRect, imageSprite.texture, textureRect);
            }
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight + spriteHeight * 2 + padding * 3;
    }
}
