using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InfParticle : MonoBehaviour
{
    public float speed = 1;
    public float range = 50;
 
    private GameObject target;
    public String targetTag = "Enemy";
     
    private Rigidbody rb ;
 
    private void Start()
    {
        target = FindTarget();
        rb = GetComponent<Rigidbody>();
        
    }
     
    private void FixedUpdate()
    {
        target = FindTarget();
        if( target != null && InRange())
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private bool InRange()
    {
        float dist = Vector3.Distance(target.transform.position, transform.position);
        return dist <= range;
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
}
