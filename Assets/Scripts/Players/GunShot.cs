﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShot : MonoBehaviour
{
    public float gunDamage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "rat")
        {
            other.GetComponent<Rat>().ratHealth -= gunDamage;
        }
    }

}