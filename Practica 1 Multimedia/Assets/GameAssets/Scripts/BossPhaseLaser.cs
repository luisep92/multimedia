using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class BossPhaseLaser : BossPhase
{
    private enum Action { DISAPPEARING, MOVING, APPEARING, ATTACKING }

    [SerializeField] Material defaultMaterial;
    [SerializeField] GameObject ray;
    private const float TIME_BLINK = 0.5f;
    private Action action;
    private SpriteRenderer sr;
    private float t = TIME_BLINK;
    private float limit = 7.35f;
    private Vector3 tempShake;
    private bool aim = false;
    private LineRenderer lren;


    protected override void Start()
    {
        base.Start();
        sr = GetComponent<SpriteRenderer>();
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
    private IEnumerator Move()
    {
        action = Action.MOVING;
        GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(TIME_BLINK);
        action = Action.APPEARING;
        float x = Random.Range(-limit, limit);
        transform.position = new Vector3(x, transform.position.y, 0);
        sr.flipX = Player.Instance.transform.position.x > transform.position.x;
        GetComponent<CircleCollider2D>().enabled = true;
        aim = true;
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

    // Change alpha.
    private void ChangeAlpha(float value)
    {
        Color myNewColor = sr.color;
        myNewColor.a = value;
        sr.color = myNewColor;
    }

    private LineRenderer GetLine()
    {
        LineRenderer lren = gameObject.AddComponent<LineRenderer>();
        lren.material = defaultMaterial;
        lren.startWidth = 0.07f;
        lren.endWidth = 0.07f;
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
}
