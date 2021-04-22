using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed;

    [SerializeField]
    public GameObject playerCamera;

    [SerializeField]
    public float rotateSpeed;

    [SerializeField]
    public GameObject playerMesh;

    [SerializeField]
    private int playerIndex = 0;

    private CharacterController controller;

    private Vector3 moveDirection = Vector3.zero;
    private Vector2 inputVector = Vector2.zero;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
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
        
    }

    public void RotateTowardsMovement(Quaternion rotation)
    {

        //Rotates the Player's mesh towards the 'rotation' variable.
        playerMesh.gameObject.transform.rotation = Quaternion.RotateTowards(playerMesh.gameObject.transform.rotation, rotation, rotateSpeed);
    }
}
