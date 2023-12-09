using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CheckpointSystem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().IsInteractedWithCheckpoint = true;
            collision.gameObject.GetComponent<Player>().GetLastCheckpointPosition = transform.position;
        }
    }
}