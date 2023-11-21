using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float flipYRotationTime = 0.5f;

    private Coroutine turnCoroutine;
    private Player player;
    private bool isFacingRight;

    private void Awake()
    {

    }
}
