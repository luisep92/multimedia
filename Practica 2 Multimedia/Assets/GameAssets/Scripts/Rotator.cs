using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 giro;

    private void Start()
    {
        giro = new Vector3(Random.value * 80, Random.value * 80, Random.value * 80);
    }

    void Update()
    {
        transform.Rotate(giro * Time.deltaTime);
    }
}

