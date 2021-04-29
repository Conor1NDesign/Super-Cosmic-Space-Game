using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float distance;
    public float shipSpeed;
    public GameObject shipSpeedObject;
    public GameObject winText;
    public GameObject loseText;
    public GameObject engineer;
    public GameObject pilot;
    public GameObject scientist;
    public GameObject gunner;
    public float engineerHp;
    public float pilotHp;
    public float scientistHp;
    public float gunnerHp;
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
        shipSpeed = shipSpeedObject.GetComponent<ShipSpeed>().shipActualSpeed;
        distance -= shipSpeed * Time.deltaTime;

        if (distance == 0f)
        {
            GameWin();
        }

        if (engineerHp == 0f || pilotHp == 0f || scientistHp == 0f || gunnerHp == 0)
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
}
