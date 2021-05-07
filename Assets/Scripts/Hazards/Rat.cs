using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour
{
    public GameObject spawnOrigin;
    public float ratHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ratHealth <= 0)
        {
            spawnOrigin.GetComponent<Mess>().currentRats -= 1f;
            Destroy(gameObject);
        }
    }


}
