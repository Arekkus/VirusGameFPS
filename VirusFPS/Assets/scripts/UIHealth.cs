using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    public Text healthText;
    public GameObject player;
    public Slider slider;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + player.GetComponent<PlayerHealth>().health;
        slider.value = player.GetComponent<PlayerHealth>().calculateHealth();
    }
}
