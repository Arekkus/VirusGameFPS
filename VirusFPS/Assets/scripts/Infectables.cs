using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Infectables : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("InfParticle"))
        {
            gameObject.tag = "Friend";
        }
        if (other.collider.CompareTag("EnemyInf"))
        {
            gameObject.tag = "Enemy";
        }
        Destroy(other.gameObject);
    }
}
