using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Total Travel Distance")]
    public float maxDistance;
    public float currentDistance;
    private float shipSpeed;
    public float shipSpeedModifier = 10f;
    public float shipSpeedModeifierBroken = 20f;
    
    [Header("Time Pressure Element")]
    public float timeRemaining;
    //percentage of distance you can travel at min speed and still succeed (presented as a decimal)
    public float percentageAtMinSpeed = 0.5f;
    
    [Header("Bridge Controls and Refueling Station")]
    public GameObject shipSpeedObject;
    public GameObject fuelStation;

    [Header("Win/Loss UI")]
    public GameObject winText;
    public GameObject loseText;
    [Header("Progress Slider")]
    public Slider progressSlider;
    [Header("Ship Speed Text Object")]
    public Text shipSpeedText;
    [Header("FuelGauge")]
    public Slider fuelGauge;
    [Header("Time Remaining UI")]
    public Slider timerUI;

    [Header("ShipMessages")]
    public GameObject impactMessage;
    public GameObject enginesMessage;
    public GameObject lifeSupportMessage;
    public float displayTime;

    [Header("Player Objects")]
    public GameObject pilot;
    public GameObject engineer;
    public GameObject scientist;
    public GameObject gunner;
    private bool engineerDead;
    private bool pilotDead;
    private bool scientistDead;
    private bool gunnerDead;
    private bool navBroken;

    [Header("Ship Life Support System")]
    public ShipSystems lifeSupport;
    public ShipSystems bridgeControls;
    public ShipSystems engineControls;

    // Update is called once per frame
    public void Awake()
    {
        var minspeed = shipSpeedObject.GetComponent<ShipSpeed>().minSpeed;
        timeRemaining  = ((maxDistance / (minspeed / shipSpeedModifier)) * percentageAtMinSpeed);
        progressSlider.maxValue = maxDistance;
        fuelGauge.maxValue = fuelStation.GetComponent<Fuel>().maxFuel;
        timerUI.maxValue = timeRemaining;
    }
    public void Update()
    {
        shipSpeed = shipSpeedObject.GetComponent<ShipSpeed>().shipActualSpeed;

        //Get Player's HP values
        engineerDead = engineer.GetComponent<PlayerHealth>().isDead;
        pilotDead = pilot.GetComponent<PlayerHealth>().isDead;
        scientistDead = scientist.GetComponent<PlayerHealth>().isDead;
        gunnerDead = gunner.GetComponent<PlayerHealth>().isDead;

        if (navBroken)
        {
            currentDistance -= (shipSpeed / shipSpeedModeifierBroken * Time.deltaTime);
        }

        else
        {
            currentDistance += (shipSpeed / shipSpeedModifier* Time.deltaTime);
        }


        if (currentDistance >= maxDistance)
        {
            GameWin();
        }

        if ((pilotDead && engineerDead && scientistDead && gunnerDead) || (timeRemaining <= 0f))
        {
            GameLoss();
        }

        timeRemaining -= Time.deltaTime;
        progressSlider.value = currentDistance;
        shipSpeedText.text = shipSpeed.ToString();

        //Updates fuel and timer UI
        fuelGauge.value = fuelStation.GetComponent<Fuel>().currentFuel;
        timerUI.value = timeRemaining;

        //Checks if life support is broken, and if it is, displays a warning.
        if (lifeSupport.broken)
            lifeSupportMessage.SetActive(true);
        else
            lifeSupportMessage.SetActive(false);

        if (engineControls.broken || bridgeControls.broken)
            enginesMessage.SetActive(true);
        else
            enginesMessage.SetActive(false);
    }

    private void GameWin()
    {
        winText.SetActive(true);
    }

    private void GameLoss()
    {
        loseText.SetActive(true);
    }

    public void BrokenNav()
    {
        navBroken = true;
    }

    public void Repair()
    {
        navBroken = false;
    }

    public IEnumerator ImpactAlert()
    {
        impactMessage.SetActive(true);
        yield return new WaitForSeconds(displayTime);
        impactMessage.SetActive(false);
    }
}