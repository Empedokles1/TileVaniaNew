using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
  
    Rigidbody2D rb;
    float moveSpeed = 1f;

	void Start () {
        rb = GetComponent<Rigidbody2D>();      
	}
	
	// Update is called once per frame
	void Update () {
        Move();     
	}

    private void Move()
    {
        
        if (FacingRight())
        {
            rb.velocity = new Vector2(moveSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed, 0);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rb.velocity.x)),1f);       
    }

    private bool FacingRight()
    {
        return transform.localScale.x > 0f;
    }
}
