using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyKamikaze : Enemy
{
    Transform player;

    void Start()
    {
        player = Player.Instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        Move();

        if(health <= 0)
            Die();
    }


    public override void GetDamage(int damage)
    {
        health -= damage;
    }

    protected override void Attack()
    {
        transform.up = player.position - transform.position;
    }

    protected override void Die()
    {
        Destroy(Instantiate(dieParticle, transform.position, Quaternion.identity), 3);
        Destroy(gameObject);
    }

    protected override void Move()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        health -= 1;
    }
}
