using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Movement Settings")]    
    
    //Hãy sửa lại dòng nãy hợp lý, mỗi thành viên sẽ thay phiên nhau dùng int hoặc float. 
    //Sau đó kiểm tra những phần còn thiếu khác để script có thể hoạt động bình thường
    
    public int moveSpeed;      
    public int jumpForce;   

    [Header("Player Stats")]
    public int health = 3;          
    public int coins = 0;           

    [Header("Ground Check")]
    public Transform groundCheck;   
    public float groundCheckRadius = 0.2f; 
    public LayerMask groundLayer;   

    private Rigidbody2D rb;
    public Animator anim;
    private bool isGrounded;        
    private float moveInput;     
    private bool isFacingRight = true;   

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (!isGrounded)
        {
            anim.SetBool("isJump", true); 
        }
        else
        {
            anim.SetBool("isJump", false); 
        }

        moveInput = Input.GetAxisRaw("Horizontal"); 

        if (moveInput != 0)
        {
            anim.SetBool("isRun", true); 
        }
        else
        {
            anim.SetBool("isRun", false); 
        }

        if (moveInput > 0 && !isFacingRight)
        {
            Flip(); 
        }
        else if (moveInput < 0 && isFacingRight)
        {
            Flip(); 
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); 
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1; 
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            coins++;                     
            Destroy(collision.gameObject); 
            Debug.Log("Coins: " + coins);
        }

        if (collision.CompareTag("Enemy") || collision.CompareTag("Trap"))
        {
            health--;

            if (health <= 0)
            {
                Debug.Log("Game Over!");
            }
        }
    }
}