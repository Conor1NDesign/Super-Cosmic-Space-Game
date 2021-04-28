using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int maxItems = 3;
    public int currentItems;
    public int ammo;
    public int components;
    public int fuel;
    public GameObject trashCompactor;
    public GameObject craftingTable;
    public Collider playerInTrigger;
    public InventoryManager otherInventory;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if (fuel >0f) 
      { 


      }
        
    }

    public void Delivery()
    {
        // scientist delivery


        if (playerInTrigger.GetComponent<PlayerController>().role == PlayerController.playerRole.Pilot && otherInventory.components < maxItems)
        {
            currentItems -= 1;
            fuel -= 1;
            otherInventory.currentItems += 1;
            otherInventory.fuel += 1;
        }
        if (playerInTrigger.GetComponent<PlayerController>().role == PlayerController.playerRole.Engineer && otherInventory.components < maxItems)
        {
            currentItems -= 1;
            components -= 1;
            otherInventory.currentItems += 1;
            otherInventory.components += 1;
        }
        if (playerInTrigger.GetComponent<PlayerController>().role == PlayerController.playerRole.Gunner && otherInventory.components < maxItems)
        {
            currentItems -= 1;
            ammo -= 1;
            otherInventory.currentItems += 1;
            otherInventory.ammo += 1;
        }
    }
     
    public void Trash()
    {
            currentItems = 0;
            fuel = 0;
            ammo = 0;
            components = 0;
    }  


    public void CraftingComponents()
    {
        //if (gameObject.tag == "PlayerScientist" && nameInTrigger == "CraftingTable" && currentItems<maxItems)
        
            components += 1;
            currentItems += 1;
        
    }

    public void CraftingFuel()
    {
        //if (gameObject.tag == "PlayerScientist" && nameInTrigger == "CraftingTable" && currentItems < maxItems)
        
            fuel += 1;
            currentItems += 1;
        
    }

    public void CraftingAmmo()
    {
        //if (gameObject.tag == "PlayerScientist" && nameInTrigger == "CraftingTable" && currentItems < maxItems)
        
            ammo += 1;
            currentItems += 1;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        playerInTrigger = other;
       otherInventory = other.gameObject.GetComponent<InventoryManager>();

    }

}
