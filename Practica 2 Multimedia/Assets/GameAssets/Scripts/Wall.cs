using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Luis Escolano Piquer

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


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ChangeWallSize();
            ChangePlayerSize(collision);
        }
    }

    private void ChangeWallSize()
    {
        Transform t = transform;
        float size = t.localScale.y;
        if (size <= 2f)
            return;
        t.localScale = new Vector3(t.localScale.x, t.localScale.y - 1, t.localScale.z);
        t.position = new Vector3(t.position.x, t.position.y - 0.5f, t.position.z);
    }

    private void ChangePlayerSize(Collision col)
    {
        Transform p = col.transform;
        float scale = p.localScale.x;
        if (scale > 0.6f && transform.rotation.y == 0)
            p.localScale = new Vector3(scale - 0.2f, scale - 0.2f, scale - 0.2f);
        else if (scale < 1.4f && transform.rotation.y != 0)
            p.localScale = new Vector3(scale + 0.2f, scale + 0.2f, scale + 0.2f);
    }
}
