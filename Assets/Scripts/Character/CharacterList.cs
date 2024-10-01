using UnityEngine;

[CreateAssetMenu(fileName = "CharacterList", menuName = "Character/Create New Character List")]
public class CharacterList : ScriptableObject
{
    public Character[] characters;
}
