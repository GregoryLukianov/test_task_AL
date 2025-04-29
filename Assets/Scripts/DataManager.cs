using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class DataManager : MonoBehaviour
{
#if UNITY_EDITOR
    private static string filePath => Application.dataPath + "/../LocalData" + "/GameData.json";
#else
    private static string filePath => $"{Application.persistentDataPath}/GameData.json";
#endif

    public static GameData LoadData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<GameData>(json);
        }
        return new GameData();
    }
    
    public static void SaveData(GameData data)
    {
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }
}