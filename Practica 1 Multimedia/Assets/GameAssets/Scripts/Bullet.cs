using System.Collections.Generic;
using UnityEngine;

// Luis Escolano Piquer
// Balas normales

public class Bullet : MonoBehaviour
{
    [SerializeField] List<string> ignoreTag = new();
    [SerializeField] float speed = 5;
    [SerializeField] int damage = 1;
    [SerializeField] GameObject hitParticle;

    void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ignores determined tags.
        for (int i = 0; i < ignoreTag.Count; i++)
            if (collision.gameObject.tag == ignoreTag[i])
                return;
        
        // Deal damage if collision is IDamageable.
        IDamageable obj = collision.gameObject.GetComponent<IDamageable>();
        if (obj != null)
            obj.GetDamage(damage);

        // Instantiate particle, destroy object.
        Destroy(Instantiate(hitParticle, this.transform.position, Quaternion.identity), 1);
        Destroy(gameObject);
    }
}
