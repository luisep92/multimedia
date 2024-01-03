using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;


public class EnemyBoss : Enemy
{
    private BossPhase[] attacks;
    [HideInInspector] public IEnumerable others;

    protected override void Start()
    {
        base.Start();
        others = from go in FindObjectsOfType<EnemyBoss>()
                 where go.gameObject != this.gameObject
                 select go.gameObject;
        attacks = GetComponents<BossPhase>();
        Health = 20;
        ChangeAttack(0f);
    }

    protected override void Attack()
    {
        
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }

    protected override void Move()
    {
        
    }

    public override void GetDamage(int damage)
    {
        base.GetDamage(damage);
        aSource.PlayOneShot(sounds[0]);
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
}
