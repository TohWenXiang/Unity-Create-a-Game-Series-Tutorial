using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    public float startingHealth;
    public event System.Action OnDeath;

    protected float health;
    protected bool isDead;


    protected virtual void Awake()
    {
        isDead = false;
        health = startingHealth;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public void TakeDamage(float damage, RaycastHit hit)
    {
        //do some stuff with hit variable
        //particle effects at point of damage
        TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log(gameObject.name + "'s remaining health: " + health);
        if (health < 1 && !isDead)
        {
            Die();
        }
    }

    protected void Die()
    {
        Debug.Log(gameObject.name + " is dead!!!");
        isDead = true;
        if (OnDeath != null)
        {
            OnDeath();
        }
        GameObject.Destroy(gameObject);
    }

}
