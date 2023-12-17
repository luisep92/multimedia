using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    public static Player Instance;

    [SerializeField] private float speed;
    [SerializeField] private GameObject bullet;
    private Rigidbody2D rb;
    private int maxHealth = 5;
    private int currentHealth = 5;
    private List<GameObject> UIHealth = new();


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
    }

    // Move here because it's done by rigidbody.
    private void FixedUpdate()
    {
        Move();
    }

    void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
            Instantiate(bullet, new Vector3(transform.position.x, transform.position.y + 0.5f, 0), Quaternion.identity);
    }

    private void Move()
    {
        var horizontal = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        rb.MovePosition(new Vector2(transform.position.x + horizontal, transform.position.y));
    }

    public void GetDamage(int damage)
    {
        // TODO: what if receives more than 1 damage?
        UIHealth[maxHealth - currentHealth].SetActive(false);
        currentHealth -= damage;
        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {

    }
}
