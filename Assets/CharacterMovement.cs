using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{

    Vector2 moveInput;
    Vector2 lookInput;

    Rigidbody rb;
    [SerializeField] int moveSpeed = 10;
    [SerializeField] int lookSpeed = 10;
    [SerializeField] int jumpSpeed = 10;

    int mouseSensivity = 10;

    Transform playerCamera;

    float xRotation = 0.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // on r�cup�re dans la hi�rachie le rigid body
        playerCamera = transform.Find("Camera");
    }

    // Update is called once per frame
    void Update()
    {
        TransformMovement();
        FirstPersonLook();
    }

    void TransformMovement()
    {
        transform.Translate(moveInput.x * Time.deltaTime * moveSpeed, 0, moveInput.y * Time.deltaTime * moveSpeed);
    }

    void Look()
    {
        transform.Rotate(0, lookInput.x * Time.deltaTime * lookSpeed, 0);
        xRotation -= lookInput.x * Time.deltaTime * lookSpeed;
        playerCamera.localRotation = Quaternion.Euler(Mathf.Clamp(xRotation,-90f,90f), 0, 0);
    }

    void FirstPersonLook()
    {
        float mouseX = lookInput.x * mouseSensivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90.0f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }

    void RigidbodyMovement()
    {
        rb.linearVelocity = new Vector3(moveInput.x * moveSpeed, rb.linearVelocity.y, moveInput.y * moveSpeed);
    }

    void OnLook(InputValue inputValue)
    {
        lookInput = inputValue.Get<Vector2>();
    }

    void OnJump(InputValue inputValue)
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x,
            rb.linearVelocity.y +
            jumpSpeed, rb.linearVelocity.z);
    }
    // input System on peut appeler les fonctions de mouvements 
    void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();

    }
}
