using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Chapter", menuName = "Chapter/Create New Chapter")]
public class Chapter : ScriptableObject
{
    public List<Pages> pages;
}

[Serializable]
public enum Characters
{
    AsesorCarlos = 0,
    JhonMayor = 1,
    Jhon = 2,
    JulianaMayor = 3,
    Juliana = 4,
    Santiago = 5,
    SantiagoMayor = 6,
    Narrador = -1
}

[Serializable]
public class Pages
{
    public Sprite background;
    public bool isInteractive;
    public List<Dialog> dialogs;
    public List<Decision> decisions;
}

[Serializable]
public class Decision
{
    public string id;
    public string nextId;
    [TextArea(2, 10)]
    public string text;
    public List<DecisionOption> options;
}

[Serializable]
public class DecisionOption
{
    public string optionLabel;
    [TextArea(5, 10)]
    public string contentLabel;
    public int pageResult;
}


[Serializable]
public class Dialog
{
    public string id;
    public string nextId;
    public Characters DialogCharacter;

    [TextArea(3, 10)]
    public string text;

    public List<Characters> screenCharacters;
    public Sprite otherImage;
    //public bool sendToInteractivePage;
}
