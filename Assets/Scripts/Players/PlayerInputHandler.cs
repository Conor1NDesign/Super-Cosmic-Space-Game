using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{

    private PlayerInput playerInput;
    private PlayerController playerController;
    public GameObject toInteractWith;

    private float playerMoveSpeed;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        var playerControllers = FindObjectsOfType<PlayerController>();
        var index = playerInput.playerIndex;
        playerController = playerControllers.FirstOrDefault(m => m.GetPlayerIndex() == index);
        playerMoveSpeed = playerController.moveSpeed;
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
                playerController.moveSpeed = 0;
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
                StartCoroutine(GunShot());
            }
            
        }
    }



    public void Update()
    {
        gameObject.transform.position = playerController.transform.position;
    }

    IEnumerator GunShot()
    {
        playerController.gunObject.SetActive(true);
        playerController.moveSpeed = 0;
        Debug.Log ("bang");
        yield return new WaitForSeconds(.1f);
        playerController.gunObject.SetActive(false);
        playerController.moveSpeed = playerMoveSpeed;
    }
}
