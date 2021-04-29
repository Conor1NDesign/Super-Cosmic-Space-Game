﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Variables:")]
    public float health;
    public int maxHealth;

    [Header("Fire Damage:")]
    public float damage;

    [Header("Invincibility After Damage:")]
    public float iframes;
    public float invicibility;

    [Header("Healing From Potions:")]
    public float healingAmount;

    [Header("Potion Prefab and Settings:")]
    public GameObject potion;
    public int potionRange;
    public float potionCurrentCooldown;
    public float potionThrowCooldown;
    void Awake()
    {
        iframes = 0;
        potionCurrentCooldown = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (iframes >= 0f)
        {
            iframes -= Time.deltaTime;
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Harmful" && iframes <= 0f)
        {
           health -= damage;
            iframes = invicibility;
            Debug.Log("burned");
        } 


    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Harmful" && iframes <= 0f)
        {
            health -= damage;
            iframes = invicibility;
            Debug.Log("BURNING  ");
        }

    }

    public void HealPlayer()
    {

        health += healingAmount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }


}
