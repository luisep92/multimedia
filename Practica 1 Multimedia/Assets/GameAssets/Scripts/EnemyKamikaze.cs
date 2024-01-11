using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKamikaze : Enemy
{
    public enum State { SEARCHING, MOVING }

    [SerializeField] GameObject crosshair;
    private Transform target;
    private State state = State.SEARCHING;

    State CState
    {
        get => state;
        set
        {
            state = value;
            if (value == State.MOVING)
                StartCoroutine(MoveSound());
        }
    }

    protected override void Start()
    {
        base.Start();
        points = 15;
        SetCrosshair();
    }


    void Update()
    {
        Attack();

        if(CState == State.MOVING)
            Move();
    }

    // Reduces health
    public override void GetDamage(int damage)
    {
        PlaySound(Sounds[1]);
        base.GetDamage(damage);
    }

    // Look at target
    protected override void Attack()
    {
        transform.up = target.position - transform.position;
    }

    // Moves to target
    protected override void Move()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
    }

    // Instantiate particle, destroy object
    protected override void Die()
    {
        if (CState == State.SEARCHING)
            Destroy(target.gameObject);
        Destroy(Instantiate(dieParticle, transform.position, Quaternion.identity), 3);
        Destroy(gameObject);
    }

    // May change this to Notify(), targets player and starts moving.
    public void SetTarget()
    {
        target = Player.Instance.transform;
        CState = State.MOVING;
    }

    // Instantiates crosshair
    private void SetCrosshair()
    {
        Vector3 chPos = new Vector3(transform.position.x, transform.position.y - 0.1f);
        target = Instantiate(crosshair, chPos, Quaternion.identity).transform;
        target.GetComponent<Crosshair>().parent = this;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Deal damage if collision is IDamageable.
        IDamageable obj = collision.gameObject.GetComponent<IDamageable>();
        if (obj != null)
            obj.GetDamage(2);
        if (obj is Player)
            GetDamage(Health);
    } 

    private IEnumerator MoveSound()
    {
        PlaySound(Sounds[0]);
        float t = aSource.clip.length;
        yield return new WaitForSeconds(t);
        StartCoroutine(MoveSound());
    }
}
