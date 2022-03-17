using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleEnemyMove : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public float speed = 1.0f;
    public GameObject bullet;
    public GameObject bullet2;

    public Rigidbody enemy;

    public Camera fpsCam;
    public float range = 100f;

    public bool isFrozen;

    public float freezeTimer = 0.0f;
    public float unFreeze = 10f;


    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if(isFrozen)
        {
            freezeTimer += Time.deltaTime;
        }

        if (unFreeze <= freezeTimer && isFrozen)
        {
            enemy.isKinematic = false;
            isFrozen = false;
            agent.speed = 3.5f;
            agent.angularSpeed = 120f;
            freezeTimer = 0.0f;
            

        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //if the enemy is hit by the bullet then their movement will stop. If their movement is stopped, a timer will tick up,
        //and if the timer hits enough seconds, the enemy will regain movement. timer++
        //if boject is hit again, it will infreeze itself. 
        agent.SetDestination(player.position * speed * Time.deltaTime);
    }


    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("EnemyHit");
        if (!isFrozen && other.gameObject.tag == "bullet")
        {
            agent.speed = 0f;
            agent.angularSpeed = 0f;
            isFrozen = true;
            enemy.isKinematic = true;
        }

        if (other.gameObject.tag == "bullet2")
        {
            agent.speed = 0f;
            agent.angularSpeed = 0f;
            enemy.isKinematic = true;
        }
    }
}