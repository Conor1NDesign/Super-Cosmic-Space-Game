using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
    public void OnTriggerStay(Collider other)
    {
        var fireScript = other.GetComponent<FireHealth>();
        Debug.Log(other + " has entered the trigger!");

        if (fireScript != null)
        {
            fireScript.fireHp -= 40 * Time.deltaTime;
            Debug.Log("Attempting to decrease fire's HP!");
        }
    }
}
