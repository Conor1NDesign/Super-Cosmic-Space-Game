using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Max Inventory Size")]
    public int maxItems = 3;

    [Header("Current Inventory Items (DO NOT CHANGE FROM 0")]
    public int currentItems;
    public int ammo;
    public int components;
    public int fuel;
    public int medkits;

    [Header("Game Objects and Components")]
    public GameObject trashCompactor;


    public void Trash()
    {
            currentItems = 0;
            fuel = 0;
            ammo = 0;
            components = 0;
    }  


    public void CraftItem(ShipSystems.buttonOptions button)
    {
        if (button == ShipSystems.buttonOptions.XButton && currentItems < maxItems)
        {
            ammo += 1;
            currentItems += 1;
        }

        if (button == ShipSystems.buttonOptions.YButton && currentItems < maxItems)
        {
            components += 1;
            currentItems += 1;
        }

        if (button == ShipSystems.buttonOptions.AButton && currentItems < maxItems)
        {
            fuel += 1;
            currentItems += 1;
        }

        if (button == ShipSystems.buttonOptions.BButton && currentItems < maxItems)
        {
            medkits += 1;
            currentItems += 1;
        }
        Debug.Log("Inventory Items: " + currentItems + " Ammo: " + ammo + " Components: " + components + " Fuel: " + fuel + " Medkits: " + medkits);
    }
}
