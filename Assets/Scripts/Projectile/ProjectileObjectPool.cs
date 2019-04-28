using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileObjectPool : MonoBehaviour
{
    public Projectile projectilePrefab;

    List<Projectile> projectilePool;

    private void Awake()
    {
        projectilePool = new List<Projectile>();    
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Projectile CreateProjectile(Vector3 pos, Quaternion rot)
    {
        //search for an unused projectile
        foreach (Projectile projectile in projectilePool)
        {
            //if not active
            if (projectile.gameObject.activeInHierarchy == false)
            {
                return projectile;
            }
        }

        //create a new one if there is no unused object
        Projectile newProjectile = Instantiate(projectilePrefab, pos, rot, transform);
        //add to object pool
        projectilePool.Add(newProjectile);
        return newProjectile;
    }
}
