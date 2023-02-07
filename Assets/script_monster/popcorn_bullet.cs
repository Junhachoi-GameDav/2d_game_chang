using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popcorn_bullet : MonoBehaviour
{
    Rigidbody2D rb;
    bool isground;
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    private void Update()
    {
        if(isground == true)
        {
            animator.SetBool("Grow", true);
        }
    }
    public void boom()
    {
        animator.SetBool("Ready",true);
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="bottom")
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector3(0, 0, 0);
            isground = true;
        }
    }
}
