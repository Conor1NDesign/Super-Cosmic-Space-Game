using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarshipCamera : MonoBehaviour
{
    public Transform starshipPosition;

    // Update is called once per frame
    void Update()
    {
        transform.position = starshipPosition.position;
    }
}
