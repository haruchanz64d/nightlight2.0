using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    #region Global Boolean
    private bool isGamePaused;

    public bool IsGamePaused { get { return isGamePaused; } set { isGamePaused = value; } }
    #endregion
    #region Canvas
    [SerializeField] private Canvas pauseCanvas;
    #endregion
    #region Chapter Progress
    private bool isPrologueCompleted;
    private bool isChapterOneCompleted;
    private bool isChapterTwoCompleted;
    private bool isChapterThreeCompleted;
    private bool isChapterFourCompleted;
    private bool isChapterFiveCompleted;
    private bool isEpilogueCompleted;
    private bool isGameCompleted;
    #endregion

    #region Chapter Getter And Setter
    public bool IsPrologueCompleted
    {
        get { return isPrologueCompleted; }
        set { isPrologueCompleted = value; }
    }

    public bool IsChapterOneCompleted
    {
        get { return isChapterOneCompleted; }
        set { isChapterOneCompleted = value; }
    }

    public bool IsChapterTwoCompleted
    {
        get { return isChapterTwoCompleted; }
        set { isChapterTwoCompleted = value; }
    }

    public bool IsChapterThreeCompleted
    {
        get { return isChapterThreeCompleted; }
        set { isChapterThreeCompleted = value; }
    }

    public bool IsChapterFourCompleted
    {
        get { return isChapterFourCompleted; }
        set { isChapterFourCompleted = value; }
    }

    public bool IsChapterFiveCompleted
    {
        get { return isChapterFiveCompleted; }
        set { isChapterFiveCompleted = value; }
    }

    public bool IsEpilogueCompleted
    {
        get { return isEpilogueCompleted; }
        set { isEpilogueCompleted = value; }
    }

    public bool IsGameCompleted
    {
        get { return isGameCompleted; }
        set
        {
            isGameCompleted = value;
        }
    }
    #endregion

    #region Unity MonoBehaviour functions
    private void Start()
    {
        pauseCanvas.enabled = false;
    }

    public void OnPause()
    {
        Time.timeScale = 0f;
        IsGamePaused = true;
        pauseCanvas.enabled = true;
    }

    public void OnResume()
    {
        Time.timeScale = 1f;
        IsGamePaused = false;
        pauseCanvas.enabled = false;
    }

    public void OnMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    #endregion
}
