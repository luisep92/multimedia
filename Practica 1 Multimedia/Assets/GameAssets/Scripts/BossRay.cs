using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRay : MonoBehaviour
{
    private float speed = 30f;
    private float t;
    private SpriteRenderer sren;
    private PolygonCollider2D col;

    private void Start()
    {
        sren = GetComponent<SpriteRenderer>();
        col = GetComponent<PolygonCollider2D>();
    }


    void Update()
    {
        t += Time.deltaTime;
        if (t > 0.4f)
        {
            // Ray moves
            float mod = Time.deltaTime * 3f;
            transform.Translate(-transform.up * Time.deltaTime * speed, Space.World);
            if (sren.size.x > 0)
                sren.size = new Vector2(sren.size.x - mod, sren.size.y);
            // Adjust collider
            Vector2 p0 = new(col.points[0].x + (mod/2), col.points[0].y);
            Vector2 p1 = new(col.points[1].x + (mod/2), col.points[1].y);
            Vector2 p2 = new(col.points[2].x - (mod/2), col.points[2].y);
            Vector2 p3 = new(col.points[3].x - (mod/2), col.points[3].y);
            Vector2[] points = { p0, p1, p2, p3 };
            col.SetPath(0, points);
        }
        else
        {
            // Ray grows up
            float mod = speed * Time.deltaTime;
            sren.size = new Vector2(sren.size.x, sren.size.y + mod);
            // Adjust collider
            Vector2 p1 = new(col.points[1].x, col.points[1].y - mod);
            Vector2 p2 = new(col.points[2].x, col.points[2].y - mod);
            Vector2[] points = { col.points[0], p1, p2, col.points[3] };
            col.SetPath(0, points);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            Player.Instance.GetDamage(2);
    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
