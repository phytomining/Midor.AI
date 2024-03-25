using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;

public class UI : MonoBehaviour
{
    public float slowTimeScale = 0.5f; 

    public PostProcessProfile profile;

    public GameObject processingHandler;

    public bool modeActive = false;
    public float lerpSpeed = 1f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !modeActive)
        {
            //SetPostProcessingIntensity(darkenIntensity);
            Time.timeScale = slowTimeScale;
            modeActive = true;
            //GetComponent<Volume>().enabled = true;
            Debug.Log("mode is active!");
        }
        if (!(Input.GetKey(KeyCode.LeftControl)) && modeActive){
            //SetPostProcessingIntensity(0f);
            Time.timeScale = 1f;
            modeActive = false;
            //GetComponent<Volume>().enabled = false;
            Debug.Log("mode is deactivated");
        }

        if (modeActive){
            GetComponent<Volume>().weight = Mathf.Lerp(GetComponent<Volume>().weight, 1f, lerpSpeed * Time.deltaTime);
            Debug.Log("Setting to: " + GetComponent<Volume>().weight);
        }else{
            GetComponent<Volume>().weight = Mathf.Lerp(GetComponent<Volume>().weight, 0f, lerpSpeed * Time.deltaTime);
            Debug.Log("Setting to: " + GetComponent<Volume>().weight);
        }
    }

}
