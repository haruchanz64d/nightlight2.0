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
        SceneManager.LoadScene("Prologue");
    }

    public void ShowMenu()
    {
        achievementCanvas.enabled = false;
        mainMenuCanvas.enabled = true;
    }

    public void ShowAchievement()
    {
        achievementCanvas.enabled = true;
        mainMenuCanvas.enabled = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
