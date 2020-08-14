using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passerby : MonoBehaviour
{
    public BoxCollider2D collider2D;
    public Rigidbody2D rb;
    public Animator myAnimator;
    public bool isWalkingRight;
    public bool isWalkingLeft;
    public float moveSpeed = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        collider2D = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        CheckPosition();
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
    }

    void Walk()
    {
        if (collider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            if (isWalkingRight)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                myAnimator.SetBool("Walk", true);
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }

            else if (isWalkingLeft)
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                myAnimator.SetBool("Walk", true);
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }

            else
            {
                myAnimator.SetBool("Walk", false);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Boundary"))
        {
            Destroy(gameObject);
        }
    }

    void CheckPosition()
    {
        if (transform.position.x > 5f)
        {
            isWalkingLeft = true;
        }

        else
        {
            isWalkingRight = true;
        }
    }
}
