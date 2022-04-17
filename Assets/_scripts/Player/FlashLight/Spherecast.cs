using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CommandPattern
{
    public class Spherecast : MonoBehaviour
{
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

    // Update is called once per frame
    void Update()
    {
        SphereCast();
    }

    void SphereCast()
    {
        // only do spherecast when the flashlight is on
        if (FlashLightToggle.FlashlightActive)
        {
            // get the current game object's position and direction
            origin = transform.position;
            direction = Quaternion.Euler(0, 92.0f, -5.0f) * transform.forward;

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
            // red light
            if (currentHitObject.GetComponent<Ghost>().health > 0 
                && FlashLightToggle.FlashlightMode)
            {
                currentHitObject.GetComponent<Ghost>().health -= 1;
            }
            // white light
            else if (FlashLightToggle.FlashlightMode == false &&
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
            StartCoroutine(FadeOutMaterial(0.1f, currentHitObject));
        }
    }
    //code reference from https://stackoverflow.com/questions/54042904/how-to-fade-out-disapear-a-gameobject-slowly
    IEnumerator FadeOutMaterial(float fadeSpeed, GameObject currentGameObject)
    {
        Renderer rend = currentGameObject.GetComponent<SkinnedMeshRenderer>();
        Color matColor = rend.material.color;
        float alphaValue = rend.material.color.a;

        if (alphaValue > 14f)
        {
            // make hello guy disappear
            Destroy(currentGameObject);
            SoundManager.PlaySound(SoundManager.Sound.increaseBattery);
            BatteryBar.changeBatteries(1f);
        } else if (alphaValue > 10f)
        {
            // play audio and play particle system
            SoundManager.PlaySound(SoundManager.Sound.recover, gameObject.transform.position);
            currentGameObject.transform.GetChild(1).gameObject.SetActive(true);
            ScreenShake.Execute();
        }
        
        // make the hello guy slowly glowing up
        while (rend&&rend.material.color.a < 15f)
        {
            alphaValue += Time.deltaTime / fadeSpeed;
            rend.material.color = new Color(matColor.r, matColor.g, matColor.b, alphaValue);
        
            yield return null;
        }
    }

    
    // for seeing how the sphere cast is working in the scene
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + direction * currentHitDistance);
        Gizmos.DrawWireSphere(origin + direction * currentHitDistance, sphereRadius);
    }
}
}
