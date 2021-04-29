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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (outOfFuel)
        {
          shipActualSpeed -= Time.deltaTime;
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
        if (!outOfFuel || shipActualSpeed < maxSpeed)
        {
            shipActualSpeed += acceleartion;
            Debug.Log("Zoom zoom poggy woggy :)");
        }
    }

    public void Deccelerate()
    {
        if (shipActualSpeed > minSpeed)
        { 
            shipActualSpeed += decceleration;
            Debug.Log("Brakes.");
        }
    }

    public void OutOfFuel()
    {
        outOfFuel = true;
    }

}
