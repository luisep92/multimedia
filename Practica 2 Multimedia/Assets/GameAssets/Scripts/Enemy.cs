using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private enum Target { WAYPOINT, PLAYER }
    [SerializeField] GameObject dieParticle;
    [SerializeField] Transform[] _waypoints;
    [SerializeField] float speed;
    [SerializeField] float detectPlayerDistance;
    [SerializeField] LayerMask layerPlayer;
    [SerializeField] Light lantern;
    private Vector3[] waypoints;  // Get position of waypoints at the beggining, so waypoints can be child of enemy
    private int _nextWaypoint = 0;
    private Transform player;
    private Target _targ = Target.WAYPOINT;

    private Target targ
    {
        get => _targ;
        set
        {
            if (_targ != value)
            {
                lantern.color = LightColor(value);
                if(value == Target.PLAYER)
                    GetComponent<AudioSource>().Play();
            }
            _targ = value;
        }
    }

    private int nextWaypoint
    {
        get { return _nextWaypoint; }
        set 
        { 
            if (value >= waypoints.Length)
                value = 0;
            _nextWaypoint = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        waypoints = _waypoints.Select(w => w.position).ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target;
        Vector3 playerPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        if (Vector3.Distance(transform.position, playerPos) < detectPlayerDistance)
        {
            Vector3 vecToPlayer = player.position - transform.position;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, vecToPlayer, out hit, detectPlayerDistance) && hit.collider.CompareTag("Player"))
            {
                target = playerPos;
                targ = Target.PLAYER;
            }
            else
            {
                target = waypoints[nextWaypoint];
                targ = Target.WAYPOINT;
            }
           
        }
        else
        {
            target = waypoints[nextWaypoint];
            targ = Target.WAYPOINT;
        }
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        transform.LookAt(target);
        if (Vector3.Distance(transform.position, waypoints[nextWaypoint]) < 0.2f)
            nextWaypoint++;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Vector3 direction = new Vector3(player.position.x, 0, player.position.z);
            direction = (direction - transform.position) * 5f;
            direction = direction + Vector3.up * 10f;
            Rigidbody rb = player.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(direction, ForceMode.Impulse);
            player.GetComponent<PlayerController>().GetDamage(1);
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        Vector3 pos = transform.position;
        pos.y -= transform.localScale.y / 2f;
        Destroy(Instantiate(dieParticle, pos, Quaternion.identity), 3f);
    }

    private Color LightColor(Target target)
    {
        return target == Target.PLAYER ? Color.red : Color.yellow;
    }
}
