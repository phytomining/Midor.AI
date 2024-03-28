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
    public UI interfaces;


    void Awake(){

    }
    void Start()
    {
        Debug.Log("Meow meow starting");
    }

    
    void Update()
    {
        //Debug.Log("Current midori: " + midori.GetComponent<Midori>());
        if (gameHandler.GetComponent<UI>().modeActive && Input.GetMouseButtonDown(0)){
            GameObject objectHit = PreformRaycast();
            if(objectHit != null && objectHit.name.Contains("Midori")){
                Debug.Log("Found A MIDORI");
                Debug.Log("Attempting to swap midoris");
                midori.transform.SetParent(null);
                midori.GetComponent<Midori>().selected = false;
                midori = objectHit;
                cameraMain.GetComponent<Camera>().GetComponent<ThirdPersonCam>().playerObj = midori.transform;
                virtualCameraMain.GetComponent<CinemachineFreeLook>().Follow = midori.transform;
                this.gameObject.transform.position = new Vector3(midori.transform.position.x+0.08f, midori.transform.position.y+1.5f, midori.transform.position.z-0.02f);
                midori.transform.SetParent(this.gameObject.transform);
                midori.GetComponent<Midori>().selected = true;
                interfaces.UpdateHealth(midori.GetComponent<Midori>().health);
                interfaces.midori = midori;
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
