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

    // Update is called once per frame
    void Update()
    {
        origin = transform.position;
        direction = transform.forward;
        
        RaycastHit hit;
        if (Physics.SphereCast(origin, sphereRadius, direction, out hit, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal))
        {
            currentHitObject = hit.transform.gameObject;
            currentHitDistance = hit.distance;
            if(currentHitObject.tag == "NPC")
            {
                currentHitObject.GetComponent<NavMeshAgent>().isStopped = true;
                currentHitObject.GetComponent<Renderer>().material.color = Color.blue;
                currentHitObject.GetComponent<AudioSource>().Play();
                StartCoroutine(ExecuteAfterTime(4, currentHitObject));
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

    IEnumerator ExecuteAfterTime(float time, GameObject gameObject)
    {
        yield return new WaitForSeconds(time);

        gameObject.GetComponent<NavMeshAgent>().isStopped = false;
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }
}
