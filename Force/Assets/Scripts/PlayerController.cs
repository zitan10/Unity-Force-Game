﻿using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    public Camera PlayerCam;

    private float speed;
    [SerializeField]
    private float lookSensitivity = 5f;

    private float jumpSpeed = 10f;

    private PlayerMotor motor;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
     
        //Check if player wants to sprint
        speed = Input.GetKey(KeyCode.LeftShift) ? 10f : 5f;

        //Calculate movement velocity as a 3D vector
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 movHorizontal = transform.right * xMov;
        Vector3 movVertical = transform.forward * zMov;
        Vector3 movUp;

        if (Input.GetKey(KeyCode.Space))
        {
            //Jump Speed
            movUp = transform.up * jumpSpeed;
        }
        //Don't Jump
        else
        {
            movUp = transform.up * 0;
        }

        //Final movement vector
        Vector3 velocity = (movHorizontal + movVertical + movUp).normalized * speed;

        //Apply movement
        motor.Move(velocity);

        //Calculate rotation as a 3D vector (turning around)
        float yRot = Input.GetAxisRaw("Mouse X");

        Vector3 rotation = new Vector3(0f, yRot, 0f) * lookSensitivity;

        //Apply rotation
        motor.Rotate(rotation);

        //Calculate camera as a 3D vector (turning around)
        float xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 cameraRotation = new Vector3(xRot, 0f, 0f) * lookSensitivity;

        //Apply rotation
        motor.RotateCamera(cameraRotation);

    }

}
