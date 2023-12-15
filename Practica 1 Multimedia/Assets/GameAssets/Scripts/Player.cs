using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField] private float speed;
    [SerializeField] private GameObject bullet;
    private Rigidbody2D rb;


    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Shoot();
    }

    private void FixedUpdate()
    {
        var horizontal = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        rb.MovePosition(new Vector2(transform.position.x + horizontal, transform.position.y));
    }

    void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
            Instantiate(bullet, new Vector3(transform.position.x, transform.position.y + 0.5f, 0), Quaternion.identity);
    }
}
