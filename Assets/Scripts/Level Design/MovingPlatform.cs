using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LunarflyArts
{
    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField] private GameObject[] waypoints;
        private int currentWaypointIndex = 0;
        [SerializeField] private float platformSpeed = 2.5f;

        private void Update()
        {
            if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < 0.1f)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex = 0;
                }
            }

            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, platformSpeed * Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.transform.SetParent(transform);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.transform.SetParent(null);
            }
        }
    }
}
