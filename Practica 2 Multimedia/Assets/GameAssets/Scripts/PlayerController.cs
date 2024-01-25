using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private enum Sounds { WIN = 0, LOSE = 1 }

    [SerializeField] TMP_Text healthText;
    [SerializeField] float jumpForce;
    public float speed;
    public TMP_Text countText;
    public GameObject hitParticle;
    public TMP_Text winText;
    public AudioClip[] sounds;
    private Rigidbody rb;
    private Light lig;
    private Renderer ren;
    private int count;
    private Vector3 initPos;
    private bool canMove = true;
    private int health;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "RollABall")
            GameManager.Instance.Health = 3;
        health = GameManager.Instance.Health;
        healthText.text = health.ToString();
        initPos = transform.position;
        rb = GetComponent<Rigidbody>();
        lig = gameObject.GetComponent<Light>();
        ren = GetComponent<Renderer>();
        count = 0;
        SetCountText();
        winText.text = "";
    }
    private void Update()
    {
        if (canMove && Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.1f)
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        if (transform.position.y < -5f)
            Fall();
    }
    void FixedUpdate()
    {
        if (canMove)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            rb.AddForce(movement * speed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Con la etiqueta Pick Up
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.GetComponent<PickUp>().OnPickUp();
            count = count + 1;
            SetCountText();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            ChangeColor();
            SpawnParticle(collision);
        }
    }

    void SetCountText()
    {
        countText.text = "PickUps left: " + (12 - count).ToString();
        if (count >= 12)
        {
            StartCoroutine(Win());
        }
    }

    private void ChangeColor()
    {
        ren.material.EnableKeyword("_EMISSION");
        Color c = new Color(Random.value, Random.value, Random.value);
        ren.material.SetColor("_EmissionColor", c * 3);
        ren.material.color = new Color(Random.value, Random.value, Random.value);
        lig.color = c;
    }

    private void SpawnParticle(Collision collision)
    {
        Vector3 collisionNormal = collision.contacts[0].normal;
        Quaternion rotation = Quaternion.LookRotation(collisionNormal);
        Destroy(Instantiate(hitParticle, collision.contacts[0].point, rotation), 3f);
    }


    private void Fall()
    {
        transform.position = initPos;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        GetDamage(1);
    }

    public void GetDamage(int quantity)
    {
        health -= quantity;
        if(health <= 0)
        {
            health = 0;
            StartCoroutine(Die());
        }
        healthText.text = health.ToString();
    }

    private IEnumerator Win()
    {
        winText.text = "You Win!";
        winText.GetComponent<Animator>().Play("WinLoseText");
        winText.color = Color.green;
        GetComponent<AudioSource>().PlayOneShot(sounds[(int)Sounds.WIN]);
        GameManager.Instance.Health = health;
        canMove = false;
        yield return new WaitForSeconds(5f);
        if (SceneManager.GetActiveScene().name == "Labyrinth")
            GameManager.Instance.LoadScene("MainMenu");
        else
            GameManager.Instance.LoadScene("Labyrinth");
    }

    private IEnumerator Die()
    {
        winText.text = "You Lose!";
        winText.color = Color.red;
        winText.GetComponent<Animator>().Play("WinLoseText");
        GetComponent<AudioSource>().PlayOneShot(sounds[(int)Sounds.LOSE]);
        canMove = false;
        yield return new WaitForSeconds(5f);
        GameManager.Instance.Health = 3;
        GameManager.Instance.LoadScene("MainMenu");
    }
}
