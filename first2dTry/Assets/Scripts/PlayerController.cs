using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{[Space]
    public float speed = 5f;//速度
    public float timeSpentInvincible = 0f;
    public float faceDirection;

    [Space]
    public bool isJump = false;//是否处于跳跃状态
    bool jumpPress;//跳跃按键是否按下
    public float jumpForce = 5f;//跳跃力度
    public int jumpCount;//额外跳跃次数
    public bool a = false;

    [Space]
    private Rigidbody2D rigidbody2d;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [Space]
    public LayerMask ground;
    public bool isGround;

    public Transform groundCheck;

    [Space]
    public GameObject JumpButton;
    public Joystick joystick;
    public GameObject RightButton;
    public GameObject LeftButton;
    public GameObject RushButton;
    
    [Space]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2.0f;


    public bool isHurt = false;//受伤状态

    [Space]
    public Transform frontCheck;
    bool isWallCheck = false;
    bool isWallSlide = false;
    public float wallSlideSpeed = 10f;

    [Space]
    public float wallJumpTime = 0.3f;
    public bool isWallJumping = false;
    public float xWallForce = 10f;
    public float yWallForce = 10f;
    public float wallFace;


    [Space]
    public bool isRushing;
    public float rushTime;
    public float rushForce;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //float x = Input.GetAxisRaw("Horizontal");
        //float y = Input.GetAxisRaw("Vertical");
        
        if (Input.GetButtonDown("Jump")&&jumpCount>0)
        {
            jumpPress = true;
        }
        if ((RushButton.GetComponent<MobileButton>().isDown||Input.GetKeyDown(KeyCode.LeftShift) )&&(animator.GetBool("isFalling")||animator.GetBool("isJumping")||animator.GetBool("isDoubleJumping")))
        {
            isRushing = true;

        }
        WallSlide();
        hurt();
        Rush();
    }

    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, ground);
        isWallCheck = Physics2D.OverlapCircle(frontCheck.position, 0.1f, ground);
        Move(); 
       
        Jump();
        SetAnimator();
       

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CheckPoint")
        {

            UIManager.Instance.ShowDialog();
            
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CheckPoint")
        {

            UIManager.Instance.CloseDialog();


        }
    }
    
    void Move()
    {
        if (isRushing) { return; }
        float x = Input.GetAxis("Horizontal");
        if (RightButton.GetComponent<MobileButton>().isDown || LeftButton.GetComponent<MobileButton>().isDown)
            x = RightButton.GetComponent<MobileButton>().isDown ? 1 : -1;
        rigidbody2d.velocity = new Vector2(x * speed, rigidbody2d.velocity.y);
        //rigidbody2d.transform.position +=new Vector3(x * speed * Time.fixedDeltaTime,0, 0);
        //Debug.Log(rigidbody2d.velocity.y);
        if (x > 0)
        {
            //rigidbody2d.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            transform.localScale = new Vector3(6,6,6);
            animator.SetBool("isRunning", true);
            faceDirection = 1;
        }

        if (x < 0)
        {
            //rigidbody2d.transform.eulerAngles = new Vector3(0f, 180f, 0f);
            transform.localScale = new Vector3(-6,6, 6);
            animator.SetBool("isRunning", true);
            faceDirection = -1;
        }
        if (x < 0.001f && x > -0.001f)
        {
            animator.SetBool("isRunning", false);
        }
        
    }

    void Jump()
    {
        if (!jumpPress && isGround)
        {
            jumpCount = 1;
            isJump = false;
            
        }

       
         if(jumpPress && isGround)
        {
           
            isJump = true;
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpForce);
           
            
            jumpPress = false;
            SoundManager.instance.PlayJumpAudio();

        }
        
        else if(jumpPress && jumpCount>0 && !isGround&&!isWallJumping)
        {
            animator.SetBool("isDoubleJumping", true);
            isJump = true;
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpForce-3);
            jumpCount--;
            jumpPress = false;
            SoundManager.instance.PlayJumpAudio();

        }



        if (rigidbody2d.velocity.y < 0)
        {
            rigidbody2d.gravityScale = fallMultiplier;
        }
        else if (rigidbody2d.velocity.y > 0 && (!Input.GetButton("Jump")&&!JumpButton.GetComponent<MobileButton>().isDown))
        {
            rigidbody2d.gravityScale = lowJumpMultiplier;
        }

        else { rigidbody2d.gravityScale = 1f; }

        if (isWallJumping)
        {
            
            rigidbody2d.velocity = new Vector2(xWallForce * wallFace, yWallForce);
        }
    }

    void SetAnimator()
    {
        
        if (isGround)
        {
            animator.SetBool("isFalling", false);
            
        }
        if (isJump||(rigidbody2d.velocity.y>0.01&&!isGround))
        {
           
            animator.SetBool("isJumping", true);
           
        }
        if(rigidbody2d.velocity.y < -0.01&&!animator.GetBool("isWallSliding"))
        {
            animator.SetBool("isFalling", true);
            animator.SetBool("isJumping", false);
            animator.SetBool("isDoubleJumping", false);
        }
        
    }
    public void JumpPress()
    {   if(jumpCount>0)
        jumpPress = true;
    }

    public void WallSlide()
    {
        if (isWallCheck && (Input.GetAxisRaw("Horizontal") != 0|| RightButton.GetComponent<MobileButton>().isDown|| LeftButton.GetComponent<MobileButton>().isDown))
        {
            isWallSlide = true;
            animator.SetBool("isWallSliding", true);
        }
        else {
            isWallSlide =false;
            animator.SetBool("isWallSliding", false);
        }

        if (isWallSlide)
        {
            
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, -1);
            if (Input.GetButtonDown("Jump")||JumpButton.GetComponent<MobileButton>().isDown)
            {
                isWallJumping = true;
                SoundManager.instance.PlayJumpAudio();
                wallFace = -faceDirection;
                Invoke("SetWallJumpingfalse", wallJumpTime);
            }
        }
        

    }
    private void SetWallJumpingfalse()
    {
        isWallJumping = false;
    }

   public void hurt()
    {
        if (isHurt)
        {
            //2
            timeSpentInvincible += Time.deltaTime;

            //3
            if (timeSpentInvincible < 3f)
            {
                float remainder = timeSpentInvincible % 0.3f;
                spriteRenderer.enabled = remainder > 0.15f;
            }
            //4
            else
            {
                timeSpentInvincible = 0f;
                spriteRenderer.enabled = true;
                isHurt = false;
            }
        }


    }
    public void Rush()
    {
        if (isRushing)
        {
            rigidbody2d.gravityScale = 0f;
            rigidbody2d.velocity = new Vector2(faceDirection*rushForce, rigidbody2d.velocity.y);

            
            
            Invoke("StopRushing", rushTime);
        }
    }
    public void StopRushing()
    {
        isRushing = false;
        
        rigidbody2d.gravityScale = 1f;
    }
    
}
