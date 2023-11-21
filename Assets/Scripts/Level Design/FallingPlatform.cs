using UnityEngine;
using System.Collections;
namespace LunarflyArts
{
    public class FallingPlatform : MonoBehaviour
    {
        private float fallDelay = 1f;
        private float destroyDelay = 0.5f;

        Rigidbody2D rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
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
            Destroy(gameObject, destroyDelay);
        }
    }
}
