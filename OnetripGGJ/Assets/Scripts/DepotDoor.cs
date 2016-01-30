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
        {
            anim.SetBool("open", true);
            playing = true;
            startTime = Time.time;
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "Player")
        {
            anim.SetBool("open", false);
            playing = false;
            anim.speed = 1;
        }
    }

    void Update()
    {
        if (playing)
        {
            if (Time.time - startTime >= 1)
                anim.speed = 0;
        }
    }
}
