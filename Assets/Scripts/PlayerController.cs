using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float start_x;
    public float start_y;

    public float speed = 2f;
    public float jumpForce = 5f;

    private RaycastHit2D raycastHit;

    private float bufferTime = 0.1f;
    private float bufferCounter;

    private float coyoteTime = 0.1f;
    private float coyoteCounter;

    private bool isJumping;

    private float moveInput;

    private Rigidbody2D rb;
    private BoxCollider2D col;

    private bool facingRight = true;

    private bool isGrounded;
    private float extraHeight = 0.02f;

    private Animator anim;

    public GameObject DeathBlock;

    void Start()
    {
        transform.position = new Vector3(start_x, start_y, 0);
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (moveInput == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }

        if (moveInput > 0 && !facingRight)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            facingRight = true;
        }
        else if (moveInput < 0 && facingRight)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            facingRight = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            bufferCounter = bufferTime;
        }
        else
        {
            bufferCounter -= Time.deltaTime;
            bufferCounter = Mathf.Max(0, bufferCounter);
        }
    }

    void FixedUpdate()
    {
        if (raycastHit.collider != null)
        {
            anim.SetBool("isJumping", false);
            coyoteCounter = coyoteTime;
        }
        else
        {
            anim.SetBool("isJumping", true);
            coyoteCounter -= Time.deltaTime;
            coyoteCounter = Mathf.Max(0, coyoteCounter);
        }

        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        raycastHit = Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, extraHeight);

        if (bufferCounter > 0f && coyoteCounter > 0f && !isJumping)
        {
            anim.SetTrigger("takeoff");
            rb.velocity = Vector2.up * jumpForce;
            bufferCounter = 0f;
            coyoteCounter = 0f;
            StartCoroutine(JumpCooldown());
            SoundManager.PlaySound("jump");
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Hazard":
                Kill();
                break;
            case "Objective":
                PlayerPrefs.SetInt("scene" + SceneManager.GetActiveScene().buildIndex, 1);
                PlayerPrefs.Save();
                SceneManager.LoadScene(2);
                SoundManager.PlaySound("victory");
                break;
        }
    }

    public void Kill()
    {
        DeathBlock.transform.position = transform.position;
        transform.position = new Vector3(start_x, start_y, 0);
        SoundManager.PlaySound("death");
    }

    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(coyoteTime);
        isJumping = false;
    }
}
