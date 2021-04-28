using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
     // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var playerHealth = other.GetComponent<PlayerHealth>().health;
        var playerMaxHealth = other.GetComponent<PlayerHealth>().maxHealth;
        if (playerHealth < playerMaxHealth)
        {
            other.GetComponent<PlayerHealth>().HealPlayer();
        }
    }

}
