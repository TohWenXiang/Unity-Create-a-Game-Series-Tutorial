using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * To Do:
 * Modify miliseconds between shots to rate of fire
 * Create a bullet manager for object pooling
 */
public class Gun : MonoBehaviour
{
    public Transform muzzle;
    public Projectile bullet;
    public float milisecondsBetweenShots = 100;
    public float muzzleVelocity = 35;

    float nextShotTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        //don't shoot until next shooting time
        if (Time.time > nextShotTime)
        {
            //update next shooting time, convert miliseconds to seconds
            nextShotTime = Time.time + (milisecondsBetweenShots * 0.001f);
            Projectile newProjectile = Instantiate(bullet, muzzle.position, muzzle.rotation);
            newProjectile.SetBulletSpeed(muzzleVelocity);
        }
        
    }
}
