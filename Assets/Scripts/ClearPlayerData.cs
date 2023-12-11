using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ClearPlayerData : MonoBehaviour
{
    public static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("All PlayerPref keys removed!");
    }

    public static void ClearPlayerDataJson()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "playerStats.json");
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("Player Data had been removed!");
        }
    }

    public static void ClearAllData()
    {
        ClearPlayerPrefs();
        ClearPlayerDataJson();
    }
}
