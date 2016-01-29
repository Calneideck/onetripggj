using UnityEngine;
using System.Collections;

public class Letter : MonoBehaviour
{
    public float removeTime;

    private bool removing = false;

    void OnCollisionEnter(Collision coll)
    {
        if (!removing)
        {
            removing = true;
            Invoke("Remove", removeTime);
        }
        if (coll.collider.tag == "Player")
            Remove();
    }

    void Remove()
    {
        GameObject.Destroy(gameObject);
    }
}
