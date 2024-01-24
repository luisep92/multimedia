using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    Transform player;
    Renderer ren;


    private bool isBehindPlayer => (transform.position.z + (transform.localScale.z / 2f) < player.position.z);

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        ren = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBehindPlayer)
            SetAlpha(0.15f);
        else
            SetAlpha(1f);

    }

    private void SetAlpha(float a)
    {
        if (ren.material.color.a == a)
            return;
        Color c = ren.material.color;
        c.a = a;
        ren.material.color = c;
    }
}
