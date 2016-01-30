using UnityEngine;
using System.Collections;

public class LetterPickup : MonoBehaviour
{
    public GameObject[] packagePrefabs;
    public float pickupInterval;
    public float packageLaunchForce;
    public Transform car;

    private float lastPickupTime;
    private bool inPickupZone = false;
    private Vector3 spawnPos;
    private Car carScript;
    private int packagesInAir = 0;

    void Start ()
    {
        carScript = car.GetComponent<Car>();
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
                if (carScript.PackageCount + packagesInAir < 3)
                {
                    lastPickupTime = Time.time;
                    SpawnPackage();
                }
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

    void SpawnPackage()
    {
        // Create the letter and give it a random direction to fly in initially 
        GameObject package = (GameObject)GameObject.Instantiate(packagePrefabs[Random.Range(0, packagePrefabs.Length)], spawnPos + Vector3.up, Random.rotation);
        package.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-10, 10), packageLaunchForce, Random.Range(-10, 10)));
        package.GetComponent<Letter>().SetLetterPickup(this);
        package.GetComponent<LetterFlyToCar>().enabled = true;
        package.GetComponent<LetterFlyToCar>().SetCar(car);
        packagesInAir++;
    }

    public void PackagePickedUp()
    {
        packagesInAir--;
    }
}
