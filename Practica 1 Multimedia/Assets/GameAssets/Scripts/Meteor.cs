using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Luis Escolano Piquer
// Controlador del meteorito

public class Meteor : MonoBehaviour
{
    float limit = 10f;
    float speed = 8;
    float limitY = -6;
    float startY = 6.3f;
    Vector3 direction;

    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(-limit, limit), startY, 0);
        float x = Random.Range(-limit + 2, limit - 2);
        float y = Player.Instance.transform.position.y;
        direction = new Vector3(x, y, 0);
        direction = (direction - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 50) * Time.deltaTime);
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        if (transform.position.y < limitY)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.gameObject.GetComponent<IDamageable>().GetDamage(1);
    }
}
