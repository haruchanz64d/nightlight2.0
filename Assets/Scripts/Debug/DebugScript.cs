using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    #region Debug Variables
    [SerializeField] private bool isBeingPlayedInEditor = Application.isEditor;
    [SerializeField] private bool isDevelopmentBuild;
    #endregion

    #region Development Scenes
    private const string TEST_MOVEMENT_ENVIRONMENT = "DebugTestEnvironment";
    private const string TEST_ENEMY_AI_ENVIRONMENT = "DebugTestEnemyAI";
    private const string TEST_XERO_BOSS_ENVIRONMENT = "DebugTestXeroFightEnvironment";
    private const string TEST_COLLECTION_ENVIRONMENT = "DebugTestLightOrbEnvironment";
    #endregion

    #region Gameplay Scenes
    private const string GAMEPLAY_PR = "Prologue";
    private const string GAMEPLAY_CH1 = "ChapterOne";
    private const string GAMEPLAY_CH2 = "ChapterTwo";
    private const string GAMEPLAY_CH3 = "ChapterThree";
    private const string GAMEPLAY_CH4 = "ChapterFour";
    private const string GAMEPLAY_CH5 = "ChapterFive";
    private const string GAMEPLAY_EP = "Epilogue";
    private const string MAIN_MENU = "MainMenu";
    #endregion

    #region Debugging
    private void HandleDebugEnvironment()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.F1))
        {
            SceneManager.LoadScene(TEST_MOVEMENT_ENVIRONMENT);
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.F2))
        {
            SceneManager.LoadScene(TEST_ENEMY_AI_ENVIRONMENT);
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.F3))
        {
            SceneManager.LoadScene(TEST_COLLECTION_ENVIRONMENT);
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.F4))
        {
            SceneManager.LoadScene(TEST_XERO_BOSS_ENVIRONMENT);
        }
    }
    #endregion

    #region Debug Chapters
    private void HandleDebugChapterScenes()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Alpha0))
        {
            SceneManager.LoadScene(GAMEPLAY_PR);
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Alpha1))
        {
            SceneManager.LoadScene(GAMEPLAY_CH1);
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Alpha2))
        {
            SceneManager.LoadScene(GAMEPLAY_CH2);
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Alpha3))
        {
            SceneManager.LoadScene(GAMEPLAY_CH3);
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Alpha4))
        {
            SceneManager.LoadScene(GAMEPLAY_CH4);
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Alpha5))
        {
            SceneManager.LoadScene(GAMEPLAY_CH5);
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Alpha6))
        {
            SceneManager.LoadScene(GAMEPLAY_EP);
        }
    }
    #endregion

    #region Unity MonoBehaviour Functions
    private void Awake()
    {
        isDevelopmentBuild = Debug.isDebugBuild;
    }

    private void Update()
    {
        if (!isBeingPlayedInEditor && !isDevelopmentBuild) return;

        if (Input.GetKey(KeyCode.F9) && isBeingPlayedInEditor || isDevelopmentBuild)
        {
            HandleDebugEnvironment();
            HandleDebugChapterScenes();
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(MAIN_MENU);
        }
    }
    #endregion
}