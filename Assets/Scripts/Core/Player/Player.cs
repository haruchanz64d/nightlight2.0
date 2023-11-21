using UnityEngine;

namespace LunarflyArts
{
    public class Player : MonoBehaviour
    {
        public InputManager inputManager;

        private void Start()
        {
            inputManager = FindObjectOfType<InputManager>();
        }
    }
}
