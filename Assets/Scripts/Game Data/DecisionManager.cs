using System.Collections.Generic;
using UnityEngine;

public class DecisionManager : MonoBehaviour
{
    private Chapter currentChapter;  // Referencia al ScriptableObject que contiene el chapterID
    private GameData gameData;

    public void Start()
    {
        // Cargar los datos guardados al iniciar el juego
        gameData = SaveSystem.LoadGame();
    }

    public void SetCurrentChapter(Chapter chapter)
    {
        currentChapter = chapter;
    }

    // Método para guardar una decisión
    public void MakeDecision(string decisionID, string decisionValue)
    {
        if (currentChapter == null)
        {
            Debug.LogError("No se ha asignado el capítulo actual en el DecisionManager.");
            return;
        }

        // Obtener el chapterID desde el ScriptableObject Chapter
        string chapterID = currentChapter.chapterID;

        // Buscar si ya existe una entrada para este capítulo
        ChapterDecisions chapterDecisions = gameData.decisionsByChapter.Find(cd => cd.chapterID == chapterID);

        if (chapterDecisions == null)
        {
            // Si no existe una entrada para este capítulo, la creamos
            chapterDecisions = new ChapterDecisions { chapterID = chapterID, decisions = new List<DecisionEntry>() };
            gameData.decisionsByChapter.Add(chapterDecisions);
        }

        // Buscar si la decisión ya existe en la lista de este capítulo
        DecisionEntry existingDecision = chapterDecisions.decisions.Find(d => d.decisionID == decisionID);

        if (existingDecision != null)
        {
            // Si ya existe, sobrescribimos el valor de la decisión
            existingDecision.decisionValue = decisionValue;
        }
        else
        {
            // Si no existe, creamos una nueva entrada en la lista
            DecisionEntry newDecision = new DecisionEntry { decisionID = decisionID, decisionValue = decisionValue };
            chapterDecisions.decisions.Add(newDecision);
        }

        // Guardar los datos actualizados
        SaveSystem.SaveGame(gameData);

        Debug.Log("Decisión guardada para el capítulo " + chapterID + ": " + decisionID + " = " + decisionValue);
    }

    public void UnlockChapter(int newChapterIndex, string newChapterId)
    {
        if(gameData.unlockedChapters.Count < newChapterIndex)
        {
            gameData.unlockedChapters.Add(newChapterId);
        }
        else
        {
            gameData.unlockedChapters[newChapterIndex-1] = newChapterId;
        }

        // Guardar los datos actualizados
        SaveSystem.SaveGame(gameData);

        Debug.Log("Capitulo desbloqueado " + newChapterId);
    }

}