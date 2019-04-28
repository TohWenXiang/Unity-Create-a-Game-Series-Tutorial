using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class WorldBound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("sds");

        //if collided and is a projectile
        if (other.gameObject.GetComponent<Projectile>() != null)
        {
            //set inactive
            other.gameObject.SetActive(false);
        }
    }
}
