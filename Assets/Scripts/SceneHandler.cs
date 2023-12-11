using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneHandler : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoadDebugScene(string debugScene)
    {
        SceneManager.LoadScene(debugScene);
    }
}
