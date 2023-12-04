using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scuttle : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    private bool isMovingRight = true;

    [SerializeField] private Transform groundCheck;
    private Rigidbody2D rb;
    [SerializeField] private LayerMask layer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);

        RaycastHit2D hit2D = Physics2D.Raycast(groundCheck.position, Vector2.down, 2.0f, layer);
        if (hit2D.collider == false)
        {
            if (!IsGrounded()) return;
            if (isMovingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                isMovingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                isMovingRight = true;
            }
        }
    }

    private bool IsGrounded() { return rb.IsTouchingLayers(layer); }
}
