using UnityEngine;
using UnityEngine.SceneManagement;
public class StoryTrigger : MonoBehaviour
{
    [SerializeField]
    private string chapterName;
    private Player player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            string currentSceneName = SceneManager.GetActiveScene().name;

            if (currentSceneName == chapterName)
            {
                switch (chapterName)
                {
                    case "Prologue":
                        other.GetComponent<Player>().IsPrologueCompleted = true;
                        break;
                    case "ChapterOne":
                        other.GetComponent<Player>().IsChapterOneCompleted = true;
                        break;
                    case "ChapterTwo":
                        other.GetComponent<Player>().IsChapterTwoCompleted = true;
                        break;
                    case "ChapterThree":
                        other.GetComponent<Player>().IsChapterThreeCompleted = true;
                        break;
                    case "ChapterFour":
                        other.GetComponent<Player>().IsChapterFourCompleted = true;
                        break;
                    case "ChapterFive":
                        other.GetComponent<Player>().IsChapterFiveCompleted = true;
                        break;
                    case "Chapter6":
                        other.GetComponent<Player>().IsEpilogueCompleted = true;
                        break;
                }

                if (IsAllChaptersCompleted())
                {
                    other.GetComponent<Player>().IsGameCompleted = true;
                }
            }
        }
    }

    private bool IsAllChaptersCompleted()
    {
        // Check if all boolean flags are true
        return player.IsPrologueCompleted &&
               player.IsChapterOneCompleted &&
               player.IsChapterTwoCompleted &&
               player.IsChapterThreeCompleted &&
               player.IsChapterFourCompleted &&
               player.IsChapterFiveCompleted &&
               player.IsEpilogueCompleted;
    }
}
