using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhaseDash : BossPhase
{
    [SerializeField] GameObject appearParticle;
    private TrailRenderer tRen;
    private float direction = 0;
    private float speed;
    float t = 0f;


    private void Awake()
    {
        tRen = GetComponent<TrailRenderer>();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        speed = GetComponent<Enemy>().Speed;
    }

    private void Update()
    {
        if (t > 0.8f)
            transform.Translate(new Vector3(speed * direction * Time.deltaTime, 0, 0), Space.World);
        else
            ChangeAlpha(Mathf.Lerp(0, 1, t / 0.8f));

        t += Time.deltaTime;
    }

    private void OnEnable()
    {
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
        float r = Random.Range(0f, 1f) >= 0.5f ? -1 : 1;
        float x = -r * limit;
        float y = Player.Instance.transform.position.y;
        transform.position = new Vector3( x, y, 0 );
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 45 * -r));
        direction = r;
        tRen.emitting = false;
        Instantiate(appearParticle, transform.position, Quaternion.identity);
    }

    IEnumerator Attack()
    {
        while (IsNear())
            yield return null;
        Appear();
        yield return new WaitForSeconds(0.8f);
        tRen.time = 0.2f;
        tRen.emitting = true;
        yield return new WaitForSeconds(1.2f);
        t = 0;
        enabled = false;
    }
}
