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
    string nameInTrigger;
    public InventoryManager otherInventory;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      

        
    }

    void ActivatedA()
    {
        // scientist delivery
        if (gameObject.tag == "PlayerScientist")
        {
            if (nameInTrigger == "PlayerPilot" && otherInventory.fuel < maxItems)
            {
                currentItems -= 1;
                fuel -= 1;
                otherInventory.currentItems += 1;
                otherInventory.fuel += 1;
            }
            if (nameInTrigger == "PlayerEngineer" && otherInventory.components < maxItems)
            {
                currentItems -= 1;
                components -= 1;
                otherInventory.currentItems += 1;
                otherInventory.components += 1;
            }
            if (nameInTrigger == "PlayerGunner" && otherInventory.ammo < maxItems)
            {
                currentItems -= 1;
                ammo -= 1;
                otherInventory.currentItems += 1;
                otherInventory.ammo += 1;
            }
        }
        if (nameInTrigger == "TrashCompactor")
        {
            currentItems = 0;
            fuel = 0;
            ammo = 0;
            components = 0;
        }
    }  


    void ActivatedB()
    {
        if (gameObject.tag == "PlayerScientist" && nameInTrigger == "CraftingTable" && currentItems<maxItems)
        {
            components += 1;
            currentItems += 1;
        }
    }

    void ActivatedY()
    {
        if (gameObject.tag == "PlayerScientist" && nameInTrigger == "CraftingTable" && currentItems < maxItems)
        {
            fuel += 1;
            currentItems += 1;
        }
    }

    void ActivatedX()
    {
        if (gameObject.tag == "PlayerScientist" && nameInTrigger == "CraftingTable" && currentItems < maxItems)
        {
            ammo += 1;
            currentItems += 1;
        }
    }

    private void OnTriggerStay(Collider other)
    {
       nameInTrigger = other.tag;
       otherInventory = other.gameObject.GetComponent<InventoryManager>();

    }

}
