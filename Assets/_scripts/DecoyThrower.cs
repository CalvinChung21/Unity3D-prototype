using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoyThrower : MonoBehaviour
{
    // reference code from https://www.youtube.com/watch?v=BYL6JtUdEY0
    public float throwForce = 40f;
    public GameObject decoyPrefab;
    public static int decoyNum = 4;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && WeaponSwitching.selectedWeapon == 1 && decoyNum > 0)
        {
            ThrowDecoy();
        }
    }

    void ThrowDecoy()
    {
        SoundManagerScript.playSound("throw");
        decoyNum--;
        GameObject decoy = Instantiate(decoyPrefab, transform.position, transform.rotation);
        Rigidbody rb = decoy.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce);
    }

}
