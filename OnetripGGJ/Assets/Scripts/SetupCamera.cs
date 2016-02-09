using UnityEngine;
using System.Collections;

public class SetupCamera : MonoBehaviour
{
    void Start()
    {
        Canvas canvas = GameObject.Find("UI").GetComponent<Canvas>();

        canvas.transform.Find("MenuPanel").gameObject.SetActive(true);
        canvas.GetComponent<PlayMusic>().PlaySelectedMusic(0);
        canvas.worldCamera = GetComponent<Camera>();
        canvas.GetComponent<StartOptions>().inMainMenu = true;
    }
}
