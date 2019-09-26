using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 0.2f;
    public float jumpHeight = 20f;

    private bool grounded;

    private Animator anim;
    private Rigidbody2D rb;

    private SpriteRenderer sr;

    // ground Probe
    public Transform groundProbe;
    public float groundProbeRadius = 0.5f;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("SpeedY", rb.velocity.y);
        anim.SetBool("Grounded", grounded);
        anim.SetBool("Running", false);

        if (Input.GetKey(KeyCode.D))
        {
            sr.flipX = false;
            rb.velocity = new Vector2(speed, rb.velocity.y);
            anim.SetBool("Running", true);
        }
        if (Input.GetKey(KeyCode.A))
        {
            sr.flipX = true;
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            anim.SetBool("Running", true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded) {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            anim.SetTrigger("Jump");
        }

        grounded = Physics2D.OverlapCircle(groundProbe.position, groundProbeRadius, groundLayer);
        Debug.Log(grounded);
    }
}
