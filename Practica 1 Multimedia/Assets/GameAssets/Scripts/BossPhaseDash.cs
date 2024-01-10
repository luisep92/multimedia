using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossPhaseDash : BossPhase
{
    [SerializeField] GameObject appearParticle;
    private TrailRenderer tRen;
    private CircleCollider2D col;
    private float direction = 0;
    private float speed;
    float t = 0f;


    protected override void Awake()
    {
        base.Awake();
        tRen = GetComponent<TrailRenderer>();
        col = GetComponent<CircleCollider2D>();
        speed = boss.Speed;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        col.enabled = true;
        if (t > 0.8f)
            transform.Translate(new Vector3(speed * direction * Time.deltaTime, 0, 0), Space.World);
        else
            ChangeAlpha(Mathf.Lerp(0, 1, t / 0.8f));

        t += Time.deltaTime;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(Attack());
    }


    protected override void OnDisable()
    {
        base.OnDisable();
    }

    private void OnBecameVisible()
    {
        tRen.time = 0;
        tRen.emitting = false;
    }


    private void Appear()
    {
        MovePosition();
        tRen.emitting = false;
        Destroy(Instantiate(appearParticle, transform.position, Quaternion.identity), 2f);
    }

    private void MovePosition(int n = 20)
    {
        bool onlyPlayer = (n - 1) <= 0;
        n--;
        float r = Random.Range(0f, 1f) >= 0.5f ? -1 : 1;
        float x = -r * limit;
        float y = Player.Instance.transform.position.y;
        transform.position = new Vector3(x, y, 0);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 45 * -r));
        direction = r;
        
        if (IsNear(onlyPlayer))
            MovePosition(n);
    }

    IEnumerator Attack()
    {
        Appear();
        yield return new WaitForSeconds(0.8f);
        tRen.time = 0.2f;
        tRen.emitting = true;
        yield return new WaitForSeconds(1.2f);
        t = 0;
        enabled = false;
    }
}
