using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected GameObject dieParticle;
    [SerializeField] private int health = 3;
    [SerializeField] protected float speed = 7f;
    protected int points = 10;

    protected int Health
    {
        get => health;
        set
        {
            health = value;
            if (health <= 0)
                Die();
        }
    }

    protected abstract void Move();

    protected abstract void Attack();

    protected abstract void Die();

    public virtual void GetDamage(int damage)
    {
        Health -= damage;
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

    protected void OnDestroy()
    {
        GameManager.Instance.Score += points;
    }
}
