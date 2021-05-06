using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public enum playerAnimation
    {
        Idle,
        Moving,
        Interacting
    };
    
    public enum playerRole //Enumeration for the 4 main Player Roles.
    {
        Pilot,
        Engineer,
        Gunner,
        Scientist
    };

    public enum craftableItems
    {
        Ammo,
        Components,
        Fuel,
        Medkits
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
    public ShipSystems.buttonOptions requestedButton1;     //Enumeration for the 4 main Input buttons on a gamepad, taken from the ShipSystems script.
    public ShipSystems.buttonOptions requestedButton2;
    public ShipSystems.buttonOptions requestedButton3;
    public ShipSystems.buttonOptions requestedButton4;


    public PlayerHealth playerHealthScript;

    [Header("Throwable Variables")]
    public float throwCurrentCooldown;
    public float throwMaxCooldown;
    public float throwableSpawnDistance; //Distance that the throwable object spawns in front of the player.
    public GameObject ammoPrefab;
    public GameObject componentPrefab;
    public GameObject fuelPrefab;
    public GameObject medkitPrefab;

    [Header("Fire Extinguisher Object")]
    public GameObject extinguisherObject;
    public GameObject gun;

    [Header("Animation Stuff")]
    public Animator playerAnimator;
    public playerAnimation anim;

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

        var rotation = Quaternion.LookRotation(rotationVector);

        //Checks if there is still input coming from the player. This prevents the mesh from rotating back to Y = 0 when there's no input.
        if (moveDirection.magnitude != 0)
        {
            RotateTowardsMovement(rotation);
            anim = playerAnimation.Moving;
        }
        else if (anim != playerAnimation.Interacting)
        {
            anim = playerAnimation.Idle;
        }

        //Keeps the player at y = 0
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        //Cooldown timer ticks down
        if (throwCurrentCooldown >= 0f)
        {
            throwCurrentCooldown -= Time.deltaTime;
        }

        if (anim == playerAnimation.Idle)
        {
            playerAnimator.SetBool("isRunning", false);
            playerAnimator.SetBool("isInteracting", false);
        }

        if (anim == playerAnimation.Moving)
        {
            playerAnimator.SetBool("isRunning", true);
            playerAnimator.SetBool("isInteracting", false);
        }
        if (anim == playerAnimation.Interacting)
        {
            playerAnimator.SetBool("isRunning", false);
            playerAnimator.SetBool("isInteracting", true);
        }
    }

    public void RotateTowardsMovement(Quaternion rotation)
    {

        //Rotates the Player's mesh towards the 'rotation' variable.
        playerMesh.gameObject.transform.rotation = Quaternion.RotateTowards(playerMesh.gameObject.transform.rotation, rotation, rotateSpeed);
    }

    public void RecieveButtonInput(ShipSystems.buttonOptions button)
    {
        if (canInteract)
        {
            if (button == requestedButton1 || button == requestedButton2 || button == requestedButton3 || button == requestedButton4)
            {
                systemInRange.Interaction(button);
                anim = playerAnimation.Interacting;
            }
            else Debug.Log("Wrong button, dingus!");
        }
        else if (role == playerRole.Scientist)
        {
            if (button == ShipSystems.buttonOptions.BButton)
            {
                ThrowItem(craftableItems.Medkits);
            }

            if (button == ShipSystems.buttonOptions.AButton)
            {
                ThrowItem(craftableItems.Fuel);
            }

            if (button == ShipSystems.buttonOptions.XButton)
            {
                ThrowItem(craftableItems.Ammo);
            }

            if (button == ShipSystems.buttonOptions.YButton)
            {
                ThrowItem(craftableItems.Components);
            }
        }
    }

    public void ThrowItem(craftableItems thrownItem)

    {
        if (thrownItem == craftableItems.Medkits && throwCurrentCooldown <= 0f && gameObject.GetComponent<InventoryManager>().medkits > 0)
        {
            Instantiate(medkitPrefab, transform.position + (playerMesh.transform.forward * throwableSpawnDistance), playerMesh.transform.rotation);
            throwCurrentCooldown = throwMaxCooldown;
            gameObject.GetComponent<InventoryManager>().medkits -= 1;
            gameObject.GetComponent<InventoryManager>().currentItems -= 1;
        }

        if (thrownItem == craftableItems.Fuel && throwCurrentCooldown <= 0f && gameObject.GetComponent<InventoryManager>().fuel > 0)
        {
            Instantiate(fuelPrefab, transform.position + (playerMesh.transform.forward * throwableSpawnDistance), playerMesh.transform.rotation);
            throwCurrentCooldown = throwMaxCooldown;
            gameObject.GetComponent<InventoryManager>().fuel -= 1;
            gameObject.GetComponent<InventoryManager>().currentItems -= 1;
        }

        if (thrownItem == craftableItems.Ammo && throwCurrentCooldown <= 0f && gameObject.GetComponent<InventoryManager>().ammo > 0)
        {
            Instantiate(ammoPrefab, transform.position + (playerMesh.transform.forward * throwableSpawnDistance), playerMesh.transform.rotation);
            throwCurrentCooldown = throwMaxCooldown;
            gameObject.GetComponent<InventoryManager>().ammo -= 1;
            gameObject.GetComponent<InventoryManager>().currentItems -= 1;
        }

        if (thrownItem == craftableItems.Components && throwCurrentCooldown <= 0f && gameObject.GetComponent<InventoryManager>().components > 0)
        {
            Instantiate(componentPrefab, transform.position + (playerMesh.transform.forward * throwableSpawnDistance), playerMesh.transform.rotation);
            throwCurrentCooldown = throwMaxCooldown;
            gameObject.GetComponent<InventoryManager>().components -= 1;
            gameObject.GetComponent<InventoryManager>().currentItems -= 1;
        }

    }

}
