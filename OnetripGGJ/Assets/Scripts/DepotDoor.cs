using UnityEngine;

public class DepotDoor : MonoBehaviour
{
    public Animator anim;

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
