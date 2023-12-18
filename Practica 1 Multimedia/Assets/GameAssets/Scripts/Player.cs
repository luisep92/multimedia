using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    public static Player Instance;

    [SerializeField] float speed;
    [SerializeField] GameObject bullet;
    private bool godMode;
    private Rigidbody2D rb;
    private int maxHealth = 5;
    private int currentHealth = 5;
    private List<GameObject> UIHealth = new();

    private bool GodMode
    {
        get { return godMode; }
        set 
        { 
            godMode = value; 
            if (value == false) ChangePlayerAlpha(1); 
        }
    }

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);
        Instance = this;
    }

    void Start()
    {
        foreach (Transform t in GameObject.Find("Content").transform)
            UIHealth.Add(t.gameObject);
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Shoot();

        if (godMode)
            Blink();
    }

    // Move here because it's done by rigidbody.
    private void FixedUpdate()
    {
        Move();
    }

    // Player shoot.
    void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
            Instantiate(bullet, new Vector3(transform.position.x, transform.position.y + 0.5f, 0), Quaternion.identity);
    }

    // Player movement.
    private void Move()
    {
        var horizontal = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        rb.MovePosition(new Vector2(transform.position.x + horizontal, transform.position.y));
    }

    // If not godmode receives damage, update UI, enter godmode.
    public void GetDamage(int damage)
    {
        if (GodMode)
            return;
        // TODO: what if receives more than 1 damage?
        UIHealth[maxHealth - currentHealth].SetActive(false);
        currentHealth -= damage;
        if (currentHealth <= 0)
            Die();
        StartCoroutine(EnterGodMode(2f));
    }

    private void Die()
    {

    }

    // Character alpha changes between 0.2 - 1 on time.
    void Blink()
    {
        ChangePlayerAlpha(Mathf.PingPong(Time.time * 5, 1) + 0.2f);
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
}
