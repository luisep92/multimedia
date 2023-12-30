using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{
    private void Start()
    {
        Health = 20;
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
    }

    public void NotifyIdleState()
    {

    }
}
