using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;


public class EnemyBoss : Enemy
{
    private enum States { IDLE, ATTACKING }

    public List<Transform> others;
    private States _state = States.IDLE;
    private BossPhase[] attacks;

    private States State
    {
        get => _state;
        set
        {
            if (value == States.ATTACKING)
                ChangeAttack(0f);
            _state = value;
        }
    }

    protected override void Start()
    {
        base.Start();
        attacks = GetComponents<BossPhase>();
        Health = 20;
        //StartCoroutine(StartAnimation());

    }

    protected override void Attack()
    {
        
    }

    protected override void Die()
    {
        Destroy(Instantiate(dieParticle, transform.position, transform.rotation), 3);
        Destroy(gameObject);
    }

    protected override void Move()
    {
        
    }

    public override void GetDamage(int damage)
    {
        aSource.PlayOneShot(sounds[0]);
        base.GetDamage(damage);
    }

    public void ChangeAttack(float t)
    {
        if (isActiveAndEnabled)
            StartCoroutine(ChangeCoroutine(t));
    }

    private IEnumerator ChangeCoroutine(float t)
    {
        yield return new WaitForSeconds(t);
        int n = Random.Range(0, attacks.Length);
        attacks[n].enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player p = Player.Instance;
            p.GetDamage(1);
            Vector3 direction = transform.position - p.transform.position;
            float dirX = direction.x < 0 ? 1 : -1;
            float dirY = direction.y > 0 ? 1 : -1;
            p.StartCoroutine(p.Dash(dirX, dirY));
        }     
    }

    protected override void OnDestroy()
    {
        foreach (Transform t in others)
        {
            t.GetComponent<EnemyBoss>().others.Remove(this.transform);
        }
    }

    private IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(6f);
        State = States.ATTACKING;
    }


    public void SetAttackState()
    {
        GetComponent<Animator>().enabled = false;
        State = States.ATTACKING;
    }
}
