using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.SceneManagement;

public class PlayerInputHandler : MonoBehaviour
{
    private MenuController menuController;

    private PlayerInput playerInput;
    private PlayerController playerController;
    public GameObject toInteractWith;
    private InventoryManager inventoryManager;

    private GameObject playerCanvas;
    private GameObject playerCamera;

    private MenuButtons startButtonObject;
    private MenuButtons quitPromptObject;
    private MenuButtons quitConfirmObject;

    private float playerMoveSpeed;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        playerInput = GetComponent<PlayerInput>();

        var menuControllers = FindObjectsOfType<MenuController>();
        var index = playerInput.playerIndex;
        menuController = menuControllers.FirstOrDefault(m => m.GetPlayerIndex() == index);
        menuController.UpdateJoinedUI();

        startButtonObject = GameObject.Find("PlayButton").GetComponent<MenuButtons>();
        quitPromptObject = GameObject.Find("QuitPromptButton").GetComponent<MenuButtons>();


        /* COMMENTED CHUNK OF AWAKE METHOD OUT FOR MAIN MENU TESTING
        playerInput = GetComponent<PlayerInput>();
        var playerControllers = FindObjectsOfType<PlayerController>();
        var index = playerInput.playerIndex;
        playerController = playerControllers.FirstOrDefault(m => m.GetPlayerIndex() == index);
        playerMoveSpeed = playerController.moveSpeed;
        inventoryManager = playerController.GetComponent<InventoryManager>();

        playerCamera = transform.Find("Camera").gameObject;

        if (playerController.playerIndex == 0)
        {
            playerCanvas = GameObject.Find("Canvas_P1");
        }

        if (playerController.playerIndex == 1)
        {
            playerCanvas = GameObject.Find("Canvas_P2");
        }

        if (playerController.playerIndex == 2)
        {
            playerCanvas = GameObject.Find("Canvas_P3");
        }

        if (playerController.playerIndex == 3)
        {
            playerCanvas = GameObject.Find("Canvas_P4");
        }

        playerCanvas.GetComponent<Canvas>().worldCamera = playerCamera.GetComponent<Camera>();
        playerCanvas.GetComponent<Canvas>().planeDistance = 1;
        */
    }

    public void OnMove(CallbackContext context)
    {
        if (playerController != null)
            playerController.SetInputVector(context.ReadValue<Vector2>());
    }

    public void OnAButtonPush(CallbackContext context)
    {
        if (playerController != null)
        {
            if (context.performed)
            {
                var button = ShipSystems.buttonOptions.AButton;
                playerController.RecieveButtonInput(button);
            }
        }

        else if (quitConfirmObject != null)
        {
            quitConfirmObject.QuitConfirm();
        }
    }

    public void OnBButtonPush(CallbackContext context)
    {
        if (playerController != null)
        {
            if (context.performed)
            {
                var button = ShipSystems.buttonOptions.BButton;
                playerController.RecieveButtonInput(button);
            }
        }
        else if (quitConfirmObject != null)
        {
            quitConfirmObject.CancelQuitting();
            quitConfirmObject = null;
        }

        else if (quitPromptObject != null)
        {
            quitPromptObject.QuitPrompt();
            quitConfirmObject = quitPromptObject.exitPrompt.GetComponent<MenuButtons>();
        }
    }

    public void OnXButtonPush(CallbackContext context)
    {
        if (playerController != null)
        {
            if (context.performed)
            {
                var button = ShipSystems.buttonOptions.XButton;
                playerController.RecieveButtonInput(button);
            }
        }
    }

    public void OnYButtonPush(CallbackContext context)
    {
        if (playerController != null)
        {
            if (context.performed)
            {
                var button = ShipSystems.buttonOptions.YButton;
                playerController.RecieveButtonInput(button);
            }
        }
    }

    public void OnLeftTriggerPress(CallbackContext context)
    {
        if (playerController != null && playerController.role == PlayerController.playerRole.Gunner)
        {
            if (context.performed)
            {
                playerController.moveSpeed = 0.1f;
                playerController.extinguisherObject.SetActive(true);
            }

            if (context.canceled)
            {
                playerController.moveSpeed = playerMoveSpeed;
                playerController.extinguisherObject.SetActive(false);
            }
        }
    }

   public void OnRightTriggerPress(CallbackContext context)
    {
        if (playerController != null && playerController.role == PlayerController.playerRole.Gunner && playerController.readyToFire)
        {
            if (context.started)
            {
                StartCoroutine(playerController.GunShot());
            }
            
        }
    }

    public void OnBackButtonPress(CallbackContext context)
    {
        if (playerController != null)
        {
            if (context.performed)
                playerController.playerCard.SetActive(true);

            if (context.canceled)
                playerController.playerCard.SetActive(false);
        }
    }

    public void OnStartButtonPress(CallbackContext context)
    {
        if (startButtonObject != null)
        {
            if (context.performed)
                startButtonObject.PlayTheGame();
        }
    }



    public void Update()
    {
        if (playerController != null)
        gameObject.transform.position = playerController.transform.position;

        if (playerController == null && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Main_ShipScene"))
        {
            OnSceneLoaded();
        }
    }


    public void OnSceneLoaded()
    {
        Debug.Log("Hey this works!");
        startButtonObject = null;
        quitPromptObject = null;

        var playerControllers = FindObjectsOfType<PlayerController>();
        var index = playerInput.playerIndex;
        playerController = playerControllers.FirstOrDefault(m => m.GetPlayerIndex() == index);
        playerMoveSpeed = playerController.moveSpeed;
        inventoryManager = playerController.GetComponent<InventoryManager>();

        playerCamera = transform.Find("Camera").gameObject;
        playerCamera.SetActive(true);

        if (playerController.playerIndex == 0)
        {
            playerCanvas = GameObject.Find("Canvas_P1");
        }

        if (playerController.playerIndex == 1)
        {
            playerCanvas = GameObject.Find("Canvas_P2");
        }

        if (playerController.playerIndex == 2)
        {
            playerCanvas = GameObject.Find("Canvas_P3");
        }

        if (playerController.playerIndex == 3)
        {
            playerCanvas = GameObject.Find("Canvas_P4");
        }

        playerCanvas.GetComponent<Canvas>().worldCamera = playerCamera.GetComponent<Camera>();
        playerCanvas.GetComponent<Canvas>().planeDistance = 1;
    }
}
