using UnityEngine;

public class Player : MonoBehaviour
{
    public InputManager inputManager;

    private void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
    }
}
