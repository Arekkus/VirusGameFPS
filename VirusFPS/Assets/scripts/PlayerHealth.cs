using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 300;
    public float maxHealth = 100f;
    
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
    
    public float calculateHealth()
    {
        return health / maxHealth;
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Bullet"))
        {
            Bullet bullet = other.transform.GetComponent<Bullet>();
            health -= bullet.damage;
        }
    }
}
