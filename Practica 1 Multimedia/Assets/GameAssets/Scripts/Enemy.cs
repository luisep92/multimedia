using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Luis Escolano Piquer
// Clase base de enemigo

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected GameObject dieParticle;
    [SerializeField] private int health = 3;
    [SerializeField] protected float speed = 7f;
    public AudioClip[] Sounds;
    protected AudioSource aSource;
    protected int points = 10;


    public int Health
    {
        get => health;
        private set
        {
            health = value;
            if (health <= 0)
                Die();
        }
    }

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    protected virtual void Start()
    {
        aSource = GetComponent<AudioSource>();
        speed = speed + GameManager.Instance.Difficulty;
    }

    protected abstract void Move();

    protected abstract void Attack();

    protected abstract void Die();

    public virtual void GetDamage(int damage)
    {
        Health -= damage;
        StartCoroutine(BlinkColor(0.1f, Color.red));
    }

    // Change color by specified time
    private IEnumerator BlinkColor(float time, Color color)
    {
        ChangeColor(color);
        yield return new WaitForSeconds(time);
        ChangeColor(Color.white);
    }

    protected void ChangeColor(Color c)
    {
        GetComponent<SpriteRenderer>().material.color = c;
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

    protected virtual void OnDestroy()
    {
        GameManager.Instance.Score += points;
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip == null || aSource == null)
            return;

        if (aSource.isActiveAndEnabled)
            aSource.PlayOneShot(clip);
    }
}
