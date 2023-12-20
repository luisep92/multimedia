using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : Enemy
{
    [SerializeField] GameObject bullet;
    protected float xMin = -8.75f;
    protected float xMax = 8.75f;

    private void Start()
    {
        Attack();
    }

    private void Update()
    {
        Move();
    }

    public override void GetDamage(int damage)
    {
        base.GetDamage(damage);
    }

    protected override void Die()
    {
        Vector3 shootPos = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        Destroy(Instantiate(dieParticle, shootPos, Quaternion.identity), 3);
        Destroy(gameObject);
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(Random.Range(0.3f, 2f));
        Instantiate(bullet, transform.position, transform.rotation);
        StartCoroutine(Shoot());
    }

    protected override void Move()
    {
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));

        if (transform.position.x <= xMin || transform.position.x >= xMax)
            speed *= -1;
    }

    protected override void Attack()
    {
        StartCoroutine(Shoot());
    }
}
