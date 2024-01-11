using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Luis Escolano Piquer
// Enemigo, el que dispara

public class EnemyShooter : Enemy
{
    [SerializeField] GameObject bullet;
    protected float xMin = -8.75f;
    protected float xMax = 8.75f;
    private float yMin = -4.8f;
    private bool canShoot = true;

    protected override void Start()
    {
        base.Start();
        speed = speed * ( 1 + GameManager.Instance.GetWave() * 0.3f);
        Attack();
    }

    private void Update()
    {
        Move();
    }

    public override void GetDamage(int damage)
    {
        PlaySound(Sounds[1]);
        base.GetDamage(damage);
    }

    protected override void Die()
    {
        Vector3 shootPos = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        Destroy(Instantiate(dieParticle, shootPos, Quaternion.identity), 3);
        Destroy(gameObject);
    }

    // Shoot between 0.3-2 seconds
    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(Random.Range(0.3f, 2f));
        if(canShoot)
            Instantiate(bullet, transform.position, transform.rotation);
        PlaySound(Sounds[0]);
        StartCoroutine(Shoot());
    }

    protected override void Move()
    {
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));

        if ((transform.position.x <= xMin && speed > 0) || (transform.position.x >= xMax && speed < 0))
        {
            speed *= -1;
            transform.Translate(new Vector3(0, 1f, 0));
        }
    }

    protected override void Attack()
    {
        StartCoroutine(Shoot());
    }

    private void OnBecameInvisible()
    {
        if (transform.position.y > yMin)
            return;
        Player.Instance.GetDamage(100);
        Destroy(gameObject);
    }
}
