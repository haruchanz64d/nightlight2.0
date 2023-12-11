using System;
using System.IO;
using UnityEngine;
public class ClearPlayerData : MonoBehaviour
{
    public static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public static void ClearPlayerDataJson()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "playerStats.json");
        try
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error deleting file: " + e.Message);
        }
    }

    public static void ClearAllData()
    {
        ClearPlayerPrefs();
        ClearPlayerDataJson();

        Debug.Log("Player Prefs & PlayerStats.json had been removed!");
    }
}