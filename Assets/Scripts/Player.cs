using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpStrength = 20f;
    [SerializeField] float climbSpeed = 5f;
    float initialGravityScale;

    private Rigidbody2D rb;
    private Animator anim;
    private CapsuleCollider2D playerCollider;

    

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        initialGravityScale = rb.gravityScale;
    }
	
	
	void Update () {
        Run();
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Jump();
        }
        ClimbLadder();

        
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
        if (Mathf.Abs(rb.velocity.y) > Mathf.Epsilon)//if y movement > 0
        {
            return;
        }
        Vector2 jumpVelocity = new Vector2(rb.velocity.x, jumpStrength);
        rb.velocity = jumpVelocity;
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
}
