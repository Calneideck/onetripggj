using UnityEngine;
using System.Collections;

public class Letter : MonoBehaviour
{
    public float removeTime;

    private bool removing = false;

    void OnCollisionEnter(Collision coll)
    {
        // wait for 'removetime' seconds before removing the letter when it collides with the world
        if (!removing)
        {
            removing = true;
            Invoke("Remove", removeTime);
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            coll.GetComponent<Car>().ReceivedLetter();
            Remove();
        }
    }

    void Remove()
    {
        GameObject.Destroy(gameObject);
    }
}
