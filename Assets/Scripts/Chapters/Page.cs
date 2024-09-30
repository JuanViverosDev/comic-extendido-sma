using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "Page", menuName = "Chapter/Crear nueva pagina")]
public class Page : ScriptableObject
{
    public BookBg background;
    public bool isInteractive;
    public bool isFinal;
    public string resultID;
    public List<Dialog> dialogs;
    public List<Decision> decisions;
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
    public string OptionID;
    public string optionLabel;
    [TextArea(5, 10)]
    public string contentLabel;
    [TextArea(3, 10)]
    public string retroalimentation;
    public List<Page> pages;
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