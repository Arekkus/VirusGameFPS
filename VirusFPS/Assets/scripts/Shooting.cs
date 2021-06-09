using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject InfParticle;

    private bool alreadyShot = false;
    public float bulletTimer = 2f;
    
    public int ammunition = 15;
    
    public int particleNumber = 3;

    public Camera playerCamera;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButton(0))
        {
            if (!alreadyShot)
            {
                shootBullet();
                Invoke(nameof(ResetAttack), bulletTimer);
            }
            
            
        }
        if (Input.GetMouseButtonDown(1))
        {
            for (int i = 0; i < particleNumber; i++)
            {
                if (ammunition > 0)
                {
                    GameObject bulletObject = Instantiate(InfParticle);
                    bulletObject.transform.position = playerCamera.transform.position + playerCamera.transform.forward/2;
                    bulletObject.transform.forward = playerCamera.transform.forward;
                    ammunition--;
                }
            }
        }
    }
    
    private void ResetAttack()
    {
        alreadyShot = false;
    }

    void shootBullet()
    {
        GameObject bulletObject = Instantiate(Bullet);
        bulletObject.transform.position = playerCamera.transform.position + playerCamera.transform.forward*2;
        bulletObject.transform.forward = playerCamera.transform.forward;
        alreadyShot = true;
    }
}
