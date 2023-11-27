using UnityEngine;

namespace LunarflyArts
{
    public class ResetDash : MonoBehaviour
    {
        private Movement player;
        private void OnTriggerEnter2D(Collider2D collision)
        {  
            if(collision.CompareTag("Player"))
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
                player.ResetDash();
                Destroy(gameObject);
            }
        }
    }
}