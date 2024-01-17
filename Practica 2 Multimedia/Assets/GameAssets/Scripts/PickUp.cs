using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] GameObject particle;
    public void OnPickUp()
    {
        GetComponent<Light>().color = Color.green;
        GetComponent<Rotator>().enabled = false;
        particle.SetActive(false);
    }
}
