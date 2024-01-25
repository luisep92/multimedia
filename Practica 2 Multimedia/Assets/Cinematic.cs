using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematic : MonoBehaviour
{
    CameraController c; 


    void Start()
    {
        c = GetComponent<CameraController>();
        StartCoroutine(StartCinematic());
    }

    private IEnumerator StartCinematic()
    {
        c.enabled = false;
        var player = FindObjectOfType<PlayerController>();
        player.Disable();
        yield return new WaitForSeconds(1f);
        GetComponent<Animator>().Play("Cinematic");
        yield return new WaitForSeconds(5f);
        player.Enable();
        c.enabled = true;
    }
}
