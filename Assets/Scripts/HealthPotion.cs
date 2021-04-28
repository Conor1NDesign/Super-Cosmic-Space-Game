using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public int yeetPower = 30;
    public float lifetimeDuration = 5f;

    public void Awake()
    {
        gameObject.GetComponent<Rigidbody>().velocity = transform.forward * yeetPower;
        transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
        //Debug.Log("Attempting to yeet!");

        StartCoroutine(SelfDestroy());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        { 
            var playerHealth = other.GetComponent<PlayerHealth>().health;
            var playerMaxHealth = other.GetComponent<PlayerHealth>().maxHealth;
            if (playerHealth < playerMaxHealth)
            {
                other.GetComponent<PlayerHealth>().HealPlayer();
                Destroy(gameObject);
            }
        }
    }

    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(lifetimeDuration);
        Destroy(gameObject);
    }

}
