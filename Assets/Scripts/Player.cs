using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {
   
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpStrength = 20f;
    [SerializeField] float climbSpeed = 5f;
    float initialGravityScale;

    public Rigidbody2D rb;
    public Animator anim;
    private CapsuleCollider2D playerCollider;
    private BoxCollider2D playerFeet;

    private bool canMove = true;

    public bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        initialGravityScale = rb.gravityScale;
        playerFeet = GetComponent<BoxCollider2D>();
    }
		
	void Update () {
        if (canMove)
        {
            Run();
            Jump();
            ClimbLadder();
            Die();
        }       
	}

    void Run()
    {
        
        float xAxis = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 movement = new Vector2(xAxis * runSpeed, rb.velocity.y);
        rb.velocity = movement;
        bool playerHasHorizontalSpeed = Mathf.Abs(movement.x) > Mathf.Epsilon;      
        anim.SetBool("run", playerHasHorizontalSpeed);      
        FlipSprite(movement);
    }

    void Jump()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            if (!playerFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                return;
            }
            Vector2 jumpVelocity = new Vector2(0, jumpStrength);
            rb.velocity = jumpVelocity;
        }
        
    }

    void ClimbLadder()
    {
        if (!playerCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            rb.gravityScale = initialGravityScale;
            anim.SetBool("climb", false);
            return;
        }
        rb.gravityScale = 0f;
        Vector2 climbing = new Vector2(rb.velocity.x, CrossPlatformInputManager.GetAxis("Vertical") * climbSpeed);
        rb.velocity = climbing;
        anim.SetBool("climb", true);
    }

    private void FlipSprite(Vector2 movement)
    {
        if (movement.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        if (movement.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void Die()
    {
        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")) || playerFeet.IsTouchingLayers(LayerMask.GetMask("Enemy","Hazards")))
        {
            canMove = false;           
            anim.SetTrigger("die");
            rb.AddForce(new Vector2(0f, 10f), ForceMode2D.Impulse);
            GameObject.FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}
