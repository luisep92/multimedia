using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected GameObject dieParticle;
    [SerializeField] protected int health = 1;
    [SerializeField] protected float speed = 7f;

    protected abstract void Move();

    protected abstract void Attack();

    protected abstract void Die();

    public abstract void GetDamage(int damage);
}
