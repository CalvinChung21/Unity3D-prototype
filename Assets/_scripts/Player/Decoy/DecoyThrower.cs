using System.Collections;
using System.Collections.Generic;
using CommandPattern;
using UnityEngine;

public class DecoyThrower : MonoBehaviour
{
    // reference code from https://www.youtube.com/watch?v=BYL6JtUdEY0
    public float throwForce = 40f;
    public GameObject decoyPrefab;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && WeaponSwitching.selectedWeapon == 1 && LevelState.decoyNum > 0)
        {
            ThrowDecoy();
        }
    }

    void ThrowDecoy()
    {
        LevelState.decoyNum--;
        SoundManager.PlaySound(SoundManager.Sound.throwing, gameObject.transform.position);
        GameObject decoy = Instantiate(decoyPrefab, transform.position, transform.rotation);
        Rigidbody rb = decoy.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce);
    }

}
