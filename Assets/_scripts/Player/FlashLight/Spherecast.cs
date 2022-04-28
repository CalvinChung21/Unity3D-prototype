using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

    public class Spherecast : MonoBehaviour
    {
        [SerializeField] private GameObject camera;
    
    // for sphere cast
    public float sphereRadius;
    public float maxDistance;
    public LayerMask layerMask;

    public static GameObject currentHitObject;

    public static GameObject CurrentHitObject
    {
        get => currentHitObject;
        set => currentHitObject = value;
    }
    private float currentHitDistance;
    private Vector3 origin;
    private Vector3 direction;

    private float multiplier = 0.5f;
    public static float t = 0;

    // Update is called once per frame
    void Update()
    {
        SphereCast();
    }

    void SphereCast()
    {
        // only do spherecast when the flashlight is on
        if (Flashlight.FlashlightActive)
        {
            // get the camera's position and direction
            origin = camera.transform.position;
            direction = camera.transform.forward;

            // using spherecast to raycast and get the info about the object being hit
            RaycastHit hit;
            if (Physics.SphereCast(origin, sphereRadius, direction, out hit, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal))
            {
                currentHitObject = hit.transform.gameObject;
                currentHitDistance = hit.distance;
                
                // if the object being hit is enemy,
                DealWithNPC();
                
            }
            else
            {
                // reset when nothing is hit by sphere ray cast
                currentHitDistance = maxDistance;
                currentHitObject = null;
            }
        }
        else
        {
            // reset when flashlight is turn off
            currentHitDistance = maxDistance;
            currentHitObject = null;
        }
    }

    private void DealWithNPC()
    {
        // dealing with ghost
        if (currentHitObject.tag == "NPC")
        {
            // white light
            if (Flashlight.FlashlightActive &&
                !currentHitObject.GetComponent<Ghost>().stopPathFinding)
            {
                SoundManager.PlaySound(SoundManager.Sound.who);
                currentHitObject.GetComponent<Ghost>().stopPathFinding = true;
                currentHitObject.GetComponent<Animator>().SetBool("Hit", true);
            }
        }
        // dealing with hello guy
        else if (currentHitObject.tag == "HelloGuy" && currentHitObject.GetComponent<SkinnedMeshRenderer>()!=null)
        {
            // StartCoroutine(FadeOutMaterial(0.1f, currentHitObject));
            if (t < 150)
            {
                Color colour = currentHitObject.GetComponent<SkinnedMeshRenderer>().material.GetColor("_EmissionColor");
                colour *= 1.1f;
                t += 1f;
                currentHitObject.GetComponent<SkinnedMeshRenderer>().material.SetColor("_EmissionColor", colour);
            }
            else
            {
                SoundManager.PlaySound(SoundManager.Sound.happy);
                HealthBarFade.Heal();
                Destroy(currentHitObject);
            }
            
        }
        else
        {
            t = 1;
        }

    }
    //code reference from https://stackoverflow.com/questions/54042904/how-to-fade-out-disapear-a-gameobject-slowly
    // IEnumerator FadeOutMaterial(float fadeSpeed, GameObject currentGameObject)
    // {
    //     Renderer rend = currentGameObject.GetComponent<SkinnedMeshRenderer>();
    //     Color matColor = rend.material.color;
    //     float alphaValue = rend.material.color.a;
    //     
    //     if (alphaValue > 14f)
    //     {
    //         // make hello guy disappear
    //         Destroy(currentGameObject);
    //         SoundManager.PlaySound(SoundManager.Sound.increaseBattery);
    //         Flashlight.changeBatteries(1f);
    //         
    //     } else if (alphaValue > 10f)
    //     {
    //         // play audio and play particle system
    //         SoundManager.PlaySound(SoundManager.Sound.recover, gameObject.transform.position);
    //         currentGameObject.transform.GetChild(1).gameObject.SetActive(true);
    //         ScreenShake.Execute();
    //     }
    //     
    //     // make the hello guy slowly glowing up
    //     while (rend&&rend.material.color.a < 15f)
    //     {
    //         alphaValue += Time.deltaTime / fadeSpeed;
    //         rend.material.color = new Color(matColor.r, matColor.g, matColor.b, alphaValue);
    //     
    //         yield return null;
    //     }
    // }

    
    // for seeing how the sphere cast is working in the scene
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + direction * currentHitDistance);
        Gizmos.DrawWireSphere(origin + direction * currentHitDistance, sphereRadius);
    }
}

