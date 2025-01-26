using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 playerVelocity;
    //[SerializeField] private bool touchingGround;
    //[SerializeField] private float playerSpeedWalk = 2.0f;
    [SerializeField] private float playerSpeedSwim = 3.0f;
    [SerializeField] private float mouseSensibility = 1.0f;
    [SerializeField] private float jumpForce = 1.0f;
    [SerializeField] private float gravity = -9.81f;

    [SerializeField] private bool swiming;
    private Vector2 movementInput;
    private Vector2 lookInput;

    private PlayerInput playerInput;
    private CharacterController characterController;

    [SerializeField] private float XRotation = 0;
    [SerializeField] private float YRotation = 90;

    [SerializeField] private LayerMask InteractableLayer;
    [SerializeField] private float RangeDetection = 2f;
    
    private bool ultimo;
    private Outline outlineScript;
    private GameObject lastObject;

    private Inventory inventory;

    //[Header("Gravity")]
    //[SerializeField] 
    private bool isGrounded;
    [SerializeField] private float moveSpeed = 5f; // Velocidad de movimiento
    [SerializeField] private float jumpHeight = 2f; // Altura del salto
    private Vector3 velocity; // Almacena la velocidad del jugador
    [SerializeField] Oxigen oxigen;

    void Start()
    {
        playerInput = gameObject.GetComponent<PlayerInput>();
        characterController = gameObject.GetComponent<CharacterController>();
        inventory = gameObject.GetComponent<Inventory>();
        swiming = false;

        oxigen = gameObject.GetComponent<Oxigen>();
    }

    void Update()
    {
        
        movementInput = playerInput.actions["Movement"].ReadValue<Vector2>();
        lookInput = playerInput.actions["Look"].ReadValue<Vector2>();
        //Debug.Log("Vect x: " + movementInput.x + "      Vect y: " + movementInput.y);

        Cursor.lockState = CursorLockMode.Locked;
        RotateCamera();
        SelectedObject();

        if (swiming)
        {
            Swim();
            oxigen.ActivateOxigen(true);
        }
        else
        {
            Walk();
            oxigen.ActivateOxigen(false);
        }

    }

    public void SelectedObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;        

        if (Physics.Raycast(ray, out hit, RangeDetection, InteractableLayer))
        {
            GameObject selectedObject = hit.collider.gameObject;
            lastObject = selectedObject;

            outlineScript = selectedObject.GetComponent<Outline>();
            outlineScript.enabled = true;

            ultimo = true;
        }
        else if(ultimo == true)
        {
            ultimo = false;
            outlineScript.enabled = false;

        }

        if (playerInput.actions["Interact"].WasPressedThisFrame() && ultimo)
        {
            if (lastObject.CompareTag("Button"))
            {
                InteractButton interactButton = lastObject.GetComponent<InteractButton>();
                interactButton.OnInteract(inventory.obtainedObjects);
            }
            else
            {
                Destroy(lastObject);
                //Debug.Log("Objeto obtenido: " + lastObject.name);
                inventory.ObtainObjects(lastObject.name);
            }
            ultimo = false;
        }
    }

    private void Swim()
    {
        
        Vector3 movement = transform.forward * movementInput.y + transform.right * movementInput.x;

        if (playerInput.actions["Jump"].IsPressed())
        {
            //Debug.Log("Brincando");
            movement.y += 0.3f;
        }
        
        characterController.Move(movement * playerSpeedSwim * Time.deltaTime);
    }

    private void Walk()
    {
        isGrounded = characterController.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Evita acumulación de gravedad cuando está en el suelo
        }

        // Movimiento horizontal

        Vector3 movement = transform.forward * movementInput.y + transform.right * movementInput.x;

        characterController.Move(movement * moveSpeed * Time.deltaTime);

        // Salto
        if (playerInput.actions["Jump"].IsPressed() && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Fórmula para calcular velocidad de salto
        }

        // Aplicar gravedad
        velocity.y += gravity * Time.deltaTime;

        // Mover al personaje según la gravedad
        characterController.Move(velocity * Time.deltaTime);

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
