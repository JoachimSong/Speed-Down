using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    Animator animator;
    public float checkRadius;
    public LayerMask platformMask;
    public GameObject checkPoint;
    public GameObject freezePanel;
    public bool isOnGround;
    private float orangeTimer;
    private bool reduceGravity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        reduceGravity = false;
        orangeTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        isOnGround = Physics2D.OverlapCircle(checkPoint.transform.position, checkRadius, platformMask);
        animator.SetBool("isOnGround", isOnGround);
        Move();
        if (reduceGravity)
        {
            orangeTimer += Time.deltaTime;
            if (orangeTimer >= 5)
            {
                FreezeRecover();
            }
        }
    }
    void Move()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(xInput * speed, rb.velocity.y);
        if (xInput != 0)
            transform.localScale = new Vector3(xInput, 1, 1);
        animator.SetFloat("speed", Mathf.Abs(rb.velocity.x));
    }
    public void Die()
    {
        Debug.Log("Die");
        GameManager.GameOver(true);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Spike"))
        {
            Die();
        }
        if (other.gameObject.CompareTag("Orange"))
        {
            other.GetComponent<Animator>().SetBool("collected", true);
            Freeze();
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Spike"))
        {
            Die();
        }
        if (other.gameObject.CompareTag("Fan"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 10f);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(checkPoint.transform.position, checkRadius);
    }

    void Freeze()
    {
        rb.gravityScale = 0.5f;
        orangeTimer = 0;
        reduceGravity = true;
        freezePanel.SetActive(true);
    }

    void FreezeRecover()
    {
        reduceGravity = false;
        rb.gravityScale = 2.0f;
        orangeTimer = 0;
        freezePanel.SetActive(false);
    }
}
