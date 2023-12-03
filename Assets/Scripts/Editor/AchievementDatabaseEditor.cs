using UnityEngine;
using System.IO;
using UnityEditor;

[CustomEditor(typeof(AchievementDatabase))]
public class AchievementDatabaseEditor : Editor
{
    private AchievementDatabase achievementDatabase;

    private void OnEnable()
    {
        achievementDatabase = target as AchievementDatabase;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Generate Enum", GUILayout.Height(30)))
        {
            GenerateEnum();
        }
    }

    private void GenerateEnum()
    {
        string filePath = Path.Combine(Application.dataPath + "/Data/", "Achievements.cs");
        string code = "public enum Achievements {";
        foreach(Achievement achievement in achievementDatabase.achievements)
        {
            code += achievement.achievementID + "," + "\n";
        }
        code += "}";
        File.WriteAllText(filePath, code);
        AssetDatabase.ImportAsset("Assets/Data/Achievements.cs");
    }
}
