using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    
    public GameObject target;

    public String targetTag = "Friend";

    public LayerMask whatIsGround, whatIsFriend, whatIsEnemy;
    private LayerMask whatIsTarget;
    public float revertToPassiveTimer = 100f;

    public bool isPassive = true;
    private bool alreadyInfected = false;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        target = FindTarget();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        SetTarget();
        if (!isPassive)
        {
            target = FindTarget();


            if (target != null)
            {
                float distToTarget = Vector3.Distance(target.transform.position, transform.position);
                         
                         //Check for sight and attack range
                if (distToTarget <= sightRange)
                {
                    playerInSightRange = true;
                } else {
                    playerInSightRange = false;
                }
                if (distToTarget <= attackRange) 
                { 
                    playerInAttackRange = true; 
                } else { 
                    playerInAttackRange = false;
                }
                
                if (playerInSightRange && !playerInAttackRange) ChasePlayer();
                if (playerInAttackRange && playerInSightRange) AttackPlayer();
            }

            if (!alreadyInfected)
            {
                alreadyInfected = true;
                Invoke(nameof(revertToPassive), revertToPassiveTimer);
            }
        }
        if (!playerInSightRange && !playerInAttackRange) Patroling();
    }

    void revertToPassive()
    {
        gameObject.tag = "Passive";
        isPassive = true;
        alreadyInfected = false;
    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        Debug.DrawRay(walkPoint, -Vector3.up*10);

        if (Physics.Raycast(walkPoint, -transform.up, 8f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(target.transform.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(target.transform);

        if (!alreadyAttacked)
        {
            ///Attack code here
            GameObject bulletObject = Instantiate(projectile);
            bulletObject.transform.position = transform.position + transform.forward * 3;
            bulletObject.transform.forward = transform.forward;
            ///End of attack code
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    
    public GameObject FindTarget()
    {
        List<GameObject> gos = new List<GameObject>();
        foreach (var go in GameObject.FindGameObjectsWithTag(targetTag))
        {
            gos.Add(go);
        }
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos) {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance) {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    private void SetTarget()
    {
        if (gameObject.tag.Equals("Enemy"))
        {
            GetComponent<Renderer>().material.color = Color.red;
            targetTag = "Friend";
            isPassive = false;
        }
        if (gameObject.tag.Equals("Friend"))
        {
            GetComponent<Renderer>().material.color = Color.blue;
            targetTag = "Enemy";
            isPassive = false;
        }
        if (isPassive)
        {
            gameObject.tag = "Passive";
            GetComponent<Renderer>().material.color = Color.grey;
        }
    }
}
