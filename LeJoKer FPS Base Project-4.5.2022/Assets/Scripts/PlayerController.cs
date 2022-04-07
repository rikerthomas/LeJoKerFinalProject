using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
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
    public ParticleSystem particles;
    public int enemyHits;
    public int bulletHits;
    public ParticleSystem bulletHit;


    // Start is called before the first frame update
    void Start()
    {
        bulletHits = 0;
        enemyHits = 0;
        particles = GetComponent<ParticleSystem>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(bulletHits >= 15)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
        if(enemyHits >= 10)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            bulletHit.Stop();
            particles.Play();
        }
        if (collision.gameObject.CompareTag("enemy1"))
        {
            bulletHit.Stop();
            enemyHits++;
            particles.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("EnemyBullet"))
        {
            bulletHit.Play();
            bulletHits++;
        }
    }
}
