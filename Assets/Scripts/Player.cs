using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    [SerializeField] float runSpeed = 5f;
    Rigidbody2D rb;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	
	void Update () {
        Run();
	}

    void Run()
    {
        float xAxis = CrossPlatformInputManager.GetAxis("Horizontal");
        rb.velocity = new Vector2(xAxis * runSpeed, rb.velocity.y);
    }
}
