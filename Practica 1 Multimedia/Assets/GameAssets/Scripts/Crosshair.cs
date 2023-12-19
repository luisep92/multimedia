using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    enum State { MOVING, TARGETED }

    [HideInInspector] public EnemyKamikaze parent;
    State state = State.MOVING;
    float speed = 5f;
    float timeToVanish = 1f;
    Transform target;
    Material material;
    float t = 0f;

    private void Start()
    {
        target = Player.Instance.transform;
        material = GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        if (state == State.MOVING)
           ChaseTarget();
        else
        {
            transform.position = target.position;
            LerpAlpha();
            
            if (material.color.a == 0f)
                End();
        }
    }

    // Lerp alpha of sprite form 1 to 0
    private void LerpAlpha()
    {
        float alpha = Mathf.Lerp(1f, 0f, t / timeToVanish);
        Color nextColor = material.color;
        nextColor.a = alpha;
        material.color = nextColor;
        t += Time.deltaTime;
    }

    // Notifies parent that target is set
    private void End()
    {
        parent.SetTarget();
        parent = null;
        Destroy(gameObject);
    }

    // Chases target. When it's enough close changes state.
    private void ChaseTarget()
    {
        transform.Translate((target.position - transform.position).normalized * speed * Time.deltaTime);
        speed += Time.deltaTime;
        if (Vector2.Distance(target.position, transform.position) < 0.1f)
            state = State.TARGETED;
    }
}
