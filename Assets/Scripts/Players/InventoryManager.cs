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

    [Header("UI Elements")]
    [Header("Fuel UI")]
    public GameObject fuel1;
    public GameObject fuel2;
    public GameObject fuel3;

    [Header("Ammo UI")]
    public GameObject ammo1;
    public GameObject ammo2;
    public GameObject ammo3;

    [Header("Component UI")]
    public GameObject component1;
    public GameObject component2;
    public GameObject component3;

    [Header("Medkit UI")]
    public GameObject medkit1;
    public GameObject medkit2;
    public GameObject medkit3;


    public void Trash()
    {
        currentItems = 0;
        fuel = 0;
        ammo = 0;
        components = 0;
        UpdateInventoryUI();
    }  


    public void CraftItem(ShipSystems.buttonOptions button)
    {
        if (button == ShipSystems.buttonOptions.XButton && currentItems < maxItems)
        {
            ammo += 1;
            currentItems += 1;
            UpdateInventoryUI();
        }

        if (button == ShipSystems.buttonOptions.YButton && currentItems < maxItems)
        {
            components += 1;
            currentItems += 1;
            UpdateInventoryUI();
        }

        if (button == ShipSystems.buttonOptions.AButton && currentItems < maxItems)
        {
            fuel += 1;
            currentItems += 1;
            UpdateInventoryUI();
        }

        if (button == ShipSystems.buttonOptions.BButton && currentItems < maxItems)
        {
            medkits += 1;
            currentItems += 1;
            UpdateInventoryUI();
        }
        Debug.Log("Inventory Items: " + currentItems + " Ammo: " + ammo + " Components: " + components + " Fuel: " + fuel + " Medkits: " + medkits);
    }


    public void UpdateInventoryUI()
    {
        //SCIENTIST
        if (gameObject.GetComponent<PlayerController>().role == PlayerController.playerRole.Scientist)
        {
            //FUEL
            if (fuel == 0)
            {
                fuel1.SetActive(false);
                fuel2.SetActive(false);
                fuel3.SetActive(false);
            }
            else if (fuel == 1)
            {
                fuel1.SetActive(true);
                fuel2.SetActive(false);
                fuel3.SetActive(false);
            }
            else if (fuel == 2)
            {
                fuel1.SetActive(true);
                fuel2.SetActive(true);
                fuel3.SetActive(false);
            }
            else if (fuel == 3)
            {
                fuel1.SetActive(true);
                fuel2.SetActive(true);
                fuel3.SetActive(true);
            }

            //COMPONENTS
            if (components == 0)
            {
                component1.SetActive(false);
                component2.SetActive(false);
                component3.SetActive(false);
            }
            else if (components == 1)
            {
                component1.SetActive(true);
                component2.SetActive(false);
                component3.SetActive(false);
            }
            else if (components == 2)
            {
                component1.SetActive(true);
                component2.SetActive(true);
                component3.SetActive(false);
            }
            else if (components == 3)
            {
                component1.SetActive(true);
                component2.SetActive(true);
                component3.SetActive(true);
            }

            //AMMO
            if (ammo == 0)
            {
                ammo1.SetActive(false);
                ammo2.SetActive(false);
                ammo3.SetActive(false);
            }
            else if (ammo == 1)
            {
                ammo1.SetActive(true);
                ammo2.SetActive(false);
                ammo3.SetActive(false);
            }
            else if (ammo == 2)
            {
                ammo1.SetActive(true);
                ammo2.SetActive(true);
                ammo3.SetActive(false);
            }
            else if (ammo == 3)
            {
                ammo1.SetActive(true);
                ammo2.SetActive(true);
                ammo3.SetActive(true);
            }

            //MEDKITS
            if (medkits == 0)
            {
                medkit1.SetActive(false);
                medkit2.SetActive(false);
                medkit3.SetActive(false);
            }
            else if (medkits == 1)
            {
                medkit1.SetActive(true);
                medkit2.SetActive(false);
                medkit3.SetActive(false);
            }
            else if (medkits == 2)
            {
                medkit1.SetActive(true);
                medkit2.SetActive(true);
                medkit3.SetActive(false);
            }
            else if (medkits == 3)
            {
                medkit1.SetActive(true);
                medkit2.SetActive(true);
                medkit3.SetActive(true);
            }
        }

        //PILOT
        if (gameObject.GetComponent<PlayerController>().role == PlayerController.playerRole.Pilot)
        {
            //FUEL
            if (fuel == 0)
            {
                fuel1.SetActive(false);
                fuel2.SetActive(false);
                fuel3.SetActive(false);
            }
            else if (fuel == 1)
            {
                fuel1.SetActive(true);
                fuel2.SetActive(false);
                fuel3.SetActive(false);
            }
            else if (fuel == 2)
            {
                fuel1.SetActive(true);
                fuel2.SetActive(true);
                fuel3.SetActive(false);
            }
            else if (fuel == 3)
            {
                fuel1.SetActive(true);
                fuel2.SetActive(true);
                fuel3.SetActive(true);
            }
        }

        //ENGINEER
        if (gameObject.GetComponent<PlayerController>().role == PlayerController.playerRole.Engineer)
        {
            //COMPONENTS
            if (components == 0)
            {
                component1.SetActive(false);
                component2.SetActive(false);
                component3.SetActive(false);
            }
            else if (components == 1)
            {
                component1.SetActive(true);
                component2.SetActive(false);
                component3.SetActive(false);
            }
            else if (components == 2)
            {
                component1.SetActive(true);
                component2.SetActive(true);
                component3.SetActive(false);
            }
            else if (components == 3)
            {
                component1.SetActive(true);
                component2.SetActive(true);
                component3.SetActive(true);
            }
        }

        //GUNNER/JANITOR
        if (gameObject.GetComponent<PlayerController>().role == PlayerController.playerRole.Gunner)
        {
            //AMMO
            if (ammo == 0)
            {
                ammo1.SetActive(false);
                ammo2.SetActive(false);
                ammo3.SetActive(false);
            }
            else if (ammo == 1)
            {
                ammo1.SetActive(true);
                ammo2.SetActive(false);
                ammo3.SetActive(false);
            }
            else if (ammo == 2)
            {
                ammo1.SetActive(true);
                ammo2.SetActive(true);
                ammo3.SetActive(false);
            }
            else if (ammo == 3)
            {
                ammo1.SetActive(true);
                ammo2.SetActive(true);
                ammo3.SetActive(true);
            }
        }
    }
}
