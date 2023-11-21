using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu()]
public class AchievementDatabase : ScriptableObject
{
    public List<Achievement> achievement;

    [System.Serializable]
    public class AchievementArray : List<Achievement> { }
}