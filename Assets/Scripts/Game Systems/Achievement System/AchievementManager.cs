using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    [SerializeField] private AchievementDatabase achievementDatabase;
    public AchievementNotificationController achievementNotificationController;

    private AchievementManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    public void ShowNotification(Achievements achievements)
    {
        Achievement achievement = achievementDatabase.achievements[(int)achievements];
        achievementNotificationController.ShowNotification(achievement);
    }
}