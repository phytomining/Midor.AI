using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    public CinemachineVirtualCameraBase virtualCamera;
    public CinemachineVirtualCameraBase virtualCameraMain;
    public GameObject gameHandler;
    public Color debugRayColor = Color.red;

    public GameObject cameraMain;

    public GameObject midori;

    public Midori currentMidori;
    public UI interfaces;
    public float movementSpeed = 10.0f;
    public GameObject orientation;
    public Player player;
    public GameObject playerObj;
    IEnumerator MoveCamera(GameObject midoriObj)
    {
        Transform originalFollow = virtualCamera.Follow;
        Transform originalLookAt = virtualCamera.LookAt;

        virtualCamera.Follow = null;
        virtualCamera.LookAt = null;

        Vector3 targetPosition = new Vector3(midori.transform.position.x+0.08f, midori.transform.position.y+1.5f, midori.transform.position.z-0.02f);

        //Get beginning stats
        float startTime = Time.time;
        Vector3 startPosition = virtualCamera.transform.position;
        float distance = Vector3.Distance(startPosition, targetPosition);
        float duration = distance / movementSpeed;

        while (Time.time - startTime < duration)
        {
            targetPosition = new Vector3(midori.transform.position.x+0.08f, midori.transform.position.y+1.5f, midori.transform.position.z-0.02f);
            float t = (Time.time - startTime) / duration;
            virtualCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        //Midori gurinuuuu here!!! This Ensures camera reaches the exact target position + make sure camera arrives... ;-;
        Camera.main.transform.position = targetPosition;
        this.gameObject.transform.position = targetPosition;
        virtualCamera.Follow = orientation.transform;
        virtualCamera.LookAt = orientation.transform;
        virtualCameraMain.Follow = midori.transform;
        virtualCameraMain.LookAt = orientation.transform;
        
        midori.transform.SetParent(this.gameObject.transform);
        midori.GetComponent<Midori>().selected = true;
        interfaces.UpdateHealth(midori.GetComponent<Midori>().health);
        interfaces.midori = midori;
        currentMidori = midori.GetComponent<Midori>();
        cameraMain.GetComponent<Camera>().GetComponent<ThirdPersonCam>().playerObj = midori.transform;
    }

    void Awake(){

    }
    void Start()
    {
        Debug.Log("Meow meow starting");
        currentMidori = midori.GetComponent<Midori>();
    }

    
    void Update()
    {
        //Debug.Log("Current midori: " + midori.GetComponent<Midori>());
        if (gameHandler.GetComponent<UI>().modeActive && Input.GetMouseButtonDown(0)){
            GameObject objectHit = PreformRaycast();
            if(objectHit != null && objectHit.name.Contains("Midori")){
                midori.transform.SetParent(null);
                midori.GetComponent<Midori>().selected = false;
                midori = objectHit;
                StartCoroutine(MoveCamera(midori));
                
            }
        }
        
    }

    GameObject PreformRaycast(){
        Camera mainCamera = Camera.main;
        if (mainCamera != null){
            Transform cameraTransform = mainCamera.transform;
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * 100f, debugRayColor);
            if (Physics.Raycast(ray, out hit)){
                Debug.Log("Hit object: " + hit.collider.gameObject.name);
                //aDebug.Log("Hit point 123: " + hit.point);
                return hit.collider.gameObject;
            }
        }
        return null;
    }

    void swapControl(GameObject newMidori){

    }
}
