using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spherecast : MonoBehaviour
{
    public static GameObject currentHitObject;

    public float sphereRadius;
    public float maxDistance;
    public LayerMask layerMask;

    private Vector3 origin;
    private Vector3 direction;

    private float currentHitDistance;
    // Update is called once per frame
    void Update()
    {
        // only do spherecast when the flashlight is on
        if (FlashLightToggle.flashLightStatus())
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
                // if the object being hit is NPC,
                if (currentHitObject.tag == "NPC")
                {
                    if (currentHitObject.GetComponent<NPCMove>().health > 0 && GetComponentInChildren<Light>().color == Color.red)
                    {
                        currentHitObject.GetComponent<NPCMove>().health-=5;
                    }
                    else if (GetComponentInChildren<Light>().color == Color.white)
                    {
                        currentHitObject.GetComponent<NavMeshAgent>().isStopped = true;
                    }
                }else if(currentHitObject.tag == "HelloGuy")
                {
                    Destroy(currentHitObject);
                    SoundManagerScript.playSound("haha");
                }
            }
            else
            {
                currentHitDistance = maxDistance;
                currentHitObject = null;
            }
        }
        else
        {
            currentHitDistance = maxDistance;
            currentHitObject = null;
        }
    }
    // for seeing how the sphere cast is working in the scene
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + direction * currentHitDistance);
        Gizmos.DrawWireSphere(origin + direction * currentHitDistance, sphereRadius);
    }

    public static GameObject getCurrentHitObject()
    {
        return currentHitObject;
    }
}
