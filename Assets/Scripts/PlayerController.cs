using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public enum playerRole //Enumeration for the 4 main Player Roles.
    {
        Pilot,
        Engineer,
        Gunner,
        Scientist
    };

    [Header("PLAYER ROLE")]
    public playerRole role;

    [Header("Movement Variables")]
    public float moveSpeed;
    public float rotateSpeed;

    [Header("Object and Component Assignments")]
    public GameObject playerCamera;
    public GameObject playerMesh;

    [Header("Player Index Number: Player1 = 0, Player2 = 1, Etc.")]
    public int playerIndex = 0;

    //Variable for the CharacterController component on the Player.
    private CharacterController controller;

    //Variables that handle Joystick Axis input.
    private Vector3 moveDirection = Vector3.zero;
    private Vector2 inputVector = Vector2.zero;

    [Header("Variables Relating to Ship Systems")]
    //Variables that handle Players interacting with ShipSystems
    public ShipSystems systemInRange;
    public bool canInteract;
    public ShipSystems.buttonOptions requestedButton;     //Enumeration for the 4 main Input buttons on a gamepad, taken from the ShipSystems script.
    public PlayerHealth playerHealthScript;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerHealthScript = GetComponent<PlayerHealth>();
    }

    public int GetPlayerIndex()
    {
        return playerIndex;
    }

    public void SetInputVector(Vector2 direction)
    {
        inputVector = direction;
    }


    // Update is called once per frame
    void Update()
    {
        var rotationVector = new Vector3(inputVector.x, 0, inputVector.y);
        moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = Quaternion.Euler(0, playerCamera.gameObject.transform.eulerAngles.y, 0) * moveDirection;
        moveDirection *= moveSpeed;

        controller.Move(moveDirection * Time.deltaTime);
        
        var rotation = Quaternion.LookRotation(moveDirection);

        //Checks if there is still input coming from the player. This prevents the mesh from rotating back to Y = 0 when there's no input.
        if (moveDirection.magnitude != 0)
        {
            RotateTowardsMovement(rotation);
        }

        //Keeps the player at y = 0
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);

    }

    public void RotateTowardsMovement(Quaternion rotation)
    {

        //Rotates the Player's mesh towards the 'rotation' variable.
        playerMesh.gameObject.transform.rotation = Quaternion.RotateTowards(playerMesh.gameObject.transform.rotation, rotation, rotateSpeed);
    }

    public void InteractWithSystem(ShipSystems.buttonOptions button)
    {
        if (canInteract)
        {
            if (button == requestedButton)
            {
                systemInRange.Interaction();
            }
            else Debug.Log("Wrong button, dingus!");
        }
        else if (role == playerRole.Scientist)
        {
            if (button == ShipSystems.buttonOptions.BButton)
            {
                playerHealthScript.ThrowPotion();
            }
        }
    }

}
