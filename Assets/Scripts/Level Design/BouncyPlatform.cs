using UnityEngine;
using System.Collections;
namespace LunarflyArts
{
    public class BouncyPlatform : MonoBehaviour
    {
        private float bounceForce = 20f;
        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
                StartCoroutine(PlayAnimation());
            }
        }

        private IEnumerator PlayAnimation()
        {
            animator.Play("Bounce");
            yield return new WaitForSeconds(0.5f);
            animator.Play("Idle");
        }
    }
}