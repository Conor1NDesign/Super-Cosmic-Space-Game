using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class StarshipController : MonoBehaviour
{
    [Header("Spaceship Acceleration Variables:")]
    //Minimum and Maximum values for Ship Acceleration
    public float shipMinAcceleration;
    public float shipMaxAcceleration;
    [Space(10)]
    [Header("Starship Speed Variables:")]
    //Minimum and Maximum values for Ship Speed
    public float shipMinSpeed;
    public float shipMaxSpeed;
    //Current Ship Acceleration/Speed Values
    private float currentShipAcceleration;
    private float currenShipSpeed;
    [Space(10)]
    [Header("Starship Camera Holder:")]
    public GameObject cameraHolder;
    [Space(10)]
    [Header("Ship Rotates Towards Me:")]
    public GameObject rotateToMe;
    [Space(10)]
    [Header("Camera Sensitivity:")]
    public float cameraSensitivity;
    [Header("Starship Rotation Speed")]
    public float shipRotateSpeed;
    public float cameraLag = 0.03f;  //(0 - 1)

    private Vector2 rotationVector;

    private void Update()
    {
        RotateCamera(rotationVector);
        var lookPos = rotateToMe.transform.position - transform.position;
        //lookPos.z = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        //transform.localRotation = rotation;
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * shipRotateSpeed);
        //transform.localRotation = Quaternion.Slerp(transform.localRotation, rotation, Time.deltaTime * shipRotateSpeed);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, rotation, cameraLag);
        
    }

    public void GetRotationVector(CallbackContext context)
    {
        rotationVector = context.ReadValue<Vector2>();
        Debug.Log(rotationVector);
    }

    public void ChangeAcceleration()
    {
        
    }

    public void RotateCamera(Vector2 rotationDirection)
    {
        cameraHolder.transform.Rotate(-rotationDirection.y, rotationDirection.x, 0);
    }

}
