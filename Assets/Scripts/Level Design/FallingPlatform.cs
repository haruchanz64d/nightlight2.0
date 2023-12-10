using UnityEngine;
using System.Collections;
namespace LunarflyArts
{
    public class FallingPlatform : MonoBehaviour
    {
        [SerializeField] private GameObject fallingPlatformPrefab;
        private Vector2 initPosition;
        private float fallDelay = 2.5f;
        private float destroyDelay = 0.5f;
        Rigidbody2D rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();

            initPosition = transform.position;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                StartCoroutine(Fall());
            }
        }

        private IEnumerator Fall()
        {
            yield return new WaitForSeconds(fallDelay);
            rb.bodyType = RigidbodyType2D.Dynamic;
            yield return new WaitForSeconds(destroyDelay);
            Destroy(gameObject);
            
            fallingPlatformPrefab.GetComponent<BoxCollider2D>().enabled = true;
            fallingPlatformPrefab.GetComponent<FallingPlatform>().enabled = true;

            Instantiate(fallingPlatformPrefab, initPosition, Quaternion.identity);

        }
    }
}