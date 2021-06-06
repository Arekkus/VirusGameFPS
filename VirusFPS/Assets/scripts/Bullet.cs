using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{


    public float speed = 30f;
    public float lifeDuration = 4f;
    public float damage = 10f;

    private float lifeTimer;
    private Vector3 prevPos; 
    
    // Start is called before the first frame update
    void Start()
    {
        prevPos = transform.position; 
        lifeTimer = lifeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        prevPos = transform.position;

        RaycastHit[] hits = Physics.RaycastAll(new Ray(prevPos, (transform.position-prevPos).normalized), (transform.position-prevPos).magnitude);

        if (hits.Length > 0)
        {
            Destroy(gameObject);
        }
        
        transform.position += transform.forward * (speed * Time.deltaTime);

        lifeTimer -= Time.deltaTime;
        
        if (lifeTimer <=0 )
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}
