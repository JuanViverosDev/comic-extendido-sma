using System.IO; 
using UnityEngine; 

public static class SaveSystem
{
    // Ruta del archivo donde se guardarán los datos. Usa Application.persistentDataPath para móviles.
    private static string filePath = Path.Combine(Application.persistentDataPath, "saveData.json");

    
    public static void SaveGame(GameData data)
    {
        
        string json = JsonUtility.ToJson(data, true); 
        
        File.WriteAllText(filePath, json);
        Debug.Log("Juego guardado en: " + filePath); 
    }

    
    public static GameData LoadGame()
    {
        
        if (File.Exists(filePath))
        {
            
            string json = File.ReadAllText(filePath);
            
            GameData data = JsonUtility.FromJson<GameData>(json);
            Debug.Log("Juego cargado desde: " + filePath); 
            return data;
        }
        else
        {
            Debug.LogWarning("Archivo de guardado no encontrado, creando nuevos datos.");
            return new GameData(); 
        }
    }
}
