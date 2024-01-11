using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

// Luis Escolano Piquer
// Todo lo relacionado con el jugador

public class Player : MonoBehaviour, IDamageable
{
    private enum State { IDLE, MOVING, DASHING }

    public static Player Instance;

    [SerializeField] private float speed;
    [SerializeField] private bool _godMode;
    [SerializeField] private GameObject bullet;
    [SerializeField] private AudioClip[] sounds;
    [SerializeField] private int maxHealth = 5;
    private State state = State.MOVING;
    private Camera mainCamera;
    private List<GameObject> UIHealth = new();
    private TrailRenderer tren;
    private AudioSource aSource;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool canDash = true;
    private bool _isDisabled = false;
    private int _currentHealth;
    private bool aimActive;

    public bool IsDisabled => _isDisabled;

    private int CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = value;
            if (value <= 0)
                Die();
        }
    }

    public bool GodMode
    {
        get => _godMode;
        private set 
        { 
            _godMode = value; 
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
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        SetUI();
        CurrentHealth = maxHealth;
    }

    private void Start()
    {
        aimActive = GameManager.Instance.PlayerAim;
    }

    void Update()
    {
        if (GodMode)
            Blink();

        if (IsDisabled)
            return;

        if (aimActive)
            LookAtMouse();

        Shoot();
        if (Input.GetButtonDown("Dash") && canDash)
            StartCoroutine(Dash(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
    }

    // Move here because it's done by rigidbody.
    private void FixedUpdate()
    {
        if (IsDisabled)
            return;

        if (state == State.MOVING)
            Move();
    }

    // Player shoot.
    void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bullet, transform.position + (transform.up * 0.5f), transform.rotation);
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
        StartCoroutine(ShakeCamera(0.1f, 40f));
        StartCoroutine(EnterGodMode(1f));
        for (int i = 0; i < damage; i++)
        {
            if(CurrentHealth > 0)
            {
                CurrentHealth -= 1;
                UIHealth[CurrentHealth].SetActive(false);
            }
        }
    }

    private void LookAtMouse()
    {
        Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseScreenPosition - (Vector2)transform.position).normalized;
        transform.up = direction;
    }

    // F
    private void Die()
    {
        GameManager.Instance.IsPlayerAlive = false;
        GameManager.Instance.Score = 0;
        SceneManager.LoadScene("WinLose");
    }

    // Character alpha changes between 0.2 - 1 on time.
    void Blink(float speed = 7)
    {
        ChangePlayerAlpha(Mathf.PingPong(Time.time * speed, 1) + 0.2f);
    }

    // Change player alpha.
    void ChangePlayerAlpha(float value)
    {
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

    // Character dash
    public IEnumerator Dash(float horizontal, float vertical, float cooldown = 0.4f)
    {
        state = State.DASHING;
        canDash = false;
        rb.velocity = new Vector2(horizontal * 15f, vertical * 15f);
        tren.emitting = true;
        aSource.PlayOneShot(sounds[2]);
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(VanishTrail(0f));
        rb.velocity = Vector2.zero;
        state = State.MOVING;
        yield return new WaitForSeconds(cooldown - 0.2f);
        canDash = true;
    }

    // Lower trail on time
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

    // Camera shake
    private IEnumerator ShakeCamera(float t, float intensity = 10)
    {
        float rx = Random.Range(-1f, 1f);
        float ry = Random.Range(-1f, 1f);
        Vector3 startPos = new Vector3(0, 0, -10);
        mainCamera.transform.position = new Vector3(startPos.x + (intensity / 220f) * rx, startPos.y + (intensity / 220f) * ry, startPos.z);
        t -= Time.deltaTime;
        yield return new WaitForEndOfFrame();

        if (t > 0f)
            StartCoroutine(ShakeCamera(t, intensity));
        else
            mainCamera.transform.position = startPos;
    }

    // UI components (Life)
    private void SetUI()
    {
        Transform cont = GameObject.Find("Content").transform;
        GameObject heart = cont.GetChild(0).gameObject;
        UIHealth.Add(heart);
        for (int i = 1; i < maxHealth; i++)
        {
            UIHealth.Add(Instantiate(heart, cont));
        }
    }

    // Disables player control
    public void Disable()
    {
        _isDisabled = true;
    }

    // Enables player control
    public void Enable()
    {
        _isDisabled = false;
    }

    public void SwitchGodMode()
    {
        GodMode = !GodMode;
    }
}
