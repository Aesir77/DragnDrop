using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    public Camera Camera;
    public float walkspeed = 5f;
    public float runspeed = 10f;
    public float lookspeed = 4f;
    public float lookXlimit = 40f;
    public bool canMove = true;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    { //movement
        Vector3 forward = transform.TransformDirection(Vector3.back);
        Vector3 right = transform.TransformDirection(Vector3.left);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runspeed : walkspeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runspeed : walkspeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);




        //rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookspeed;
            rotationX = Mathf.Clamp(rotationX, -lookXlimit, lookXlimit);
            Camera.transform.localRotation = Quaternion.Euler(rotationX, 180, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookspeed, 0);
        }

       



    }

}
