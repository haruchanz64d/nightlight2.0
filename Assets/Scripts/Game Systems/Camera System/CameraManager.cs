using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    [SerializeField] private CinemachineVirtualCamera[] virtualCameras;
    [Header("Properties")]
    [SerializeField] private float fallPanAmount = 0.25f;
    [SerializeField] private float fallYPanTime = 0.35f;
    public float fallSpeedYDampingThreshhold = -15f;

    public bool IsLerpingYDamping { get; private set; }
    public bool LerpedFromFalling { get; set; }

    private Coroutine coroutine;
    private CinemachineVirtualCamera currentCamera;
    private CinemachineFramingTransposer framingTransposer;

    private float normalYPanAmount;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        for (int i = 0; i < virtualCameras.Length; i++)
        {
            if (virtualCameras[i].enabled)
            {
                currentCamera = virtualCameras[i];
                framingTransposer = currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            }
        }
        normalYPanAmount = framingTransposer.m_YDamping;
    }

    #region Lerp Y Damping
    public void LerpYDamping(bool isPlayerFalling)
    {
        coroutine = StartCoroutine(LerpYAction(isPlayerFalling));
    }

    private IEnumerator LerpYAction(bool isPlayerFalling)
    {
        IsLerpingYDamping = true;

        float startDampAmount = framingTransposer.m_YDamping;
        float endDampAmount = 0f;

        if (isPlayerFalling)
        {
            endDampAmount = fallPanAmount;
            LerpedFromFalling = true;
        }
        else
        {
            endDampAmount = normalYPanAmount;
        }
        float elapsedTime = 0f;
        while (elapsedTime < fallYPanTime)
        {
            elapsedTime -= Time.deltaTime;

            float lerpedPanAmount = Mathf.Lerp(startDampAmount, endDampAmount, (elapsedTime / fallYPanTime));
            framingTransposer.m_YDamping = lerpedPanAmount;

            yield return null;
        }

        IsLerpingYDamping = false;
    }
    #endregion
}
