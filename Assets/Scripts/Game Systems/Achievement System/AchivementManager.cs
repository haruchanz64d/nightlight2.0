using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchivementManager : MonoBehaviour
{
    public AchievementDatabase achievementDatabase;
    public AchievementUINotificationController achievementUINotificationController;
    public Achievements achievements;

    public void ShowAchievementNotification()
    {
        Achievement achievement = achievementDatabase.achievement[(int) achievements];
        achievementUINotificationController.ShowAchievementNotification(achievement);
    }
}