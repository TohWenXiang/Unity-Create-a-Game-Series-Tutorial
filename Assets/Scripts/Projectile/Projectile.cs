using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * To Do:
 * Create a bullet manager for object pooling
 */
public class Projectile : MonoBehaviour
{
    public LayerMask collisionMask;

    float damage = 1;
    float bulletSpeed;

    float lifeTime = 3;
    float skinWidth = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(DestroyInSeconds(lifeTime));

        Collider[] initialCollision = Physics.OverlapSphere(transform.position, 0.1f, collisionMask);
        if (initialCollision.Length > 0)
        {
            OnHitObject(initialCollision[0]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //find distance in this frame
        float moveDist = bulletSpeed * Time.deltaTime;
        CheckCollision(moveDist);

        //moves bullet forward
        transform.Translate(Vector3.forward * moveDist);
    }

    public void SetBulletSpeed(float speed)
    {
        bulletSpeed = speed;
    }

    public void CheckCollision(float dist)
    {
        //check for collision using raycast by creating a ray with length of dist
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, dist + skinWidth, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit);
        }
    }

    void OnHitObject(RaycastHit hit)
    {
        //destroy itself when hit or recycle it
        IDamageable damageableObject = hit.collider.GetComponent<IDamageable>();
        if (damageableObject != null)
        {
            damageableObject.TakeDamage(damage, hit);
        }
        GameObject.Destroy(gameObject);
    }

    void OnHitObject(Collider collider)
    {
        //destroy itself when hit or recycle it
        IDamageable damageableObject = collider.GetComponent<IDamageable>();
        if (damageableObject != null)
        {
            damageableObject.TakeDamage(damage);
        }
        GameObject.Destroy(gameObject);
    }

    IEnumerator DestroyInSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        //call destroy in projectile object pool
        Destroy(gameObject);

        yield return null;
    }
}
