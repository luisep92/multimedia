using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Android;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float jumpForce;
    public float speed;
    public TMP_Text countText;
    public TMP_Text winText;
    private Rigidbody rb;
    private Light lig;
    private Renderer ren;
    private int count;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lig = gameObject.GetComponent<Light>();
        ren = GetComponent<Renderer>();
        count = 0;
        SetCountText();
        winText.text = "";
    }
    private void Update()
    {
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.1f)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        // Con la etiqueta Pick Up
        if (other.gameObject.CompareTag("PickUp"))
        {
            // Lo desactiva haciéndolo desaparecer
            other.gameObject.SetActive(false);
            // Contamos uno más
            count = count + 1;
            SetCountText();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            ren.material.EnableKeyword("_EMISSION");
            Color c = new Color(Random.value, Random.value, Random.value);
            ren.material.SetColor("_EmissionColor", c * 3);
            ren.material.color = new Color(Random.value, Random.value, Random.value);
            lig.color = c;
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winText.text = "You Win!";
        }
    }
}
