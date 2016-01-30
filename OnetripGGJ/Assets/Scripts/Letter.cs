using UnityEngine;
using System.Collections;

public class Letter : MonoBehaviour
{
    public float removeTime;

    private bool removing = false;
    private GameObject car;
    private HouseStruct packageInfo;
    private bool delivered = false;

    public void Setup(GameObject car, HouseStruct packageInfo)
    {
        this.car = car;
        this.packageInfo = packageInfo;
    }

    void OnCollisionEnter(Collision coll)
    {
        // wait for 'removetime' seconds before removing the letter when it collides with the world
        if (!removing)
        {
            removing = true;
            Invoke("Remove", removeTime);
        }
    }

    // This is for when letters are being picked up at the depot
    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            coll.GetComponent<Car>().ReceivedLetter();
            Remove();
        }

        // If the letter hits the right house, increase the score
        if (coll.tag == "House")
        {
            if (packageInfo == coll.GetComponent<House>().HouseInfo)
            {
                if (!delivered)
                {
                    car.GetComponent<Car>().PackageDelivered();
                    delivered = true;
                }
            }
        }
    }

    void Remove()
    {
        GameObject.Destroy(gameObject);
    }
}
