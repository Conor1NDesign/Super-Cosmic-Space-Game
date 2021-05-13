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
    public GameObject bridgeControlSystem;
    public GameObject fuelMessage;
    public float displayTime;
    // Start is called before the first frame update
    void Awake()
    {
        bridgeControlSystem = GameObject.Find("BridgeControl");
        currentFuel = maxFuel;
    }

    // Update is called once per frame
    void Update()
    {

        shipSpeed = bridgeControlSystem.GetComponent<ShipSpeed>().shipActualSpeed;
        
       if (currentFuel >= empty)
       { 
           currentFuel -= ((shipSpeed / 10) * Time.deltaTime); 
       }
        
        if (currentFuel <= empty)
        {
            bridgeControlSystem.GetComponent<ShipSpeed>().OutOfFuel();
            fuelAlert();
        }

        if (currentFuel > maxFuel)
        {
            currentFuel = maxFuel;
        }
    }

    public void Refuel()
    {
        currentFuel += refuelValue;
        bridgeControlSystem.GetComponent<ShipSpeed>().Refuel();
    }


    public IEnumerator fuelAlert()
    {
        fuelMessage.SetActive(true);
        yield return new WaitForSeconds(displayTime);
        fuelMessage.SetActive(false);
    }
}
