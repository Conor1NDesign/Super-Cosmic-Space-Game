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
    public GameObject toInteractWith;

    public bool canInteract;

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

    public void OnAButtonPress()
    {
        Debug.Log("A Button Pressed");
        if (canInteract)
        {
            Debug.Log("Interacted with " + toInteractWith.name);
            toInteractWith.GetComponent<GenericInteraction>().InteractedA();
        }
    }

    public void OnBButtonPress()
    {
        Debug.Log("B Button Pressed");
        if (canInteract)
        {
            Debug.Log("Interacted with " + toInteractWith.name);
            toInteractWith.GetComponent<GenericInteraction>().InteractedB();
        }
    }

    public void OnXButtonPress()
    {
        Debug.Log("X Button Pressed");
        if (canInteract)
        {
            Debug.Log("Interacted with " + toInteractWith.name);
            toInteractWith.GetComponent<GenericInteraction>().InteractedX();
        }
    }

    public void OnYButtonPress()
    {
        Debug.Log("Y Button Pressed");
        if (canInteract)
        {
            Debug.Log("Interacted with " + toInteractWith.name);
            toInteractWith.GetComponent<GenericInteraction>().InteractedY();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactable")
        {
            canInteract = true;
            toInteractWith = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interactable")
        {
            canInteract = false;
        }
    }

}
