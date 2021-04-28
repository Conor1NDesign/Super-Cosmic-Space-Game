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

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        var playerControllers = FindObjectsOfType<PlayerController>();
        var index = playerInput.playerIndex;
        playerController = playerControllers.FirstOrDefault(m => m.GetPlayerIndex() == index);
    }

    public void OnMove(CallbackContext context)
    {
        if (playerController != null)
            playerController.SetInputVector(context.ReadValue<Vector2>());
    }

    public void OnButtonPush(CallbackContext context)
    {
        if (playerController != null)
        {
            if (context.performed)
            {
                if (Gamepad.current.buttonSouth.isPressed)
                {
                    var button = ShipSystems.buttonOptions.AButton;
                    playerController.InteractWithSystem(button);
                }

                if (Gamepad.current.buttonEast.isPressed)
                {
                    var button = ShipSystems.buttonOptions.BButton;
                    playerController.InteractWithSystem(button);
                }

                if (Gamepad.current.buttonWest.isPressed)
                {
                    var button = ShipSystems.buttonOptions.XButton;
                    playerController.InteractWithSystem(button);
                }

                if (Gamepad.current.buttonNorth.isPressed)
                {
                    var button = ShipSystems.buttonOptions.YButton;
                    playerController.InteractWithSystem(button);
                }
            }
        }
    }

    private void Update()
    {
        gameObject.transform.position = playerController.transform.position;

    }
}
