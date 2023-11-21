using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LunarflyArts
{
    public class MovingPlatform : MonoBehaviour
    {
        Rigidbody2D rb;
        [SerializeField] private float speed = 1f;
        #region Vertical Platform
        [SerializeField] private float minHeight = -5f;
        [SerializeField] private float maxHeight = 5f;
        [SerializeField] private float currentHeight = 0f;
        #endregion

        #region Horizontal Platform
        [SerializeField] private float minDistance = -5f;
        [SerializeField] private float maxDistance = 5f;
        private float currentDistance = 0f;
        #endregion
        public bool VerticalPlatform;
        public bool HorizontalPlatform;


        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (VerticalPlatform) HandleVerticalPlatform();
            if (HorizontalPlatform) HandleHorizontalPlatform();
        }

        private void HandleVerticalPlatform()
        {
            rb.MovePosition(transform.position + new Vector3(0f, speed * Time.deltaTime));

            if (currentHeight <= minHeight)
            {
                speed = -speed;
                currentHeight = minHeight;
            }
            else if (currentHeight >= maxHeight)
            {
                speed = -speed;
                currentHeight = maxHeight;
            }

            currentHeight += speed * Time.deltaTime;
        }

        private void HandleHorizontalPlatform()
        {
            rb.MovePosition(transform.position + new Vector3(speed * Time.deltaTime, 0f));

            if (currentDistance <= minDistance)
            {
                speed = -speed;
                currentDistance = minDistance;
            }
            else if (currentDistance >= maxDistance)
            {
                speed = -speed;
                currentDistance = maxDistance;
            }

            currentDistance += speed * Time.deltaTime;
        }
    }
}
