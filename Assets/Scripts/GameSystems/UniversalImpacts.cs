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
    public GameObject researchTableSystem;
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
            RollTheDice(numberOfSystems);
            selectedSystem = diceRoll;

            if (selectedSystem == 0)
                bridgeControlSystem.GetComponent<ShipSystems>().Impact();

            if (selectedSystem == 1)
                craftingBenchSystem.GetComponent<ShipSystems>().Impact();

            if (selectedSystem == 2)
                engineeringSystem.GetComponent<ShipSystems>().Impact();

            if (selectedSystem == 3)
                fuelStationSystem.GetComponent<ShipSystems>().Impact();

            if (selectedSystem == 4)
                lifeSupportSystem.GetComponent<ShipSystems>().Impact();

            if (selectedSystem == 5)
                researchTableSystem.GetComponent<ShipSystems>().Impact();
        }

    }
}
