using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

// Luis Escolano Piquer
// Base de ataque del jefe

public abstract class BossPhase : MonoBehaviour
{
    public enum BossState { IDLE, ATTACKING }

    [HideInInspector] private List<Transform> others;
    protected EnemyBoss boss;
    protected SpriteRenderer sRen;
    protected float limit = 9f;
    private BossState state;
    private Transform _player;

    private Transform player
    {
        get => _player != null ? _player : Player.Instance.transform;
        set => _player = value;
    }

    public BossState State { get; set; }

    protected virtual void Awake()
    {
        boss = GetComponent<EnemyBoss>();
        sRen = GetComponent<SpriteRenderer>();
        others = boss.others;
    }

    protected virtual void Start()
    {

    }

    protected virtual void OnDisable()
    {
        boss.ChangeAttack(Random.Range(0f, 1f));
    }

    // Change alpha.
    protected void ChangeAlpha(float value)
    {
        Color myNewColor = sRen.color;
        myNewColor.a = value;
        sRen.color = myNewColor;
    }

    // Check if there are other bosses or player near
    protected bool IsNear(bool onlyPlayer = false)
    {
        if (Vector2.Distance(transform.position, player.position) < 1.5f)
            return true;

        if (onlyPlayer)
            return false;

        if (others == null)
            return false;

        foreach (Transform go in others)
        {
            if (go != null && go.gameObject.activeInHierarchy && Vector2.Distance(transform.position, go.transform.position) < 1.5f)
                return true;
        }

        return false;
    }

    protected virtual void OnEnable() 
    {
    
    }
}

