using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[RequireComponent(typeof(Animator))]
public class AchievementUINotificationController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI achievementTitle;

    private Animator m_animator;
    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }
    public void ShowAchievementNotification(Achievement achievement)
    {
        achievementTitle.SetText(achievement.title);
        m_animator.SetTrigger("Show Notification");
    }
}