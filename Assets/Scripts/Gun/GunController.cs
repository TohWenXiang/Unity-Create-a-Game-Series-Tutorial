using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * To do:
 * Weapon Pickup and Drop
 * Dual wielding weapons
 */
public class GunController : MonoBehaviour
{
    public Transform weaponHold;
    public Gun defaultGun;
    Gun equippedGun;

    // Start is called before the first frame update
    void Start()
    {
        //if there is a default gun,
        if (defaultGun != null)
        {
            //equip it
            EquipGun(defaultGun);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EquipGun(Gun newGun)
    {
        //if there was already a gun equipped
        if (equippedGun != null)
        {
            //destroy it or drop it
            Destroy(equippedGun.gameObject);
        }

        //equip with a new created gun or pick the gun up
        equippedGun = Instantiate(newGun, weaponHold.position, weaponHold.rotation, weaponHold);
    }

    public void Shoot()
    {
        //if there is a gun equipped, 
        if (equippedGun != null)
        {
            //use it to shoot
            equippedGun.Shoot();
        }
    }
}
