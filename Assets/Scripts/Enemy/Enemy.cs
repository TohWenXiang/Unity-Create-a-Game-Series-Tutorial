using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 *  Contains artificial intelligence for enemy
 *  targets the player at every fix amount of time
 */
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : Character
{
    public enum State
    {
        Idle,
        Chasing,
        Attacking
    };

    State currentState;
    NavMeshAgent pathFinder;
    Transform target;
    Material skinMat;
    Color originalColor;
    Character targetCharacter;
    float attackDistThreshold = 0.5f;
    float timeBetweenAttack = 1;
    float nextAttackTime;
    float myCollisionRadius;
    float targetCollisionRadius;
    bool hasTarget;
    float damage = 1;

    //for reference to other script and initialisation
    protected override void Awake()
    {
        base.Awake();
        //cache a reference to NavMeshAgent
        pathFinder = GetComponent<NavMeshAgent>();

        //cache a reference to material
        skinMat = GetComponent<Renderer>().material;
        
        //if there is a player
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            //player will be the target
            target = GameObject.FindGameObjectWithTag("Player").transform;
            //reference to the player
            targetCharacter = target.GetComponent<Character>();
            //subscribe to a event that will be broadcasted when target is dead
            targetCharacter.OnDeath += OnTargetDeath;

            //target and its own radius
            myCollisionRadius = GetComponent<CapsuleCollider>().radius;
            targetCollisionRadius = GetComponent<CapsuleCollider>().radius;

            //cache a reference to the material original color
            originalColor = skinMat.color;

            //default enemy state
            currentState = State.Chasing;
            //enemy finally have a target
            hasTarget = true;
        }
    }

    // Start is called before the first frame update, once its script is enabled
    protected override void Start()
    {
        base.Start();

        //if there is a player
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            StartCoroutine(UpdatePath());
        }   
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (hasTarget)
        {
            if (Time.time > nextAttackTime)
            {
                float sqrDistToTarget = (target.position - transform.position).sqrMagnitude;

                if (sqrDistToTarget < Mathf.Pow(attackDistThreshold + myCollisionRadius + targetCollisionRadius, 2))
                {
                    nextAttackTime = Time.time + timeBetweenAttack;
                    StartCoroutine(Attack());
                }
            }
        }
    }

    IEnumerator UpdatePath()
    {
        float refreshRate = .25f;

        //only track when there is a target
        while (hasTarget)
        {
            if (currentState == State.Chasing)
            {
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                Vector3 targetPos = target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius + (attackDistThreshold * 0.5f));
                //if enemy is dead don't move anymore
                if (!isDead)
                {
                    pathFinder.SetDestination(targetPos);
                }
            }
            yield return new WaitForSeconds(refreshRate);
        }
    }

    IEnumerator Attack()
    {
        currentState = State.Attacking;
        pathFinder.enabled = false;
        Vector3 originalPos = transform.position;
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        Vector3 attackPos = target.position - dirToTarget * myCollisionRadius;

        float attackSpeed = 3;
        float percent = 0;

        skinMat.color = Color.magenta;

        bool hasAppliedDamage = false;

        while (percent <= 1)
        {
            if (percent >= 0.5f && !hasAppliedDamage)
            {
                hasAppliedDamage = true;
                targetCharacter.TakeDamage(damage);
            }
            percent += Time.deltaTime * attackSpeed;
            float interpolation = 4 * (-Mathf.Pow(percent, 2) + percent);
            transform.position = Vector3.Lerp(originalPos, attackPos, interpolation);

            yield return null;
        }

        skinMat.color = originalColor;
        currentState = State.Chasing;
        pathFinder.enabled = true;
    }

    void OnTargetDeath()
    {
        hasTarget = false;
        currentState = State.Idle;
    }
}
