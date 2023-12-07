using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementItemController : MonoBehaviour
{
    [SerializeField] private Image lockedAchievement;
    [SerializeField] private Image unlockedAchievement;
    [SerializeField] private TextMeshProUGUI achievementTitle;
    [SerializeField] private TextMeshProUGUI achievementDescription;
    public Achievement achievements;

    private void OnValidate()
    {
        RefreshView();
    }
    public void RefreshView()
    {
        achievementTitle.SetText(achievements.achievementTitle);
        achievementDescription.SetText(achievements.achievementDescription);
        unlockedAchievement.enabled = false;
        lockedAchievement.enabled = true;

        if (achievements.isHidden)
        {
            achievementTitle.SetText("");
            achievementDescription.SetText("There's something more to this than meets the eye...");
            unlockedAchievement.enabled = false;
            lockedAchievement.enabled = true;
        }
    }
}
