using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector3 playerVelocity;
    [SerializeField] private bool touchingGround;
    [SerializeField] private float playerSpeedWalk = 2.0f;
    [SerializeField] private float playerSpeedSwim = 3.0f;
    [SerializeField] private float mouseSensibility = 1.0f;
    [SerializeField] private float jumpForce = 1.0f;
    [SerializeField] private float gravity = 0; //-9.81

    [SerializeField] private bool swiming;
    [SerializeField] private Vector2 movementInput;
    [SerializeField] private Vector2 lookInput;

    private PlayerInput playerInput;
    private CharacterController characterController;

    [SerializeField] private float XRotation = 0;
    [SerializeField] private float YRotation = 90;

    void Start()
    {
        playerInput = gameObject.GetComponent<PlayerInput>();
        characterController = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        //groundedPlayer = controller.isGrounded;
        //if (groundedPlayer && playerVelocity.y < 0)
        //{
        //    playerVelocity.y = 0f;
        //}
        /*
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Makes the player jump
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);*/

        movementInput = playerInput.actions["Movement"].ReadValue<Vector2>();
        lookInput = playerInput.actions["Look"].ReadValue<Vector2>();
        //Debug.Log("Vect x: " + movementInput.x + "      Vect y: " + movementInput.y);

        Swim();
        RotateCamera();
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void Swim()
    {
        //transform.Translate((Vector3.forward * movementInput.y + transform.right * movementInput.x) * Time.deltaTime * playerSpeedSwim);

        Vector3 movement = transform.forward * movementInput.y + transform.right * movementInput.x;

        //transform.localRotation = Quaternion.Euler(lookInput.y, lookInput.x, 0);
        if (playerInput.actions["Jump"].IsPressed())
        {
            //Debug.Log("Brincando");
            movement.y += 0.3f;
        }
        
        characterController.Move(movement * playerSpeedSwim * Time.deltaTime);
    }

    private void Walk()
    {

    }

    private void RotateCamera()
    {
        //Rotacion horizontal
        YRotation += lookInput.x * mouseSensibility * Time.deltaTime;

        // Rotación vertical de la cámara (eje X)
        XRotation -= lookInput.y * mouseSensibility * Time.deltaTime;
        XRotation = Mathf.Clamp(XRotation, -90f, 90f); // Limitar la rotación vertical entre -90 y 90 grados

        // Aplicar la rotación a la cámara
        transform.localRotation = Quaternion.Euler(XRotation, YRotation, 0);
    }
}
