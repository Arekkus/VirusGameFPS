using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    private float health;
    public float maxHealth = 100f;

    public GameObject healthBar;
    public Slider slider;
    
    
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        slider.value = calculateHealth();
    }

    void SetHealth(float health)
    {
        this.health = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        if (health > maxHealth)
        {
            health = maxHealth;
        }
        slider.value = calculateHealth();
    }

    private float calculateHealth()
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
