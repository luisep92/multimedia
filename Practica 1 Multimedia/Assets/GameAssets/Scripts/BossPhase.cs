using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BossPhase : MonoBehaviour
{
    public enum BossState { IDLE, ATTACKING }

    private IEnumerable others;
    protected EnemyBoss boss;
    protected SpriteRenderer sr;
    protected float limit = 7.35f;
    private BossState state;

    public BossState State { get; set; }


    protected virtual void Start()
    {
        boss = GetComponent<EnemyBoss>();
        sr = GetComponent<SpriteRenderer>();
        others = boss.others;
    }

    protected virtual void OnDisable()
    {
        boss.ChangeAttack(Random.Range(0f, 1f));
    }

    // Change alpha.
    protected void ChangeAlpha(float value)
    {
        Color myNewColor = sr.color;
        myNewColor.a = value;
        sr.color = myNewColor;
    }

    protected bool IsNear()
    {
        if (others == null)
            return false;

        foreach (GameObject go in others)
        {
            if (go != null && go.activeInHierarchy && Vector2.Distance(transform.position, go.transform.position) < 1.5f)
                return true;
        }
        return false;
    }
}
