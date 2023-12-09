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
        OnResume();
    }

    public void OnPause()
    {
        Time.timeScale = 0f;
        IsGamePaused = true;
        pauseCanvas.enabled = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnResume()
    {
        Time.timeScale = 1f;
        IsGamePaused = false;
        pauseCanvas.enabled = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void OnRetry()
    {
        Time.timeScale = 1f;
        StartCoroutine(ReloadSceneAsync());
    }

    IEnumerator ReloadSceneAsync()
    {
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        yield return null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
    #endregion
}
