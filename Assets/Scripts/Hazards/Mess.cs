using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mess : MonoBehaviour
{
    public GameObject rat;
    private float ratTimer;
    public float ratTimerMax;
    public float maxNumberOfRats;
    private float currentRats;
    public GameObject ratSpawn1;
    public GameObject ratSpawn2;
    public GameObject ratSpawn3;
    private int whichSpawn;

    public

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ratTimer -= Time.deltaTime;

        if (ratTimer <= 0f && currentRats <= maxNumberOfRats)
        {
            ratTimer = ratTimerMax;
        }

        else if (ratTimer <= 0f)
        {
            ratTimer = ratTimerMax;
            SpawnRat();
            currentRats += 1;
        }

    }

    public void CleanMess()
    {
        Destroy(gameObject);
    }

    public void SpawnRat()
    {
        whichSpawn = Random.Range(1, 4);
        if (whichSpawn == 1) 
        {
            Instantiate(rat, ratSpawn1.transform);
        }
        if (whichSpawn == 2)
        {
            Instantiate(rat, ratSpawn2.transform);
        }
        if (whichSpawn == 3)
        {
            Instantiate(rat, ratSpawn3.transform);
        }
    }
    

}
