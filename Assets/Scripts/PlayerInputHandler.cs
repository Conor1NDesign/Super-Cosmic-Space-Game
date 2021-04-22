using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{

    private PlayerInput playerInput;
    private PlayerController playerController;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        var playerControllers = FindObjectsOfType<PlayerController>();
        var index = playerInput.playerIndex;
        playerController = playerControllers.FirstOrDefault(m => m.GetPlayerIndex() == index);
        //playerController.playerCamera.SetActive(true);
        //playerInput.camera = playerController.playerCamera.GetComponent<Camera>();
    }

    public void OnMove(CallbackContext context)
    {
        if(playerController != null)
        playerController.SetInputVector(context.ReadValue<Vector2>());
    }

    private void Update()
    {
        gameObject.transform.position = playerController.transform.position;
    }

}
