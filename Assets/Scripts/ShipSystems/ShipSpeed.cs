using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpeed : MonoBehaviour
{
    public float shipActualSpeed;
    public float acceleartion;
    public float decceleration;
    private bool outOfFuel;
    public float maxSpeed;
    public float minSpeed;
    private bool engineBroken;

    public void Awake()
    {
        shipActualSpeed = minSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (outOfFuel || engineBroken)
        {
          shipActualSpeed -= (Time.deltaTime * 5);
        }

        if (shipActualSpeed > maxSpeed)
        {
            shipActualSpeed = maxSpeed;
        }

        if (shipActualSpeed < minSpeed)
        {
            shipActualSpeed = minSpeed;
        }
    }

    public void Accelerate()
    {
        if (!outOfFuel && shipActualSpeed < maxSpeed)
        {
            shipActualSpeed += acceleartion;
            Debug.Log("Zoom zoom poggy woggy :)");
        }
    }

    public void Deccelerate()
    {
        if (shipActualSpeed > minSpeed)
        { 
            shipActualSpeed -= decceleration;
            Debug.Log("Brakes.");
        }
    }

    public void OutOfFuel()
    {
        outOfFuel = true;
    }

    public void BrokenEngine()
    {
        engineBroken = true;
    }

    public void Refuel()
    {
        if (outOfFuel == true)
        {
            outOfFuel = false;
        }
    }

    public void Repair()
    {
        engineBroken = false;
    }

}
