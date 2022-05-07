using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitching : MonoBehaviour
{
    // reference code from https://www.youtube.com/watch?v=Dn_BUIVdAPg
    public static int selectedWeapon = 0;
    public Sprite flashlight;
    public Sprite flare;

    public Image weaponIcon;
    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedWeapon == 0)
        {
            weaponIcon.sprite = flashlight;
        }else if (selectedWeapon == 1)
        {
            weaponIcon.sprite = flare;
        }
        
        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f && !Flashlight.flicker)
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f && !Flashlight.flicker)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)&& !Flashlight.flicker)
        {
            selectedWeapon = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2 && !Flashlight.flicker)
        {
            selectedWeapon = 1;
        }

        if (previousSelectedWeapon != selectedWeapon && !Flashlight.flicker)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
