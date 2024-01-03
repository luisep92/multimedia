using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class BossPhaseLaser : BossPhase
{
    private enum Action { DISAPPEARING, MOVING, APPEARING, ATTACKING }

    [SerializeField] Material defaultMaterial;
    [SerializeField] GameObject ray;
    private const float TIME_BLINK = 0.4f;
    private Action action = Action.MOVING;
    private Vector3 tempShake;
    private LineRenderer lren;
    private float t = TIME_BLINK;
    private bool aim = false;
    private int timesAttack;



    protected override void Start()
    {
        base.Start();
        sr = GetComponent<SpriteRenderer>();
        timesAttack = Random.Range(1, 4) + 1;
        StartCoroutine(Move());
    }

    protected void Update()
    {
        if (action == Action.DISAPPEARING)
            Disappear();
        else if (action == Action.APPEARING)
            Appear();

        if (aim)
            AimPlayer();
        if (tempShake != default)
            Shake();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        timesAttack = Random.Range(1, 4);
    }

    // Change alpha to 0. Then goes Action.MOVING.
    private void Disappear()
    {
        t -= Time.deltaTime;
        ChangeAlpha(Mathf.Lerp(1f, 0f, 1f - (t / TIME_BLINK)));
        if (t <= 0)
        {
            StartCoroutine(Move());
            t = TIME_BLINK;
        }
    }

    // Disable collider, move position, start aim. Next is Action.APPEARING.
    // THIS IS THE FIRST AND LAST EVENT
    private IEnumerator Move()
    {
        action = Action.MOVING;
        GetComponent<CircleCollider2D>().enabled = false;
        MovePosition();
        yield return new WaitForSeconds(TIME_BLINK);
        action = Action.APPEARING;
        sr.flipX = Player.Instance.transform.position.x > transform.position.x;
        GetComponent<CircleCollider2D>().enabled = true;
        aim = true;
        if (timesAttack-- <= 0)
        {
            action = Action.MOVING;
            this.enabled = false;
        }
    }

    private float MovePosition()
    {
        float x = Random.Range(-limit, limit);
        transform.position = new Vector3(x, 4.5f, 0);
        if(IsNear())
            x = MovePosition();
        return x;
    }

    // Alpha 0 to 1. Next is Action.ATTACKING.
    private void Appear()
    {
        t -= Time.deltaTime;
        ChangeAlpha(Mathf.Lerp(0f, 1f, 1f - (t / TIME_BLINK)));
        if (t <= 0)
        {
            action = Action.ATTACKING;
            t = TIME_BLINK;
            StartCoroutine(Attack());
        }
    }

    // Instantiate line renderer. Stop aim, instantiate ray. End of loop.
    private IEnumerator Attack()
    {
        lren = GetLine();
        yield return new WaitForSeconds(TIME_BLINK);
        tempShake = transform.position;
        aim = false;
        Destroy(lren);
        Instantiate(ray, this.transform.position, this.transform.rotation);
        yield return new WaitForSeconds(TIME_BLINK);
        transform.position = tempShake;
        tempShake = default;
        action = Action.DISAPPEARING;
    }

    private LineRenderer GetLine()
    {
        LineRenderer lren = gameObject.AddComponent<LineRenderer>();
        lren.material = defaultMaterial;
        lren.startWidth = 0.07f;
        lren.endWidth = 0.07f;
        lren.sortingOrder = 4;
        Color c = Color.red;
        c.a = 0.2f;
        lren.startColor = c;
        lren.endColor = c;
        Vector3 down = -transform.up.normalized;
        Vector3 endPoint = transform.position + down * (limit * 2);
        lren.positionCount = 2;
        lren.SetPosition(0, transform.position);
        lren.SetPosition(1, endPoint);
        return lren;
    }

    private void Shake()
    {
        float rx = Random.Range(-0.04f, 0.04f) + tempShake.x;
        float ry = Random.Range(-0.04f, 0.04f) + tempShake.y;
        transform.position = new Vector3(rx, ry, 0);
    }

    private void AimPlayer()
    {
        transform.up = transform.position - Player.Instance.transform.position;
        if (lren != null)
            lren.SetPosition(1, transform.position - transform.up * limit * 2);
    }

    private void OnEnable()
    {
        StartCoroutine(Move());
    }
}
