using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public int yeetPower = 30;
    public float lifetimeDuration = 5f;

    public enum itemPickup
    {
        Ammo,
        Component,
        Fuel,
        Medkit
    };

    public itemPickup itemType;

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
            //MEDKIT PICKUP
            if (itemType == itemPickup.Medkit)
            {
                var playerHealth = other.GetComponent<PlayerHealth>().health;
                var playerMaxHealth = other.GetComponent<PlayerHealth>().maxHealth;
                if (playerHealth < playerMaxHealth)
                {
                    other.GetComponent<PlayerHealth>().HealPlayer();
                    Destroy(gameObject);
                }
            }

            //AMMO PICKUP
            if (itemType == itemPickup.Ammo)
            {
                var playerInventory = other.GetComponent<InventoryManager>();
                if (playerInventory != null)
                {
                    var playerCurrentlyHolding = playerInventory.currentItems;
                    var playerMaxHolding = playerInventory.maxItems;
                    if (playerCurrentlyHolding < playerMaxHolding && other.GetComponent<PlayerController>().role == PlayerController.playerRole.Gunner)
                    {
                        playerInventory.ammo += 1;
                        playerInventory.currentItems += 1;
                        playerInventory.UpdateInventoryUI();
                        Destroy(gameObject);
                    }
                }
                else return;
            }

            //FUEL PICKUP
            if (itemType == itemPickup.Fuel)
            {
                var playerInventory = other.GetComponent<InventoryManager>();
                if (playerInventory != null)
                {
                    var playerCurrentlyHolding = playerInventory.currentItems;
                    var playerMaxHolding = playerInventory.maxItems;
                    if (playerCurrentlyHolding < playerMaxHolding && other.GetComponent<PlayerController>().role == PlayerController.playerRole.Pilot)
                    {
                        playerInventory.fuel += 1;
                        playerInventory.currentItems += 1;
                        playerInventory.UpdateInventoryUI();
                        Destroy(gameObject);
                    }
                }
                else return;
            }

            //COMPONENT PICKUP
            if (itemType == itemPickup.Component)
            {
                var playerInventory = other.GetComponent<InventoryManager>();
                if (playerInventory != null)
                {
                    var playerCurrentlyHolding = playerInventory.currentItems;
                    var playerMaxHolding = playerInventory.maxItems;
                    if (playerCurrentlyHolding < playerMaxHolding && other.GetComponent<PlayerController>().role == PlayerController.playerRole.Engineer)
                    {
                        playerInventory.components += 1;
                        playerInventory.currentItems += 1;
                        playerInventory.UpdateInventoryUI();
                        Destroy(gameObject);
                    }
                }
                else return;
            }
        }
    }

    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(lifetimeDuration);
        Destroy(gameObject);
    }

}
