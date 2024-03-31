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

    public Text healthText;
    public Color yellow = Color.yellow, red = Color.red, green;
    public float duration = 0.5f;

    private Color initialColor;
    private float timer = 0f;
    public int zone = 2;
    IEnumerator ChangeColorOverTime(Color targetColor)
    {
        while (timer < duration)
        {
            float t = timer / duration;
            Color lerpedColor = Color.Lerp(initialColor, targetColor, t);
            healthBarFill.color = lerpedColor;
            timer += Time.deltaTime;
            yield return null; 
        }
        healthBarFill.color = targetColor;
        timer = 0f;
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
        Debug.Log("Width:" + width);
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
        Debug.Log("The real width: " + realWidth);
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

        if(healthPercentage >= 0.6 && zone != 2){
            ChangeColorOverTime(green);
            zone = 2;
        }else if(healthPercentage >= 0.2 && healthPercentage < 0.6 && zone != 1){
            ChangeColorOverTime(yellow);
            zone = 1;
        }else if(zone != 0 && healthPercentage < 0.2){
            ChangeColorOverTime(red);
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
        if(Input.GetKeyDown(KeyCode.T)) midori.GetComponent<Midori>().damage(0.1f);
    }
}
