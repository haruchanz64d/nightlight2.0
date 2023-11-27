using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace LunarflyArts
{
    public class Collision : Player
    {
        [SerializeField] private Transform groundCheck;
        [SerializeField] private LayerMask groundLayer;
        private PlayerAchievementTracker tracker;
        bool isFirstTimeObtaining;

        private void Awake()
        {
            tracker = GetComponent<PlayerAchievementTracker>();
        }
        public bool IsGrounded()
        {
            return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.CompareTag("Dash Crystal"))
            {
                if(!isFirstTimeObtaining)
                {
                    tracker.AchievementSparklingDiscovery();
                    isFirstTimeObtaining = true;
                }
                Destroy(collision.gameObject);
            }
            if(collision.gameObject.CompareTag("Kill Plane"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
