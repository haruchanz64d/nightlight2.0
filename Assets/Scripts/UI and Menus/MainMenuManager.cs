using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Canvas mainMenuCanvas;
    [SerializeField] private Canvas achievementCanvas;

    private void Awake()
    {
        ShowMenu();
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Debug Room");
    }

    public void ShowMenu()
    {
        mainMenuCanvas.enabled = true;
        achievementCanvas.enabled = false;
    }

    public void ShowAchievement()
    {
        mainMenuCanvas.enabled = false;
        achievementCanvas.enabled = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
