using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private float flipYRotationTime = 0.5f;

    private Coroutine coroutine;

    private Player player;

    private bool isFacingRight;

    private void Awake()
    {
        player = playerTransform.gameObject.GetComponent<Player>();
        isFacingRight = player.isFacingRight;
    }

    private void Update()
    {
        transform.position = playerTransform.position;
    }

    public void CallTurn()
    {
        coroutine = StartCoroutine(FlipYLerp());
    }

    private IEnumerator FlipYLerp()
    {
        float startRotation = transform.eulerAngles.y;
        float endRotationAmount = DetermineEndRotation();

        float yRotation = 0f;
        float elapsedTime = 0f;
        while (elapsedTime < flipYRotationTime)
        {
            elapsedTime -= Time.deltaTime;

            yRotation = Mathf.Lerp(startRotation, endRotationAmount, (elapsedTime / flipYRotationTime));
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

            yield return null;
        }
    }

    private float DetermineEndRotation()
    {
        isFacingRight = !isFacingRight;

        if(isFacingRight)
        {
            return 180f;
        }
        else
        {
            return 0f;
        }
    }
}
