using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkOrb : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    [SerializeField] private GameObject scuttlePrefab;
    private bool enableScuttleSpawning;
    public bool EnableScuttleSpawning { get { return enableScuttleSpawning; } set { enableScuttleSpawning =  value; } }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((1 << other.gameObject.layer) == layer)
        {
            Vector2 droppedPosition = transform.position;
            if (enableScuttleSpawning)
            {
                if (enableScuttleSpawning && Random.Range(0f, 1f) <= 0.2f)
                {
                    Instantiate(scuttlePrefab, droppedPosition, Quaternion.identity);
                }
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}