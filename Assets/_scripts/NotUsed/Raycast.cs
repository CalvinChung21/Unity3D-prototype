using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public GameObject lastHit;
    public Vector3 collsion = Vector3.zero;
    public LayerMask layer;

    // Update is called once per frame
    void Update()
    {
        var ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100))
        {
            lastHit = hit.transform.gameObject;
            collsion = hit.point;
            Destroy(lastHit,5);
        }
    }
}
