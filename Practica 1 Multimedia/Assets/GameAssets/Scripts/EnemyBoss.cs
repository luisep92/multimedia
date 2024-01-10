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
    private Level2Manager levelManager;

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
        levelManager = FindObjectOfType<Level2Manager>();
    }

    protected override void Attack()
    {
        
    }

    protected override void Die()
    {
        Destroy(Instantiate(dieParticle, transform.position, transform.rotation), 3);
        levelManager.bosses.Remove(this);
        Destroy(gameObject);
    }

    protected override void Move()
    {
        
    }

    public override void GetDamage(int damage)
    {
        PlaySound(sounds[0]);
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

    protected override void OnDestroy()
    {
        foreach (Transform t in others)
        {
            t.GetComponent<EnemyBoss>().others.Remove(this.transform);
        }
    }

    public void SetAttackState()
    {
        State = States.ATTACKING;
        GetComponent<Animator>().enabled = false;
    }
}
