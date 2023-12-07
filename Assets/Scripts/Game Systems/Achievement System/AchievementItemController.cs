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

    private void Start()
    {
        LoadPlayerPrefs();
    }

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

        if (PlayerPrefs.GetInt(achievements.achievementTitle) == 1)
        {
            unlockedAchievement.enabled = true;
            lockedAchievement.enabled = false;
        }
    }

    private void LoadPlayerPrefs()
    {
        int achievementValue = PlayerPrefs.GetInt(achievements.achievementID);

        unlockedAchievement.enabled = achievementValue == 1;
        lockedAchievement.enabled = !unlockedAchievement.enabled;
    }
}