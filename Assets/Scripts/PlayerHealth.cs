using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public int maxHealth;
    public float damage;
    public float iframes;
    public float invicibility;
    public float healingAmount;
    public bool canHeal;
    public GameObject potion;
    public int potionRange;
    public float potionCooldown;
    public float healRate;


    // Start is called before the first frame update
    void Start()
    {
        iframes = 0;
        potionCooldown = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        if (iframes >= 0f)
        {
            iframes -= Time.deltaTime;
        } 

        if (potionCooldown >= 0f)
        {
            potionCooldown -= Time.deltaTime;
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

    public void ThrowPotion()

    {
        if (potionCooldown <= 0f) 
        {
            Instantiate(potion, transform.position + (transform.forward * 2), transform.rotation);
            potionCooldown = healRate;
        }
        
    }
}
