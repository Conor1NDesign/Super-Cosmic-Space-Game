using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Total Travel Distance")]
    public float distance;
    private float shipSpeed;
    public float shipSpeedModifier = 10f;
    public float shipSpeedModeifierBroken = 20f;
    


    [Header("Time Pressure Element")]
    public float timeRemaining;
    //percentage of distance you can travel at min speed and still succeed (presented as a decimal)
    public float percentageAtMinSpeed = 0.5f;
    

    [Header("Bridge Controle")]
    public GameObject shipSpeedObject;

    [Header("Win/Loss UI")]
    public GameObject winText;
    public GameObject loseText;

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
    private void Awake()
    {
        engineerHp = engineer.GetComponent<PlayerHealth>().health;
        pilotHp = pilot.GetComponent<PlayerHealth>().health;
        scientistHp = scientist.GetComponent<PlayerHealth>().health;
        gunnerHp = gunner.GetComponent<PlayerHealth>().health;
        var minspeed = shipSpeedObject.GetComponent<ShipSpeed>().minSpeed;
        timeRemaining  =((distance / (minspeed / shipSpeedModifier)) * percentageAtMinSpeed);
    }
    void Update()
    {
        shipSpeed = shipSpeedObject.GetComponent<ShipSpeed>().shipActualSpeed;

        if (navBroken)
        {
            
            distance += (shipSpeed / shipSpeedModeifierBroken * Time.deltaTime);
        }

        else
        {
            
            distance -= (shipSpeed / shipSpeedModifier* Time.deltaTime);
        }


        if (distance <= 0f)
        {
            GameWin();
        }

        if ((engineerHp <= 0f && pilotHp <= 0f && scientistHp <= 0f && gunnerHp <= 0) || (timeRemaining <= 0f))
        {
            GameLoss();
        }

        timeRemaining -= Time.deltaTime;
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
}