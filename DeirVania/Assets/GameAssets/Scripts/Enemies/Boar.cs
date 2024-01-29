using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Boar : Enemy
{
    const float BOAR_PATROL_SPEED = 1.5f;
    const float BOAR_ATTACK_SPEED = 8f;
    const float BOAR_SPEED = 1000;

    [SerializeField]
    float waypointDistance;

    protected override void Awake()
    {
        base.Awake();
        speed = BOAR_SPEED;
    }


    protected override void Update()
    {
        base.Update();
        anim.SetFloat("VelX", VelX);
    }


    protected override void Attack()
    {
        LookAt(GetDirection(player.transform.position));
        if (VelX < BOAR_ATTACK_SPEED)
            Move(BOAR_ATTACK_SPEED);

        if (Vector2.Distance(player.transform.position, transform.position) > 10)
            enemyState = EnemyState.PATROL;
    }

    protected override void Idle()
    {
        
    }

    protected void Move(float maxSpeed)
    {
        if(VelX < maxSpeed)
            rb.AddForce(Vector2.right * transform.localScale.x * speed * Time.deltaTime, ForceMode2D.Force);
    }


    protected override void Patrol()
    {
        Move(BOAR_PATROL_SPEED);

        if(Vector2.Distance(transform.position, nextWaypoint) < 1f)
        {
            nextWaypoint = GetNextWaypoint();
            LookAt(GetDirection(nextWaypoint));
        }

        if(Vector2.Distance(player.transform.position, transform.position) < 5)
            enemyState = EnemyState.ATTACKING;
    }
    
    
    protected override void SetWaypoints()
    {
        Vector2 rightWP = new Vector2(transform.position.x + waypointDistance, transform.position.y);
        Vector2 leftWP = new Vector2(transform.position.x - waypointDistance, transform.position.y);
        waypoints.Add(rightWP);
        waypoints.Add(leftWP);
    }
}
