using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Canvas mainMenuCanvas;
    [SerializeField] private Canvas achievementCanvas;
    [SerializeField] private Canvas quitConfirmationDialogue;
    [SerializeField] private Canvas deleteConfirmationDialogue;
    [SerializeField] private Canvas playerDataDeletedConfirnmation;

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
        quitConfirmationDialogue.enabled = false;
        deleteConfirmationDialogue.enabled = false;
        playerDataDeletedConfirnmation.enabled = false;
    }

    public void ShowAchievement()
    {
        achievementCanvas.enabled = true;
        mainMenuCanvas.enabled = false;
        quitConfirmationDialogue.enabled = false;
        deleteConfirmationDialogue.enabled = false;
        playerDataDeletedConfirnmation.enabled = false;
    }

    public void ShowConfirmQuitGame()
    {
        achievementCanvas.enabled = false;
        mainMenuCanvas.enabled = true;
        quitConfirmationDialogue.enabled = true;
        deleteConfirmationDialogue.enabled = false;
        playerDataDeletedConfirnmation.enabled = false;
    }

    public void ConfirmQuitGame()
    {
        Application.Quit();
    }

    #region Achievement

    public void AskToDeletePlayerData()
    {
        achievementCanvas.enabled = true;
        mainMenuCanvas.enabled = false;
        quitConfirmationDialogue.enabled = false;
        deleteConfirmationDialogue.enabled = true;
        playerDataDeletedConfirnmation.enabled = false;
    }

    public void ShowThatPlayerDataHadBeenDeletedDialogueBox()
    {
        achievementCanvas.enabled = true;
        mainMenuCanvas.enabled = false;
        quitConfirmationDialogue.enabled = false;
        deleteConfirmationDialogue.enabled = false;
        playerDataDeletedConfirnmation.enabled = true;
    }
    public void ConfirmToResetAfterDeletingPlayerData()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion
}
