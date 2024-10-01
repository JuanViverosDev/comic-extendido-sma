using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Chapter", menuName = "Chapter/Crear nuevo capitulo")]
public class Chapter : ScriptableObject
{
    public int chapterIndex;
    public string chapterID;
    public List<Page> pages;
}

