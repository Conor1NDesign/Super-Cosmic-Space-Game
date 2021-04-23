using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int maxItems = 3;
    public int currentItems;
    public int ammo;
    public int maxAmmo;
    public int components;
    public int maxComponents;
    public int fuel;
    public int maxFuel;
    public GameObject trashCompactor;
    public GameObject craftingTable;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentItems < maxItems)
        {
            currentItems += 1;
        }

        
    }

    void Activated()
    {

    }


}
