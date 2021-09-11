using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spherecast : MonoBehaviour
{
    public GameObject currentHitObject;

    public float sphereRadius;
    public float maxDistance;
    public LayerMask layerMask;

    private Vector3 origin;
    private Vector3 direction;

    private float currentHitDistance;
    private GameObject temp;
    // Update is called once per frame
    void Update()
    {
        origin = transform.position;
        direction = transform.forward;
        temp = currentHitObject; 
        RaycastHit hit;
        if (Physics.SphereCast(origin, sphereRadius, direction, out hit, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal))
        {
            currentHitObject = hit.transform.gameObject;
            currentHitDistance = hit.distance;
            if(currentHitObject.tag == "NPC")
            {
                currentHitObject.GetComponent<Renderer>().material.color = Color.yellow;
                if (currentHitObject.GetComponent<NPCMove>().health > 0)
                {
                    currentHitObject.GetComponent<NPCMove>().health -= 7 / currentHitDistance;
                }
                if(temp != null)
                {
                    if (temp != currentHitObject && temp.CompareTag("NPC"))
                    {
                        temp.GetComponent<Renderer>().material.color = Color.red;
                    }
                }
                
            }
        }
        else
        {
            currentHitDistance = maxDistance;
            currentHitObject = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + direction * currentHitDistance);
        Gizmos.DrawWireSphere(origin + direction * currentHitDistance, sphereRadius);
    }
}
