using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Luis Escolano Piquer
// Enemigo jefe

public class EnemyBoss : Enemy
{
    private enum States { IDLE, ATTACKING }

    [SerializeField] private UIBoss UI;
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
        GameManager.Instance.Score += 300;
        Destroy(Instantiate(dieParticle, transform.position, transform.rotation), 3);
        levelManager.bosses.Remove(this);
        if (UI != null)
            UI.OnDie();
        Destroy(gameObject);
    }

    protected override void Move()
    {
        
    }

    public override void GetDamage(int damage)
    {
        PlaySound(Sounds[0]);
        base.GetDamage(damage);
        if (UI != null)
            UI.OnDamage(Health);
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
