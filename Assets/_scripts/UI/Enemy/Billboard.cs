using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform cam;
    private void Start()
    {
        cam = GameObject.Find("FirstPersonCharacter").GetComponent<Transform>();
    }
    void LateUpdate()
    {
        if(cam != null)
        transform.LookAt(transform.position+cam.forward);
    }
}
