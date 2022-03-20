using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public CharacterController characterController;
    private float movementX;
    private float movementZ;
    public float speed = 5f;
    Vector3 velocity;
    public float gravity = -9.81f;
    public float onGroud = 0f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;
    public float jumpHeight = 3f;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        

        movementX = Input.GetAxis("Horizontal");
        movementZ = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * movementX + transform.forward * movementZ;

        characterController.Move(movement * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }
}
