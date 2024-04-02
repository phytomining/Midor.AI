using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using System;
public class UI : MonoBehaviour
{
    public float slowTimeScale = 0.5f; 
    public bool modeActive = false;
    public float lerpSpeed = 15f;
    public GameObject player;
    public GameObject midori;
    public RawImage healthBarFill;
    public RawImage healthBarFillBg;

    public float health = 100f, maxHealth = 100f;

    public Text healthText, cpuText;
    public Color yellow = Color.yellow, red = Color.red, green;
    public float duration = 0.2f;

    private Color initialColor;
    private float timer = 0f, imgtimer = 0f;
    public int zone = 0;
    IEnumerator ChangeColorOverTime(Text text, Color targetColor)
    {
        while (timer < duration)
        {
            float t = timer / duration;
            Color lerpedColor = Color.Lerp(initialColor, targetColor, t);
            text.color = lerpedColor;
            timer += Time.deltaTime;
            Debug.Log("Changing color to: " + lerpedColor);
            yield return null; 
        }
        text.color = targetColor;
        timer = 0f;
    }

    IEnumerator ChangeColorOverTimeImage(RawImage img, Color targetColor)
    {
        while (imgtimer < duration)
        {
            float t = imgtimer / duration;
            Color lerpedColor = Color.Lerp(initialColor, targetColor, t);
            img.color = lerpedColor;
            imgtimer += Time.deltaTime;
            Debug.Log("Changing color to: (IMG)" + lerpedColor);
            yield return null; 
        }
        //img.color = targetColor;
        imgtimer = 0f;
    }

    void changeAlpha(RawImage raw, float alphaValue)
    {
        Color color = raw.color;
        color.a = alphaValue;
        raw.color = color;
    }

    void changeAlphaText(Text raw, float alphaValue)
    {
        Color color = raw.color;
        color.a = alphaValue;
        raw.color = color;
    }

    void CropImage(float hpPercent)
    {
        float width = hpPercent;
        Rect imageUVRect = new Rect(
            0f,
            0f,
            width * 1f,
            1f
        );
        healthBarFill.uvRect = imageUVRect;
    }

    public void ChangeScale(Vector3 newScale)
    {
        RectTransform rectTransform = healthBarFill.rectTransform;
        rectTransform.localScale = newScale;
    }

    void Start()
    {
        float realWidth = healthBarFill.rectTransform.sizeDelta.x;
        midori = player.GetComponent<Player>().midori;
        Debug.Log(midori.name);
        Debug.Log(midori.GetComponent<Midori>());
        health = midori.GetComponent<Midori>().health;
        maxHealth = midori.GetComponent<Midori>().maxHealth;
        realWidth = healthBarFill.rectTransform.sizeDelta.x;
        UpdateHealth(health);
    }

    public void UpdateHealthText(float currentHealth, float maxHealth)
    {
        healthText.text = "Health: " + ((float)Math.Round(currentHealth, 1)).ToString() + " / " + ((float)Math.Round(maxHealth, 1)).ToString();
    }

    public void UpdateHealth(float currentHealth)
    {
        float healthPercentage = currentHealth / maxHealth;
        UpdateHealthText(currentHealth, maxHealth);
        ChangeScale(new Vector3(6f * healthPercentage, 1.05f,1f));
        CropImage(healthPercentage);

        Debug.Log(zone + " " + healthPercentage);

        if(healthPercentage >= 0.6f && zone != 2){
            Debug.Log("Test " + healthPercentage);
            initialColor = healthText.color;
            StartCoroutine(ChangeColorOverTime(healthText, green));
            StartCoroutine(ChangeColorOverTimeImage(healthBarFill, green));
            zone = 2;
        }else if(healthPercentage >= 0.2f && healthPercentage < 0.6f && zone != 1){
            initialColor = healthText.color;
            StartCoroutine(ChangeColorOverTime(healthText, yellow));
            StartCoroutine(ChangeColorOverTimeImage(healthBarFill, yellow));
            zone = 1;
        }else if(zone != 0 && healthPercentage < 0.2){
            initialColor = healthText.color;
            StartCoroutine(ChangeColorOverTime(healthText, red));
            StartCoroutine(ChangeColorOverTimeImage(healthBarFill, red));
            zone = 0;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !modeActive)
        {
            Time.timeScale = slowTimeScale;
            modeActive = true;
        }
        if (!(Input.GetKey(KeyCode.LeftControl)) && modeActive){
            Time.timeScale = 1f;
            modeActive = false;
        }

        if (modeActive){
            GetComponent<Volume>().weight = (float)Math.Round(Mathf.Lerp(GetComponent<Volume>().weight, 1f, lerpSpeed * Time.deltaTime),4);
            changeAlpha(healthBarFill,(float)Math.Round(Mathf.Lerp(healthBarFill.color.a, 1f, lerpSpeed * Time.deltaTime),4));
            changeAlpha(healthBarFillBg,(float)Math.Round(Mathf.Lerp(healthBarFill.color.a, 1f, lerpSpeed * Time.deltaTime),4));
            changeAlphaText(healthText,(float)Math.Round(Mathf.Lerp(healthBarFill.color.a, 1f, lerpSpeed * Time.deltaTime),4));
        }else{
            GetComponent<Volume>().weight = (float)Math.Round(Mathf.Lerp(GetComponent<Volume>().weight, 0f, lerpSpeed * Time.deltaTime),4);
            changeAlpha(healthBarFill,(float)Math.Round(Mathf.Lerp(healthBarFill.color.a, 0f, lerpSpeed * Time.deltaTime),4));
            changeAlpha(healthBarFillBg,(float)Math.Round(Mathf.Lerp(healthBarFill.color.a, 0f, lerpSpeed * Time.deltaTime),4));
            changeAlphaText(healthText,(float)Math.Round(Mathf.Lerp(healthBarFill.color.a, 0f, lerpSpeed * Time.deltaTime),4));
        }   
        if(Input.GetKey(KeyCode.T)) midori.GetComponent<Midori>().damage(0.1f);
    }
}
