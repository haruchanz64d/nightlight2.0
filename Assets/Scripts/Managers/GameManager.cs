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
