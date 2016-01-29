using UnityEngine;
using System.Collections;

public class LetterPickup : MonoBehaviour
{
    public GameObject letterPrefab;
    public Transform letterPickup;
    public float pickupRange;
    public float pickupInterval;
    public float letterLaunchForce;

    private float lastPickupTime;

    void Start ()
    {
        lastPickupTime = Time.time;
	}
	
	void Update ()
    {
        if (Vector3.Distance(transform.position, letterPickup.position) <= pickupRange)
        {
            if (Time.time > lastPickupTime + pickupInterval)
            {
                lastPickupTime = Time.time;
                SpawnLetter();
            }
        }
    }

    void SpawnLetter()
    {
        GameObject letter = (GameObject)GameObject.Instantiate(letterPrefab, letterPickup.position + Vector3.up, Random.rotation);
        letter.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-10, 10), letterLaunchForce, Random.Range(-10, 10)));
        letter.GetComponent<LetterFlyToCar>().enabled = true;
        letter.GetComponent<LetterFlyToCar>().SetCar(transform);
    }
}
