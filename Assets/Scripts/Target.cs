using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;

    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -2;

    private GameManager gameManager;

    public int pointValue;

    public ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>(); // Initialize target rigidbody
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); // Get reference to game manager script

        // Toss object into air randomly
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    private float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    private Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    //private void OnMouseDown()
    //{
    //    if (gameManager.isGameActive && !gameManager.paused)
    //    {
    //        Destroy(gameObject);
    //        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
    //        gameManager.UpdateScore(pointValue);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        // Remove life if target missed
        if (!gameObject.CompareTag("Bad") && gameManager.isGameActive)
        {
            gameManager.LoseLife();
        }
    }

    public void DestroyTarget()
    {
        if (gameManager.isGameActive && !gameManager.paused)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
    }
}
