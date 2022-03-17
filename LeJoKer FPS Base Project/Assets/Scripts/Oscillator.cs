using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    float movementFactor;
    public Vector3 movementVector;
    [SerializeField] float period = 2f;
    public Rigidbody enemyrb;
    public GameObject enemy;

    public bool isFrozen;

    public float freezeTimer = 0.0f;
    public float unFreeze = 10f;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<GameObject>();
        enemyrb = GetComponent<Rigidbody>();
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFrozen)
        {
            freezeTimer += Time.deltaTime;
        }

        if (unFreeze <= freezeTimer && isFrozen)
        {
            period = 2f;
            isFrozen = false;
            enemyrb.isKinematic = false;
            freezeTimer = 0.0f;
        }

        if (period <= Mathf.Epsilon) { return; } //to prevent NaN error from happening when period equals 0. 
        float cycles = Time.time / period; // continually growing over time
        const float tau = Mathf.PI * 2; // constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1.

        movementFactor = (rawSinWave + 1f) / 2f; // recalculated to go from 0 to 1 so it's cleaner. Easy to read as well

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;


    }

    public void OnTriggerEnter(Collider other)
    {
        if(!isFrozen && other.gameObject.tag == "bullet")
        {
            enemyrb.isKinematic = true;
            period = 0f;
            isFrozen = true;
        }

        if(!isFrozen && other.gameObject.tag == "bullet2")
        {
            enemyrb.isKinematic = true;
            period = 0f;
        }
    }
}
