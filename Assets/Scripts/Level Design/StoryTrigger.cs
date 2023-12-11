using UnityEngine;
using UnityEngine.SceneManagement;
public class StoryTrigger : MonoBehaviour
{
    [SerializeField] private string chapterName;
    private Player player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    public void LoadChapterAchievement()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == chapterName)
        {
            switch (chapterName)
            {
                case "Prologue":
                    player.IsPrologueCompleted = true;
                    Debug.Log($"Prologue completed: {player.IsPrologueCompleted}");
                    break;
                case "ChapterOne":
                    player.IsChapterOneCompleted = true;
                    Debug.Log($"Chapter One completed: {player.IsChapterOneCompleted}");
                    break;
                case "ChapterTwo":
                    player.IsChapterTwoCompleted = true;
                    Debug.Log($"Chapter Two completed: {player.IsChapterTwoCompleted}");
                    break;
                case "ChapterThree":
                    player.IsChapterThreeCompleted = true;
                    Debug.Log($"Chapter Three completed: {player.IsChapterThreeCompleted}");
                    break;
                case "ChapterFour":
                    player.IsChapterFourCompleted = true;
                    Debug.Log($"Chapter Four completed: {player.IsChapterFourCompleted}");
                    break;
                case "ChapterFive":
                    player.IsChapterFiveCompleted = true;
                    Debug.Log($"Chapter Five completed: {player.IsChapterFiveCompleted}");
                    break;
                case "Epilogue":
                    player.IsEpilogueCompleted = true;
                    Debug.Log($"Chapter Epilogue completed: {player.IsEpilogueCompleted}");
                    break;
            }

            if (IsAllChaptersCompleted())
            {
                player.IsGameCompleted = true;
            }
        }
        else
        {
            Debug.Log("Invalid chapter name, mismatched to scene name!");
        }
    }

    private bool IsAllChaptersCompleted()
    {
        return player.IsPrologueCompleted &&
               player.IsChapterOneCompleted &&
               player.IsChapterTwoCompleted &&
               player.IsChapterThreeCompleted &&
               player.IsChapterFourCompleted &&
               player.IsChapterFiveCompleted &&
               player.IsEpilogueCompleted;
    }
}
