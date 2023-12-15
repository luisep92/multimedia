using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class Bullet : MonoBehaviour
{
    [SerializeField] List<string> ignoreTag = new();
    [SerializeField] float speed = 5;
    [SerializeField] int damage = 1;

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
        for (int i = 0; i < ignoreTag.Count; i++)
            if (collision.gameObject.tag == ignoreTag[i])
                return;

        IDamageable obj = collision.gameObject.GetComponent<IDamageable>();
        if (obj != null)
            obj.GetDamage(damage);
        Destroy(gameObject);
    }
}
