using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    public float currentFuel;
    public float maxFuel;
    private float empty = 0f;
    public float refuelValue;
    public float shipSpeed;
    public GameObject shipSpeedObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        shipSpeed = shipSpeedObject.GetComponent<ShipSpeed>().shipActualSpeed;
        
       if (currentFuel > empty)
       { 
           currentFuel -= (shipSpeed * Time.deltaTime); 
       }
        
        if (currentFuel == empty)
        {
            shipSpeedObject.GetComponent<ShipSpeed>().OutOfFuel();
        }

        if (currentFuel > maxFuel)
        {
            currentFuel = maxFuel;
        }
    }

    public void Refuel()
    {
        currentFuel += refuelValue;
    }
    
}
