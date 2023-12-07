using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementMenuManager : MonoBehaviour
{
    [SerializeField] private AchievementDatabase database;

    [SerializeField] private GameObject achievementItem;
    [SerializeField] private Transform content;

    [SerializeField] private List<AchievementItemController> achievementItems;


    private void Awake()
    {
        LoadAchievementTables();
    }

    private void LoadAchievementTables()
    {
        foreach (AchievementItemController controller in achievementItems)
        {
            DestroyImmediate(controller.gameObject);
        }
        achievementItems.Clear();
        foreach (Achievement achievement in database.achievements)
        {
            GameObject newAchievementItem = Instantiate(achievementItem, content);
            AchievementItemController controller = newAchievementItem.GetComponent<AchievementItemController>();
            controller.achievements = achievement;
            controller.RefreshView();
            achievementItems.Add(controller);
        }
    }
}
