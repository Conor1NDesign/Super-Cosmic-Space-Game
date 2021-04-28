using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericInteraction : MonoBehaviour
{
    public GameObject system;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void InteractedA()
    {
        Debug.Log("Interaction Successful");
        system.SendMessage("ActivatedA");
    }

    public void InteractedB()
    {
        Debug.Log("Interaction Successful");
        system.SendMessage("ActivatedB");
    }

    public void InteractedX()
    {
        Debug.Log("Interaction Successful");
        system.SendMessage("ActivatedX");
    }

    public void InteractedY()
    {
        Debug.Log("Interaction Successful");
        system.SendMessage("ActivatedY");
    }


}
