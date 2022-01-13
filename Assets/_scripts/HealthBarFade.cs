using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void Awake()
    {
        barImage = transform.Find("bar").GetComponent<Image>();
        damagedBarImage = transform.Find("damagedBar").GetComponent<Image>();
        damagedColor = damagedBarImage.color;
        damagedColor.a = 0f;
        damagedBarImage.color = damagedColor;
    }

    private void Start()
    {
        healthSystem = new HealthSystem(100);
        SetHealth(healthSystem.GetHealthNormalized());
    }

    private void Update()
    {
        if(damagedColor.a > 0)
        {
            damagedHealthFadeTimer -= Time.deltaTime;
            if(damagedHealthFadeTimer < 0)
            {
                float fadeAmount = 5f;
                damagedColor.a -= fadeAmount * Time.deltaTime;
                damagedBarImage.color = damagedColor;
            }
        }

        if(healthSystem.GetHealthNormalized() == 0)
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
        }
    }

    private static void SetHealth(float healthNormalized)
    {
        barImage.fillAmount = healthNormalized;
    }

    public static void Damage()
    {
        healthSystem.Damage(10);
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
