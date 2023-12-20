using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRay : MonoBehaviour
{
    float speed = 30f;
    SpriteRenderer sren;
    float t;

    private void Start()
    {
        sren = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        t += Time.deltaTime;
        if (t > 0.4f)
            transform.Translate(- transform.up * Time.deltaTime * speed, Space.World);
        else
            sren.size = new Vector2(sren.size.x, sren.size.y + speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
