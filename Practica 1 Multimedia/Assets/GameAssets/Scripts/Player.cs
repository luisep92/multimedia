using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    private enum State { IDLE, MOVING, DASHING }

    public static Player Instance;

    [SerializeField] float speed;
    [SerializeField] GameObject bullet;
    [SerializeField] AudioClip[] sounds;
    private Camera mainCamera;
    private State state = State.MOVING;
    private bool godMode;
    private bool canDash = true;
    private Rigidbody2D rb;
    private int maxHealth = 5;
    private int currentHealth = 5;
    private List<GameObject> UIHealth = new();
    private TrailRenderer tren;
    private AudioSource aSource;


    private int CurrentHealth
    {
        get => currentHealth;
        set
        {
            currentHealth = value;
            if (value <= 0)
                Die();
        }
    }

    private bool GodMode
    {
        get { return godMode; }
        set 
        { 
            godMode = value; 
            if (value == false) 
                ChangePlayerAlpha(1); 
        }
    }

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);
        Instance = this;

        tren = GetComponent<TrailRenderer>();
        aSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        foreach (Transform t in GameObject.Find("Content").transform)
            UIHealth.Add(t.gameObject);
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        Shoot();

        if (godMode)
            Blink();

        if (Input.GetButtonDown("Dash") && canDash)
            StartCoroutine(Dash());
    }

    // Move here because it's done by rigidbody.
    private void FixedUpdate()
    {
        if (state == State.MOVING)
            Move();
    }

    // Player shoot.
    void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bullet, new Vector3(transform.position.x, transform.position.y + 0.5f, 0), Quaternion.identity);
            aSource.PlayOneShot(sounds[1]);
        }
    }

    // Player movement.
    private void Move()
    {
        var horizontal = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        var vertical = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
        rb.MovePosition(new Vector2(transform.position.x + horizontal, transform.position.y + vertical));
    }

    // If not godmode receives damage, update UI, enter godmode.
    public void GetDamage(int damage)
    {
        if (GodMode)
            return;

        aSource.PlayOneShot(sounds[0]);
        StartCoroutine(Shake(0.1f, 30f));
        for (int i = 0; i < damage; i++)
        {
            if(CurrentHealth > 0)
            {
                CurrentHealth -= 1;
                UIHealth[maxHealth - CurrentHealth - 1].SetActive(false);
            }
        }
        StartCoroutine(EnterGodMode(1f));
    }

    private void Die()
    {
        GameManager.Instance.IsPlayerAlive = false;
        SceneManager.LoadScene("WinLose");
    }

    // Character alpha changes between 0.2 - 1 on time.
    void Blink()
    {
        ChangePlayerAlpha(Mathf.PingPong(Time.time * 7, 1) + 0.2f);
    }

    // Change player alpha.
    void ChangePlayerAlpha(float value)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color myNewColor = sr.color;
        myNewColor.a = value;
        sr.color = myNewColor;
    }

    // Activate god mode for determined time.
    IEnumerator EnterGodMode(float time)
    {
        GodMode = true;
        yield return new WaitForSeconds(time);
        GodMode = false;
    }

    private IEnumerator Dash()
    {
        state = State.DASHING;
        canDash = false;
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(horizontal * 15f, vertical * 15f);
        tren.emitting = true;
        aSource.PlayOneShot(sounds[2]);
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(VanishTrail(0f));
        rb.velocity = Vector2.zero;
        state = State.MOVING;
        yield return new WaitForSeconds(0.2f);
        canDash = true;
    }

    private IEnumerator VanishTrail(float t)
    {
        if(t >= 0.2f)
        {
            tren.time = 0.10f;
            tren.emitting = false;
            yield break;
        }
        yield return new WaitForEndOfFrame();
        tren.time = Mathf.Lerp(tren.time, 0, 1f * t);
        StartCoroutine(VanishTrail(t + Time.deltaTime));
    }

    private IEnumerator Shake(float t, float intensity = 10)
    {
        float rx = Random.Range(-1f, 1f);
        float ry = Random.Range(-1f, 1f);
        Vector3 startPos = new Vector3(0, 0, -10);
        mainCamera.transform.position = new Vector3(startPos.x + (intensity / 220f) * rx, startPos.y + (intensity / 220f) * ry, startPos.z);
        t -= Time.deltaTime;
        yield return new WaitForEndOfFrame();

        if (t > 0f)
            StartCoroutine(Shake(t, intensity));
        else
            mainCamera.transform.position = startPos;
    }
}
