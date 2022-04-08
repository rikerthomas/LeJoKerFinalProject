using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemy;
    public float spawnTimeDelay = 3f;
    public float spawnTimeInGame = 2f;
    public Vector3 spawnLocation = new Vector3(25, 5, 3);



    void Spawn()
    {
        GameObject SpawnLocation = (GameObject)Instantiate(enemy, spawnLocation, Quaternion.identity);
    }

    void Start()
    {
        // Call the Spawn function after a delay of the spawnTimeDelay and then continue to call after a certain amount of time.
        InvokeRepeating("Spawn", spawnTimeDelay, spawnTimeInGame);
    }
}
