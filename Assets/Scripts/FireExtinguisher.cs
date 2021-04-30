using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
    public void OnTriggerStay(Collider other)
    {
        var fireScript = other.GetComponent<FireHealth>();

        if (fireScript != null)
        {
            fireScript.fireHp -= 5;
        }
    }
}
