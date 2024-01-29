using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Character2DController cController;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int jumpdamage;
    [SerializeField] float hitGodModeDuration;
    public int AttackDamage;
    [HideInInspector] public bool canMove= true; //controla cuando puede moverse por si quiero bloquear el personaje
  //  [SerializeField] GameObject attackPoint;
    [HideInInspector] public bool hasHit=false;
    float horizontal;
    bool isJumping;
    
    Animator anim;
    Rigidbody2D rb;
    bool godMode;
    public bool canDoubleJump;
    bool canAttack = true;
    public bool doubleJumpLocked=true;
    

    void Start()
    {
       // attackPoint.SetActive(false);
        cController = GetComponent<Character2DController>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        float speed = canMove ? Mathf.Abs(horizontal) : 0;

        anim.SetFloat("Speed", Mathf.Abs(horizontal));
        anim.SetFloat("VelY", rb.velocity.y);
        anim.SetBool("IsGrounded", cController.IsGrounded);

        Inputs();

        if (godMode)
            Blink();
    }


    public void UnlockDoubleJump() => doubleJumpLocked = false;


    private void FixedUpdate()
    {
        float quantity = canMove ? horizontal * Time.deltaTime * moveSpeed : 0;
            cController.Move(quantity, isJumping);
    }


    public void OnGround()
    {
        isJumping = false;
        canDoubleJump = true;
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
       /* if (col.collider.tag == "Enemy")
        {
            if (JmpAttack(col.transform.position))
            {
                cController.Jump();
                col.collider.GetComponent<Health>().LoseHealth(jumpdamage);
            }
            else
            {
                
                GetDamage(col.collider.GetComponent<Enemy>().GetDamage());
            }
        }*/
    }


    bool JmpAttack(Vector2 posiEnemy)
    {
        if (posiEnemy.y < transform.position.y && rb.velocity.y < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ActiveGodMode(float duration)
    {
        if (!godMode)
        {
            StartCoroutine(GodModeCorr(duration));
        }
    }


    IEnumerator GodModeCorr(float time)
    {
        godMode = true;
        yield return new WaitForSeconds(time);
        godMode = false;
        ChangePlayerAlpha(1);
    }


    void Blink()
    {
        ChangePlayerAlpha(Mathf.PingPong(Time.time * 5, 1) + 0.3f);
    }


    void ChangePlayerAlpha(float value)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color myNewColor = sr.color;
        myNewColor.a = value;
        sr.color = myNewColor;
    }


    public void GetDamage(int damage)
    {
      /*  if (godMode)
            return;
        GameManager.instance.GetComponent<SoundManager>().SoundPlayerGetDamage();
        GetComponent<Health>().LoseHealth(damage);
        ActiveGodMode(hitGodModeDuration);*/
    }


    private void Attack()
    {
        if (!canAttack)
            return;

        
        // GameManager.instance.GetComponent<SoundManager>().SoundPlayerAttack();
        StartCoroutine(ReloadAttack());
    }


    IEnumerator ReloadAttack()
    {
        canAttack = false;
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.3f);
        canAttack = true;
        hasHit = false;
    }


    private void Inputs()
    {

        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (canMove)
            {
                Attack();
            }
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) && canMove)
        {

            {

                if (cController.IsGrounded)
                {
                    isJumping = true;
                    canDoubleJump = true;
                    //  GameManager.instance.GetComponent<SoundManager>().SoundJump();
                }
                else if (canDoubleJump)
                {
                    if (!doubleJumpLocked)
                    {
                        canDoubleJump = false;
                        cController.Jump();
                    }
                }
            }
        }
    }
}
