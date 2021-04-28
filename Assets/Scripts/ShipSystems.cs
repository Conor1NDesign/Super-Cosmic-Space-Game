using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class ShipSystems : MonoBehaviour
{
        public enum buttonOptions //Enumeration for the 4 main Input buttons on a gamepad, plus an option to select one at random.
        {
            AButton,
            BButton,
            XButton,
            YButton,
            Random
        };

    [Header("Interaction Buttons:")]
    public buttonOptions buttons;

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
        //rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (buttons != buttonOptions.AButton)
        {
            Debug.Log("Omaghod guys it's not set to A");
        }

        if (buttons == buttonOptions.AButton)
        {
            Debug.Log("Omaghod guys it's set to A");
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
        if (buttons == buttonOptions.Random)
        {
            buttons = (buttonOptions)Random.Range(0, 4);
            playerController.requestedButton = buttons;
            buttons = buttonOptions.Random;
        }
        else
            playerController.requestedButton = buttons;

        beingInteracted = true;
        playerController.systemInRange = GetComponent<ShipSystems>();
        //buttonPrompt.SetActive(true);
        playerController.canInteract = true;
    }


    public void Interaction()
    {
        Debug.Log("Oh golly gee, I've been INTERACTED WITH!");
        if (testingDinger.activeInHierarchy)
            testingDinger.SetActive(false);
        else testingDinger.SetActive(true);
    }

    public void LifeSupportInteract()
    {
        if (interactingPlayer.GetComponent<PlayerController>().role == PlayerController.playerRole.Pilot)
        {

        }
    }
}