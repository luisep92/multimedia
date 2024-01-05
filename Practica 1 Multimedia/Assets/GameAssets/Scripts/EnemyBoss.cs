using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;


public class EnemyBoss : Enemy
{
    public List<Transform> others;
    private BossPhase[] attacks;
   

    protected override void Start()
    {
        base.Start();
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
}
