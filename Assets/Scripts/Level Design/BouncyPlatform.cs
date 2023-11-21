using UnityEngine;

namespace LunarflyArts
{
    public class BouncyPlatform : MonoBehaviour
    {
        private float bounceForce = 20f;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
            }
        }
    }
}
