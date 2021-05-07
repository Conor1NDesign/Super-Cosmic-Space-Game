using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalImpacts : MonoBehaviour
{
    [Header("Ship System Variables")]
    public int numberOfSystems;
    public GameObject bridgeControlSystem;
    public GameObject craftingBenchSystem;
    public GameObject engineeringSystem;
    public GameObject fuelStationSystem;
    public GameObject lifeSupportSystem;
    //public GameObject researchTableSystem;
    private int selectedSystem;

    [Header("Max Number of Impacts")]
    public int maxImpacts;
    private int impactsRemaining;

    [Header("Impact Timer References")]
    public float impactMaxCooldown;
    public float impactMinCooldown;
    public float impactCurrentCooldown;

    [Header("Impact Roll Value")]
    public int diceRoll;

    [Header("Mess Generation Variables")]
    public int numberOfMessZones;
    public GameObject messSpawnZone01;
    public GameObject messSpawnZone02;
    public GameObject messSpawnZone03;
    public GameObject messSpawnZone04;
    public GameObject messSpawnZone05;
    public GameObject messSpawnZone06;
    //If more spawn areas are added, put them here lmao
    public int messSpawnChance;
    private int selectedMessZone;

    public List<GameObject> activeCameras;




    public void Awake()
    {
        
    }

    void Update()
    {
        if (impactCurrentCooldown > 0f)
        {
            impactCurrentCooldown -= (Time.deltaTime * (bridgeControlSystem.GetComponent<ShipSpeed>().shipActualSpeed / 10));
        }
        if (impactCurrentCooldown <= 0f)
        {
            ImpactOccurance();
            impactCurrentCooldown = (Random.Range(impactMinCooldown, impactMaxCooldown + 1));
        }
    }

    public void RollTheDice(int maxRollValue)
    {
        diceRoll = Random.Range(1, maxRollValue);
    }

    public void ImpactOccurance()
    {
        RollTheDice(maxImpacts + 1);
        impactsRemaining = diceRoll;

        for(int i = 0; i < impactsRemaining; impactsRemaining--)
        {
            RollTheDice(numberOfSystems + 1);
            selectedSystem = diceRoll;

            if (selectedSystem == 1)
                bridgeControlSystem.GetComponent<ShipSystems>().Impact();

            if (selectedSystem == 2)
                craftingBenchSystem.GetComponent<ShipSystems>().Impact();

            if (selectedSystem == 3)
                engineeringSystem.GetComponent<ShipSystems>().Impact();

            if (selectedSystem == 4)
                fuelStationSystem.GetComponent<ShipSystems>().Impact();

            if (selectedSystem == 5)
                lifeSupportSystem.GetComponent<ShipSystems>().Impact();

            //if (selectedSystem == 5)6
                //researchTableSystem.GetComponent<ShipSystems>().Impact();
        }

        RollTheDice(101);

        if(diceRoll <= messSpawnChance)
        {
            RollTheDice(numberOfMessZones);
            selectedMessZone = diceRoll;

            if (selectedMessZone == 1)
                messSpawnZone01.GetComponent<MessController>().MakeAMess();

            if (selectedMessZone == 2)
                messSpawnZone02.GetComponent<MessController>().MakeAMess();

            if (selectedMessZone == 3)
                messSpawnZone03.GetComponent<MessController>().MakeAMess();

            if (selectedMessZone == 4)
                messSpawnZone04.GetComponent<MessController>().MakeAMess();

            if (selectedMessZone == 5)
                messSpawnZone05.GetComponent<MessController>().MakeAMess();

            if (selectedMessZone == 6)
                messSpawnZone06.GetComponent<MessController>().MakeAMess();
        }

        foreach(GameObject cameras in activeCameras)
        {
            StartCoroutine(cameras.GetComponent<CameraShake>().ShakeTheCamera());
        }
    }
}
