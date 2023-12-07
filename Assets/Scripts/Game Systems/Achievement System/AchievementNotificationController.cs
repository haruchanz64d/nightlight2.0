using TMPro;
using UnityEngine;

public class AchievementNotificationController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI achievementTitleLabel;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource achievementAudio;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        achievementAudio = GetComponent<AudioSource>();
    }
    public void ShowNotification(Achievement achievement)
    {
        achievementTitleLabel.SetText(achievement.achievementTitle);
        animator.SetTrigger("isAchievementObtained");
        achievementAudio.Play();
    }
}
