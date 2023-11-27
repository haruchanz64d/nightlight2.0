using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public AchievementDatabase achievementDatabase;
    public AchievementNotificationController achievementNotificationController;

    public void ShowNotification(Achievements achievements)
    {
        Achievement achievement = achievementDatabase.achievements[(int)achievements];
        achievementNotificationController.ShowNotification(achievement);
    }
}