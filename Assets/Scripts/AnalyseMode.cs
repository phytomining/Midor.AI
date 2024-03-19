using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;

public class AnalyseMode : MonoBehaviour
{
    public float darkenIntensity = 0.5f; 
    public float slowTimeScale = 0.5f; 

    public PostProcessProfile profile;

    public GameObject processingHandler;

    private bool modeActive = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !modeActive)
        {
            //SetPostProcessingIntensity(darkenIntensity);
            Time.timeScale = slowTimeScale;
            modeActive = true;
            GetComponent<Volume>().enabled = true;
            Debug.Log("mode is active!");
        }
        if (!(Input.GetKey(KeyCode.LeftControl)) && modeActive){
            //SetPostProcessingIntensity(0f);
            Time.timeScale = 1f;
            modeActive = false;
            GetComponent<Volume>().enabled = false;
            Debug.Log("mode is deactivated");
        }
    }

    // Helper method to adjust post-processing intensity
    void SetPostProcessingIntensity(float intensity)
    {
        
    }
}