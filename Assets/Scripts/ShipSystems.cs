using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class ShipSystems : MonoBehaviour
{
    public float systemHp;
    public float maxSystemHp;
    private float minSystemHp = 0f;
    public float impactTimer;
    public float shipSpeed;
    public float fireChance;
    private float fireTrigger;
    public float maintanenceValue;
    private float repairHp;
    public bool broken;
    public GameObject fire;
    public GameObject shipSpeedObject;
    public enum buttonOptions //Enumeration for the 4 main Input buttons on a gamepad, plus an option to select one at random.
    {
        AButton,
        BButton,
        XButton,
        YButton,
        Random,
        None
    };

    public enum systemType //Enumeration for all of the possible ship systems in the game. Detemines what function will be called upon interaction.
    {
        BridgeControls,
        CraftingBench,
        DroneControls,
        EngineeringControls,
        FuelStation,
        GunnerControls,
        LifeSupport,
        ResearchTable,
        TrashBin
    };

    [Header("System Type:")]
    public systemType shipSystem;

    [Header("Interaction Buttons:")]
    public buttonOptions button1;
    public buttonOptions button2;
    public buttonOptions button3;
    public buttonOptions button4;

    //Boolean values for each PlayerRole, ticked roles can interact with the System this script is attached to.
    [Header("Player Roles Allowed:")]
    public bool pilotAllowed;
    public bool engineerAllowed;
    public bool gunnerAllowed;
    public bool scientistAllowed;

    [Header("UI Elements Relating to the object:")]
    public GameObject buttonPrompt = null;

    [Header("Who is interacting with me right now?")]
    public Collider interactingPlayer;

    [Space(10)]
    private bool beingInteracted = false;
    public GameObject testingDinger;
    
    public void Awake()
    {

        impactTimer = Random.Range(shipSpeed, 90f);
    }

    // Update is called once per frame
    void Update()
    {
        shipSpeed = shipSpeedObject.GetComponent<ShipSpeed>().shipActualSpeed;

        if (impactTimer > 0f)
        {
            impactTimer -= Time.deltaTime;
        }
        if (impactTimer == 0f)
        {
            Impact();
        }
        if (systemHp > maxSystemHp)
        {
            systemHp = maxSystemHp;
        }
        if (systemHp < minSystemHp)
        {
            systemHp =  minSystemHp;
        }
        if (systemHp == 0f)
        {
            broken = true;
        }
        

    }

    public void OnTriggerEnter(Collider other)
    {
        //Ensures that the object entering the trigger has a PlayerController script on it, otherwise ends the function immediately.
        if (other.GetComponent<PlayerController>() != null)
        {
            //Creates a playerController variable that operates only within this function.
            var playerController = other.GetComponent<PlayerController>();

            //Checks to make sure that no other eligible Players were already in range of this system beforehand, ends the function if there is.
            if (!beingInteracted)
            {
                //PILOT CHECK
                if (playerController.role == PlayerController.playerRole.Pilot && pilotAllowed)
                {
                    interactingPlayer = other;
                    WakeSystem(playerController);
                }
                else Debug.Log("Pilot not found");

                //ENGINEER CHECK
                if (playerController.role == PlayerController.playerRole.Engineer && engineerAllowed)
                {
                    interactingPlayer = other;
                    WakeSystem(playerController);
                }
                else Debug.Log("Engineer not found");

                //GUNNER CHECK
                if (playerController.role == PlayerController.playerRole.Gunner && gunnerAllowed)
                {
                    interactingPlayer = other;
                    WakeSystem(playerController);
                }
                else Debug.Log("Gunner not found");

                //SCIENTIST CHECK
                if (playerController.role == PlayerController.playerRole.Scientist && scientistAllowed)
                {
                    interactingPlayer = other;
                    WakeSystem(playerController);
                }
                else Debug.Log("Scientist not found");
            }
            else
            {
                Debug.Log("System is already being interacted with!");
                return;
            }
        }
        else
        {
            Debug.Log("Object has no PlayerController script component!");
            return;
        }
    }


    public void OnTriggerExit(Collider other)
    {
        if (other == interactingPlayer)
        {
            other.GetComponent<PlayerController>().canInteract = false;
            beingInteracted = false;
            //buttonPrompt.SetActive(false);
        }
        else return;
    }

    public void WakeSystem(PlayerController playerController)
    {
        //If the interaction button is set to 'Random', randomizes the button for the player.
        if (button1 == buttonOptions.Random)
        {
            button1 = (buttonOptions)Random.Range(0, 4);
            playerController.requestedButton1 = button1;
            button1 = buttonOptions.Random;
        }
        else
            playerController.requestedButton1 = button1;

        if (button2 == buttonOptions.Random)
        {
            button2 = (buttonOptions)Random.Range(0, 4);
            playerController.requestedButton2 = button2;
            button2 = buttonOptions.Random;
        }
        else
            playerController.requestedButton2 = button2;

        if (button3 == buttonOptions.Random)
        {
            button3 = (buttonOptions)Random.Range(0, 4);
            playerController.requestedButton3 = button3;
            button3 = buttonOptions.Random;
        }
        else
            playerController.requestedButton3 = button3;

        if (button4 == buttonOptions.Random)
        {
            button4 = (buttonOptions)Random.Range(0, 4);
            playerController.requestedButton4 = button4;
            button4 = buttonOptions.Random;
        }
        else
            playerController.requestedButton4 = button4;

        beingInteracted = true;
        playerController.systemInRange = GetComponent<ShipSystems>();
        //buttonPrompt.SetActive(true);
        playerController.canInteract = true;
    }


    public void Interaction(buttonOptions button)
    {
        if (testingDinger.activeInHierarchy)
            testingDinger.SetActive(false);
        else testingDinger.SetActive(true);

        if (shipSystem == systemType.CraftingBench)
        {
            interactingPlayer.GetComponent<InventoryManager>().CraftItem(button);
        }

        if (shipSystem == systemType.BridgeControls)
        {
            BridgeControl(button);
        }
    }

    public void BridgeControl(buttonOptions button)
    {
        if (button == buttonOptions.AButton)
        {
            gameObject.GetComponent<ShipSpeed>().Accelerate();
        }

        if (button == buttonOptions.BButton)
        {
            gameObject.GetComponent<ShipSpeed>().Deccelerate();
        }
    }

    public void Impact()
    {
        systemHp -= (Random.Range(1, shipSpeed));
        fireChance = (maxSystemHp - systemHp);
        fireTrigger = Random.Range(1, 101);
        
        
        if (fireChance > fireTrigger)
        {
            Fire();
        }

        impactTimer = (Random.Range(shipSpeed, 120) - shipSpeed);

    }
    public void Fire()
    {
        Instantiate(fire);
    }

    public void Maintain()
    {
        systemHp += maintanenceValue;
    }

    public void Repair()
    {
        if (broken)
        {
            systemHp = repairHp;
        }
        
    }

}