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


    [Header("Animation Stuff")]
    public Animator playerAnimator;
    public playerAnimation anim;

    [Header("Gun Stuff")]
    public bool readyToFire;
    private float playerDefaultMoveSpeed;
    public float gunCycleRate;
    public GameObject gunObject;
    public float fireRate;
    public GameObject thisMess;

    [Header("UI Elements")]
    public GameObject playerCard;
    public GameObject craftingButtons;
    public GameObject bridgeButtons;
    public GameObject fuelButtons;
    public GameObject aButton;
    public GameObject bButton;
    public GameObject xButton;
    public GameObject yButton;
    public GameObject componentIcon;
    public GameObject pilotReq;
    public GameObject engiReq;
    public GameObject sciReq;
    public GameObject janiReq;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerHealthScript = GetComponent<PlayerHealth>();
        readyToFire = true;
        playerDefaultMoveSpeed = moveSpeed;
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
    public void Update()
    {
        if (playerHealthScript.isDead == false)
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

            //Gunshot Cooldown

            if (gunCycleRate > 0)
            {
                gunCycleRate -= Time.deltaTime;
            }

            if (gunCycleRate <= 0 && gameObject.GetComponent<InventoryManager>().currentItems > 0)
            {
                readyToFire = true;

            }

            //Button Popups
            if (canInteract == true)
            {
                if (systemInRange.shipSystem == ShipSystems.systemType.CraftingBench && role == playerRole.Scientist)
                {
                    craftingButtons.SetActive(true);
                }

                else if (systemInRange.shipSystem == ShipSystems.systemType.BridgeControls && role == playerRole.Pilot)
                {
                    bridgeButtons.SetActive(true);
                }

                else if (systemInRange.shipSystem == ShipSystems.systemType.FuelStation && role == playerRole.Pilot)
                {
                    fuelButtons.SetActive(true);
                }

                else
                {
                    if (requestedButton1 == ShipSystems.buttonOptions.AButton)
                    {
                        aButton.SetActive(true);
                        if (role == playerRole.Engineer)
                        {
                            aButton.SetActive(false);
                            bButton.SetActive(true);
                            componentIcon.SetActive(true);
                        }
                    }

                    else if (requestedButton1 == ShipSystems.buttonOptions.BButton)
                    {
                        bButton.SetActive(true);
                        if (role == playerRole.Engineer)
                            componentIcon.SetActive(true);
                    }

                    else if (requestedButton1 == ShipSystems.buttonOptions.XButton)
                    {
                        xButton.SetActive(true);
                        if (role == playerRole.Engineer)
                        {
                            xButton.SetActive(false);
                            bButton.SetActive(true);
                            componentIcon.SetActive(true);
                        }
                    }

                    else if (requestedButton1 == ShipSystems.buttonOptions.YButton)
                    {
                        yButton.SetActive(true);
                        if (role == playerRole.Engineer)
                        {
                            yButton.SetActive(false);
                            bButton.SetActive(true);
                            componentIcon.SetActive(true);
                        }
                    }
                }
            }

            else
            {
                craftingButtons.SetActive(false);
                bridgeButtons.SetActive(false);
                fuelButtons.SetActive(false);
                if (thisMess == null)
                    aButton.SetActive(false);
                bButton.SetActive(false);
                xButton.SetActive(false);
                yButton.SetActive(false);
                componentIcon.SetActive(false);
            }

            if (extinguisherObject.activeSelf)
            {
                controller.Move(-playerMesh.transform.forward / 75);
            }
        }
    }

    public void RotateTowardsMovement(Quaternion rotation)
    {

            //Rotates the Player's mesh towards the 'rotation' variable.
            playerMesh.gameObject.transform.rotation = Quaternion.RotateTowards(playerMesh.gameObject.transform.rotation, rotation, rotateSpeed);
    }

    public void RecieveButtonInput(ShipSystems.buttonOptions button)
    { 
        if (!GetComponent<PlayerHealth>().isDead)
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

            else if (role == playerRole.Gunner)
            {
                if (button == ShipSystems.buttonOptions.AButton)
                {
                    if (thisMess != null)
                        thisMess.GetComponent<Mess>().CleanMess(gameObject);
                }
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
                gameObject.GetComponent<InventoryManager>().UpdateInventoryUI();
            }

            if (thrownItem == craftableItems.Fuel && throwCurrentCooldown <= 0f && gameObject.GetComponent<InventoryManager>().fuel > 0)
            {
                Instantiate(fuelPrefab, transform.position + (playerMesh.transform.forward * throwableSpawnDistance), playerMesh.transform.rotation);
                throwCurrentCooldown = throwMaxCooldown;
                gameObject.GetComponent<InventoryManager>().fuel -= 1;
                gameObject.GetComponent<InventoryManager>().currentItems -= 1;
                gameObject.GetComponent<InventoryManager>().UpdateInventoryUI();
            }

            if (thrownItem == craftableItems.Ammo && throwCurrentCooldown <= 0f && gameObject.GetComponent<InventoryManager>().ammo > 0)
            {
                Instantiate(ammoPrefab, transform.position + (playerMesh.transform.forward * throwableSpawnDistance), playerMesh.transform.rotation);
                throwCurrentCooldown = throwMaxCooldown;
                gameObject.GetComponent<InventoryManager>().ammo -= 1;
                gameObject.GetComponent<InventoryManager>().currentItems -= 1;
                gameObject.GetComponent<InventoryManager>().UpdateInventoryUI();
            }

            if (thrownItem == craftableItems.Components && throwCurrentCooldown <= 0f && gameObject.GetComponent<InventoryManager>().components > 0)
            {
                Instantiate(componentPrefab, transform.position + (playerMesh.transform.forward * throwableSpawnDistance), playerMesh.transform.rotation);
                throwCurrentCooldown = throwMaxCooldown;
                gameObject.GetComponent<InventoryManager>().components -= 1;
                gameObject.GetComponent<InventoryManager>().currentItems -= 1;
                gameObject.GetComponent<InventoryManager>().UpdateInventoryUI();
            }

        }

        public IEnumerator GunShot()
        {
            gunObject.SetActive(true);
            moveSpeed = 0;
            yield return new WaitForSeconds(.1f);
            gunObject.SetActive(false);
            moveSpeed = playerDefaultMoveSpeed;
            readyToFire = false;
            gameObject.GetComponent<InventoryManager>().currentItems -= 1;
            gameObject.GetComponent<InventoryManager>().ammo -= 1;
            gameObject.GetComponent<InventoryManager>().UpdateInventoryUI();
            gunCycleRate = fireRate;
        }
}