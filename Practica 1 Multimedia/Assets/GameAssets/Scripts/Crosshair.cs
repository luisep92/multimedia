using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Luis Escolano Piquer
// Marca que lanzan los enemigos kamikaze

public class Crosshair : MonoBehaviour
{
    enum State { MOVING, TARGETED }

    [HideInInspector] public EnemyKamikaze parent;
    private State state = State.MOVING;
    private Transform target;
    private Material material;
    private AudioSource aSource;
    private float speed = 5f;
    private float timeToVanish = 1f;
    private float t = 0f;

    private State CState
    {
        get => state;
        set { 
            state = value;
            if (value == State.TARGETED)
            {
                aSource.pitch = 2f;
                aSource.Play();
            }
        }
    }

    private void Start()
    {
        target = Player.Instance.transform;
        material = GetComponent<SpriteRenderer>().material;
        aSource = GetComponent<AudioSource>();

        StartCoroutine(BipSound());
    }

    void Update()
    {
        if (CState == State.MOVING)
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
            CState = State.TARGETED;
    }

    // Sounds every 0.8 seconds
    private IEnumerator BipSound(float cooldown = 0.8f)
    {
        aSource.Play();
        yield return new WaitForSeconds(cooldown);
        if (CState == State.MOVING)
            StartCoroutine(BipSound());
    }
}
