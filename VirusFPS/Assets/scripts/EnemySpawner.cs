using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private bool alreadySpawned = false;
    public float spawnTimer = 5f;

    public GameObject infParticle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      spawnInfParticle();   
    }

    void spawnInfParticle()
    {
        if (!alreadySpawned)
        {
            GameObject infParticleObject = Instantiate(infParticle);
            infParticleObject.transform.position = transform.position + transform.forward*2;
            alreadySpawned = true;
            Invoke(nameof(ResetSpawn),spawnTimer );
        }
    }

    void ResetSpawn()
    {
        alreadySpawned = false;
    }
}
