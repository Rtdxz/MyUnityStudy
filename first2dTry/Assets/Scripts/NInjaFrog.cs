using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NInjaFrog : MonoBehaviour
{
    public float speed=2.0f;
    public LayerMask layer;
    public Transform cellingCheck;
    public Transform rightupCheck;
    public Transform rightdownCheck;
    private Rigidbody2D rigidbody2D;
    private Animator animator;
    private CapsuleCollider2D capsuleCollider2D;
    private bool isCollided;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(speed , rigidbody2D.velocity.y, 0);
        transform.position += movement*Time.deltaTime;
        isCollided = Physics2D.Linecast(rightupCheck.position, rightdownCheck.position, layer);
        if (isCollided)
        {
            //Debug.DrawLine(rightupCheck.position, rightdownCheck.position, Color.red);
            rigidbody2D.transform.eulerAngles += new Vector3(0f,180f, 0f);
            speed *= -1f;
        }
        else
        {
            //Debug.DrawLine(rightupCheck.position, rightdownCheck.position, Color.green);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            float height = collision.GetContact(0).point.y - cellingCheck.position.y;
            if (height > 0)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 8.0f, ForceMode2D.Impulse);
                speed = 0f;
                animator.SetTrigger("Hit");
                capsuleCollider2D.enabled = false;
                rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
                
            }
        }
    }
    public void Death()
    {
        Destroy(gameObject);
    }
}
