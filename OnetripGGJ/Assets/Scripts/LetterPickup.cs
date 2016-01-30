using UnityEngine;
using System.Collections;

public class LetterPickup : MonoBehaviour
{
    public GameObject letterPrefab;
    public float pickupInterval;
    public float letterLaunchForce;
    public Transform car;

    private float lastPickupTime;
    private bool inPickupZone = false;
    private Vector3 spawnPos;

    void Start ()
    {
        lastPickupTime = Time.time;
        spawnPos = transform.Find("LetterSpawn").position;
	}

    void Update()
    {
        // If the car is close enough to the depot and enough time has past since the last letter was spawned, spawn a letter
        if (inPickupZone)
        {
            if (Time.time > lastPickupTime + pickupInterval)
            {
                lastPickupTime = Time.time;
                SpawnLetter();
            }
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
            inPickupZone = true;
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "Player")
            inPickupZone = false;
    }

    void SpawnLetter()
    {
        // Create the letter and give it a random direction to fly in initially 
        GameObject letter = (GameObject)GameObject.Instantiate(letterPrefab, spawnPos + Vector3.up, Random.rotation);
        letter.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-10, 10), letterLaunchForce, Random.Range(-10, 10)));
        letter.GetComponent<LetterFlyToCar>().enabled = true;
        letter.GetComponent<LetterFlyToCar>().SetCar(car);
    }
}
