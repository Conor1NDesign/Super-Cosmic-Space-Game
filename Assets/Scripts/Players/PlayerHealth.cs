using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Variables:")]
    public float health;
    public int maxHealth;

    [Header("Damage:")]
    public float fireDamage;
    public float suffocationDamage;
    public bool suffocating;
    public float ratDamage;

    [Header("Invincibility After Damage:")]
    public float iframes;
    public float invicibility;

    [Header("Healing From Potions:")]
    public float healingAmount;

    [Header("UI Elements")]
    public Slider healthSlider;


    void Awake()
    {
        iframes = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (iframes >= 0f)
        {
            iframes -= Time.deltaTime;
        }    
        
        if (suffocating)
        {
            health -= ( suffocationDamage * Time.deltaTime);
        }

        if (health <= 0f)
        {
            gameObject.SetActive(false);
        }

        healthSlider.value = health;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Harmful" && iframes <= 0f)
        {
           health -= fireDamage;
            iframes = invicibility;
            Debug.Log("burned");
        }
        if (other.tag == "Rat" && iframes <=0f)
        {
            health -= ratDamage;
            iframes = invicibility;
            Debug.Log("rats rats were the rats");
        }

    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Harmful" && iframes <= 0f)
        {
            health -= fireDamage;
            iframes = invicibility;
            Debug.Log("BURNING  ");
        }
        if (other.tag == "Rat" && iframes <= 0f)
        {
            health -= ratDamage;
            iframes = invicibility;
            Debug.Log("rats rats were the rats");
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

    public void LifeSupportBroke()
    {
        suffocating = true;
    }

    public void Repair()
    {
        suffocating = false;
    }


}
