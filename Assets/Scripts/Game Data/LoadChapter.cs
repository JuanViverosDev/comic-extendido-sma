using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadChapter : MonoBehaviour
{
    public int numberCap;
    public List<Chapter> posibleChapters;
    public BookTimeLine bookTimeLine;
    private GameData gameData;
    private Chapter loadedChapter;

    private void Start()
    {
        if (loadedChapter != null)
        {
            bookTimeLine.SendDialog("0");
        }
    }

    private void Awake()
    {
        gameData = SaveSystem.LoadGame();
        var chapterId = gameData.unlockedChapters[numberCap - 1];
        loadedChapter = posibleChapters.Find(c => c.chapterID == chapterId);

        if (loadedChapter != null)
        {
            bookTimeLine.chapter = loadedChapter;
        }
        else
        {
            bookTimeLine.chapter = posibleChapters[0];
        }
    }

}
