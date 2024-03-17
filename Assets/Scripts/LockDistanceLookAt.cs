using UnityEngine;
using Cinemachine;

public class LockDistanceLookAt : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Transform lookAtObject;
    public float desiredDistance = 5f;

    private CinemachineTransposer transposer;

    void Start()
    {
        if (virtualCamera != null && lookAtObject != null)
        {
            transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        }
    }

    void LateUpdate()
    {
       
    }
}