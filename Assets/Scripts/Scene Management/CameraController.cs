using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : Singleton<CameraController>
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    private void Start()
    {
        this.SetPlayerCameraFollow();
    }

    public void SetPlayerCameraFollow()
    {
        this.cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        this.cinemachineVirtualCamera.Follow = PlayerController.Instance.transform;
    }
}
