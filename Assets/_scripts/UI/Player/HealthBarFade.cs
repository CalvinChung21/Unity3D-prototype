using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class HealthBarFade : MonoBehaviour
{
    // reference code from https://www.youtube.com/watch?v=cR8jP8OGbhM
    private const float DAMAGED_HEALTH_FADE_TIMER_MAX = 1f;
    private static Image barImage;
    private static Image damagedBarImage;
    private static Color damagedColor;
    private static float damagedHealthFadeTimer;
    private static HealthSystem healthSystem;
    [Header("Damage Screen")] 
    public Color damageColor;
    public Image damageImage;
    float colorSmoothing = 6f;
    static bool isTakingDamage = false;

    private void Awake()
    {
        barImage = transform.Find("bar").GetComponent<Image>();
        damagedBarImage = transform.Find("damagedBar").GetComponent<Image>();
        damagedColor = damagedBarImage.color;
        damagedColor.a = 0f;
        damagedBarImage.color = damagedColor;
        
        healthSystem = new HealthSystem(100);
        SetHealth(healthSystem.GetHealthNormalized());
        
    }

    private void Update()
    {
        if(damagedColor.a > 0)
        { // let the health stay not fade for a certain amount of time
            damagedHealthFadeTimer -= Time.deltaTime;
            if(damagedHealthFadeTimer < 0)
            {// fade the color when its transparency value is over 0
                float fadeAmount = 5f;
                damagedColor.a -= fadeAmount * Time.deltaTime;
                damagedBarImage.color = damagedColor;
            }
        }

        // if taking damage then get the red color, and render it to transparent color smoothly over time
        if (isTakingDamage)
            damageImage.color = damageColor;
        else
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, colorSmoothing * Time.deltaTime);

        isTakingDamage = false;
        
        isGameOver();
    }
    
    private void isGameOver()
    {
        // when player's health reaches 0
        if(healthSystem.GetHealthNormalized() == 0)
        {
            //change to Game Over Scene
            SceneManager.LoadScene("GameLose");
        }
    }
    
    // those are set to static so that other script can access this script's function to modify the player's health
    // for example hitting the player or healing the player
    private static void SetHealth(float healthNormalized)
    {
        barImage.fillAmount = healthNormalized;
    }

    public static void Damage()
    {
        healthSystem.Damage(10);
        isTakingDamage = true;
        if (damagedColor.a <= 0)
        {
            damagedBarImage.fillAmount = barImage.fillAmount;
            damagedColor.a = 1;
            damagedBarImage.color = damagedColor;
            damagedHealthFadeTimer = DAMAGED_HEALTH_FADE_TIMER_MAX;
        }
        SetHealth(healthSystem.GetHealthNormalized());
    }

    public static void Heal()
    {
        healthSystem.Heal(10);
        SetHealth(healthSystem.GetHealthNormalized());
    }
}
