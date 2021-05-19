using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mess : MonoBehaviour
{
    public GameObject rat;
    public float ratTimer;
    public float ratTimerMax;
    public float maxNumberOfRats;
    public float currentRats;
    public GameObject ratSpawn1;
    public GameObject ratSpawn2;
    public GameObject ratSpawn3;
    private int whichSpawn;
    public bool canClean;

    // Update is called once per frame
    void Update()
    {
        ratTimer -= Time.deltaTime;

        if (ratTimer <= 0f && currentRats >= maxNumberOfRats)
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

    public void CleanMess(GameObject janitor)
    {

        Destroy(gameObject);
    }

    public void SpawnRat()
    {
        rat.GetComponent<Rat>().spawnOrigin = gameObject;
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
    public void OnTriggerEnter(Collider other)
    {
        var janitor = other.GetComponent<PlayerController>();
        if (janitor != null)
        {
            if (janitor.role == PlayerController.playerRole.Gunner)
            {
                canClean = true;
                janitor.thisMess = gameObject;
                janitor.aButton.SetActive(true);
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        var janitor = other.GetComponent<PlayerController>();
        if (janitor != null)
        {
            if (janitor.role == PlayerController.playerRole.Gunner)
            {
                janitor.thisMess = null;
                janitor.aButton.SetActive(false);
            }
        }
    }

}
