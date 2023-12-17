using UnityEngine;

public class EnemyKamikaze : Enemy
{
    public enum State { SEARCHING, MOVING }

    [SerializeField] GameObject crosshair;
    Transform target;
    State state = State.SEARCHING;


    void Start()
    {
        SetCrosshair();
    }


    void Update()
    {
        Attack();

        if(state == State.MOVING)
            Move();

        if(health <= 0)
            Die();
    }

    // Reduces health
    public override void GetDamage(int damage)
    {
        health -= damage;
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
        if (state == State.SEARCHING)
            Destroy(target.gameObject);
        Destroy(Instantiate(dieParticle, transform.position, Quaternion.identity), 3);
        Destroy(gameObject);
    }

    // May change this to Notify(), targets player and starts moving.
    public void SetTarget()
    {
        target = Player.Instance.transform;
        state = State.MOVING;
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
        GetDamage(1);
        // Deal damage if collision is IDamageable.
        IDamageable obj = collision.gameObject.GetComponent<IDamageable>();
        if (obj != null)
            obj.GetDamage(1);
    } 
}
