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

    [Header("ShipMessages")]
    public GameObject impactMessage;
    public GameObject fuelMessage;
    public GameObject lifeSupportMessage;
    public float displayTime;


    [Header("Player Objects")]
    public GameObject pilot;
    public GameObject engineer;
    public GameObject scientist;
    public GameObject gunner;
    private float engineerHp;
    private float pilotHp;
    private float scientistHp;
    private float gunnerHp;
    private bool navBroken;
    

    // Update is called once per frame
    public void Awake()
    {
        var minspeed = shipSpeedObject.GetComponent<ShipSpeed>().minSpeed;
        timeRemaining  = ((maxDistance / (minspeed / shipSpeedModifier)) * percentageAtMinSpeed);
        progressSlider.maxValue = maxDistance;
        fuelGauge.maxValue = fuelStation.GetComponent<Fuel>().maxFuel;
    }
    public void Update()
    {
        shipSpeed = shipSpeedObject.GetComponent<ShipSpeed>().shipActualSpeed;

        //Get Player's HP values
        engineerHp = engineer.GetComponent<PlayerHealth>().health;
        pilotHp = pilot.GetComponent<PlayerHealth>().health;
        scientistHp = scientist.GetComponent<PlayerHealth>().health;
        gunnerHp = gunner.GetComponent<PlayerHealth>().health;

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

        if ((engineerHp <= 0f && pilotHp <= 0f && scientistHp <= 0f && gunnerHp <= 0) || (timeRemaining <= 0f))
        {
            GameLoss();
        }

        timeRemaining -= Time.deltaTime;
        progressSlider.value = currentDistance;
        shipSpeedText.text = shipSpeed.ToString();

        fuelGauge.value = fuelStation.GetComponent<Fuel>().currentFuel;
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



    public IEnumerator lifeSupportAlert()
    {
        lifeSupportMessage.SetActive(true);
        yield return new WaitForSeconds(displayTime);
        lifeSupportMessage.SetActive(false);
    }

}