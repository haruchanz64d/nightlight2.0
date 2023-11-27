using System.Collections;
using System.Collections.Generic;
using Malee;
using UnityEngine;
[CreateAssetMenu()]
public class AchievementDatabase : ScriptableObject
{
    [Malee.List.Reorderable(sortable = false, paginate = false)]
    public AchievementsArray achievements;

    [System.Serializable]
    public class AchievementsArray : Malee.List.ReorderableArray<Achievement> { }
}
