using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float damage;
    public float iframes;
    public float invicibility;
    public float healingAmount;


    // Start is called before the first frame update
    void Start()
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
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Harmful" && iframes <= 0f)
        {
           health -= damage;
            iframes = invicibility;
            Debug.Log("burned");
        }

        if (other.tag == "PlayerPilot"|| other.tag == "PlayerEngineer"|| other.tag == "PlayerGunner" && gameObject.tag == "PlayerScientist")
        {
           // if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerHealth otherHealth = other.gameObject.GetComponent<PlayerHealth>();
                otherHealth.health += healingAmount;
            }
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

        if (other.tag == "PlayerPilot" || other.tag == "PlayerEngineer" || other.tag == "PlayerGunner" && gameObject.tag == "PlayerScientist")
        {
           // if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerHealth otherHealth = other.gameObject.GetComponent<PlayerHealth>();
                otherHealth.health += healingAmount;
            }
        }

    }

    private void HealPlayer()
    {
        health += healingAmount;
    }
}
