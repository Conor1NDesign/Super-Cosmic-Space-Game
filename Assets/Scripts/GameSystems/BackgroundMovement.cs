using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public GameObject bridgeSystem;
    public ShipSpeed shipSpeedScript;

    public float currentSpeed;

    public GameObject currentMainPlane;
    
    public GameObject plane1;
    public GameObject plane2;

    public void Awake()
    {
        shipSpeedScript = bridgeSystem.GetComponent<ShipSpeed>();
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = shipSpeedScript.shipActualSpeed;

        if (currentMainPlane == plane1)
        {
            plane1.transform.Translate(0, 0, ((-currentSpeed * 20) * Time.deltaTime));
            plane2.transform.position = new Vector3(plane1.transform.position.x, plane1.transform.position.y, plane1.transform.position.z + 10000);
        }

        if (currentMainPlane == plane2)
        {
            plane2.transform.Translate(0, 0, ((-currentSpeed * 20) * Time.deltaTime));
            plane1.transform.position = new Vector3(plane2.transform.position.x, plane2.transform.position.y, plane2.transform.position.z + 10000);
        }

        if (plane1.transform.position.z <= -10000 && currentMainPlane == plane1)
        {
            plane1.transform.position = plane2.transform.position + new Vector3(plane2.transform.position.x, plane2.transform.position.y, plane2.transform.position.z + 10000);
            currentMainPlane = plane2;
        }

        if (plane2.transform.position.z <= -10000 && currentMainPlane == plane2)
        {
            plane2.transform.position = plane1.transform.position + new Vector3(plane1.transform.position.x, plane1.transform.position.y, plane1.transform.position.z + 10000);
            currentMainPlane = plane1;
        }
    }
}
