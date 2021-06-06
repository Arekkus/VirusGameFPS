using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject InfParticle;

    public int particleNumber = 3;

    public Camera playerCamera;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bulletObject = Instantiate(Bullet);
            bulletObject.transform.position = playerCamera.transform.position + playerCamera.transform.forward;
            bulletObject.transform.forward = playerCamera.transform.forward;
        }
        if (Input.GetMouseButtonDown(1))
        {
            for (int i = 0; i < particleNumber; i++)
            {
                GameObject bulletObject = Instantiate(InfParticle);
                bulletObject.transform.position = playerCamera.transform.position + playerCamera.transform.forward/2;
                bulletObject.transform.forward = playerCamera.transform.forward;
            }
        }
    }
}
