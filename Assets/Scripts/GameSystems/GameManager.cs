using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Total Travel Distance")]
    public float distance;
    private float shipSpeed;

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
    }
    void Update()
    {
        if (navBroken)
        {
            shipSpeed = shipSpeedObject.GetComponent<ShipSpeed>().shipActualSpeed;
            distance += (shipSpeed / 10 * Time.deltaTime);
        }

        else
        {
            shipSpeed = shipSpeedObject.GetComponent<ShipSpeed>().shipActualSpeed;
            distance -= (shipSpeed / 20 * Time.deltaTime);
        }


        if (distance <= 0f)
        {
            GameWin();
        }

        if (engineerHp <= 0f && pilotHp <= 0f && scientistHp <= 0f && gunnerHp <= 0)
        {
            GameLoss();
        }
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