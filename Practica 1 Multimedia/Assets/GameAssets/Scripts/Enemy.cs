using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected GameObject dieParticle;
    [SerializeField] protected int health = 3;
    [SerializeField] protected float speed = 7f;

    protected abstract void Move();

    protected abstract void Attack();

    protected abstract void Die();

    public virtual void GetDamage(int damage)
    {
        StartCoroutine(BlinkRed(0.1f));
    }

    private IEnumerator BlinkRed(float time)
    {
        ChangeColor(Color.red);
        yield return new WaitForSeconds(time);
        ChangeColor(Color.white);
    }

    protected void ChangeColor(Color c)
    {
        GetComponent<SpriteRenderer>().material.color = c;
    }
}
