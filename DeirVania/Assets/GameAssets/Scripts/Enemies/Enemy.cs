using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;

public abstract class Enemy : MonoBehaviour
{
    public enum EnemyState { IDLE, PATROL, ATTACKING }
    

    public EnemyState enemyState;
    protected List<Vector2> waypoints = new();
    protected Vector2 nextWaypoint;
    protected Rigidbody2D rb;
    protected Player player;
    protected float speed;
    protected Animator anim;

    protected float VelX => Mathf.Abs(rb.velocity.x);


    protected abstract void Idle();

    protected abstract void Patrol();

    protected abstract void Attack();

    protected abstract void SetWaypoints();

    protected virtual void DetectPlayer()
    {

    }



    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        SetWaypoints();
        nextWaypoint = waypoints[0];
    }

    protected virtual void Start()
    {
        player = Player.Instance;
    }

    protected virtual void Update()
    {
        switch (enemyState)
        {
            case EnemyState.ATTACKING:
                Attack();
                break;
            case EnemyState.PATROL:
                Patrol();
                break;
            default:
                Idle();
                break;
        }
    }

    protected Vector2 GetNextWaypoint()
    {
        int index = waypoints.IndexOf(nextWaypoint);
        if (index == waypoints.Count - 1)
            return waypoints[0];
        return waypoints[index + 1];
    }

    public  Direction GetDirection(Vector2 objPosition)
    {
        if (objPosition.x < transform.position.x)
            return Direction.LEFT;
        return Direction.RIGHT;
    }

    public void LookAt(Direction dir)
    {
        Vector3 aux = transform.localScale;
        aux.x = (int)dir;
        transform.localScale = aux;
    }

   
}
