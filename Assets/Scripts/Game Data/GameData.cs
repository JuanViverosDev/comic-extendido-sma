using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public List<string> unlockedChapters;  // Lista de capítulos desbloqueados
    public List<ChapterDecisions> decisionsByChapter;  // Lista de decisiones organizadas por capítulos

    // Constructor que inicializa las listas
    public GameData()
    {
        unlockedChapters = new List<string> { "Cap1" };  // Por defecto, el capítulo 1 está desbloqueado
        decisionsByChapter = new List<ChapterDecisions>();  // Inicializamos la lista vacía
    }
}

// Clase para representar una entrada de decisión
[System.Serializable]
public class DecisionEntry
{
    public string decisionID;  // La clave (ID de la decisión)
    public string decisionValue;  // El valor (la opción seleccionada)
}

// Clase para representar las decisiones de un capítulo
[System.Serializable]
public class ChapterDecisions
{
    public string chapterID;  // El ID del capítulo
    public List<DecisionEntry> decisions;  // Las decisiones tomadas en este capítulo
}
