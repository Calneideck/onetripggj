using UnityEngine;
using System.Collections;

public class DepotDoor : MonoBehaviour
{
    public GameObject door;
    public Animator anim;

    private bool playing = false;
    private float startTime;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
            anim.SetBool("open", true);
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "Player")
            anim.SetBool("open", false);
    }
}
