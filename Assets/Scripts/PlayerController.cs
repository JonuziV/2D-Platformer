using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	private float moveVelocity;
	public float jumpHeight;

	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	private bool grounded;

	private bool doubleJump;

	private Animator anim;

	public  Transform firePoint;
	public GameObject ninjaStar;
	public float shotDelay;
	private float shotDelayCounter;

	public float knockback;
	public float knockLength;
	public float knonckbackCount;
	public bool knockFromRight;

	private Rigidbody2D myRigidbody2D;

	public bool onLadder;
	public float climbSpeed;
	private float climbVelocity;
	private float gravityStore;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		myRigidbody2D = GetComponent<Rigidbody2D> ();
		gravityStore = myRigidbody2D.gravityScale;
	}

	void FixedUpdate(){

		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);

	}

	// Update is called once per frame
	void Update () {

		if (grounded)
			doubleJump = false;
		anim.SetBool ("Grounded", grounded);

		if (Input.GetButtonDown("Jump") && grounded){
			Jump();
		}

		if (Input.GetButtonDown("Jump") && !doubleJump && !grounded){
			Jump();
			doubleJump = true;
		}

		//moveVelocity = 0f;

		moveVelocity = moveSpeed * Input.GetAxisRaw ("Horizontal");

		if (knonckbackCount <= 0) {
			
			myRigidbody2D.velocity = new Vector2 (moveVelocity, myRigidbody2D.velocity.y);
		} else {
			if (knockFromRight)
				myRigidbody2D.velocity = new Vector2 (-knockback, knockback);
			if (!knockFromRight)
				myRigidbody2D.velocity = new Vector2 (knockback, knockback);
			knonckbackCount -= Time.deltaTime;
		}

		anim.SetFloat ("Speed", Mathf.Abs(myRigidbody2D.velocity.x));

		if (myRigidbody2D.velocity.x > 0)
			transform.localScale = new Vector3 (1f, 1f, 1f);
		else if (myRigidbody2D.velocity.x < 0)
			transform.localScale = new Vector3 (-1f, 1f, 1f);

		if(Input.GetButtonDown("Fire1")){
			Instantiate(ninjaStar,firePoint.position, firePoint.rotation);
			shotDelayCounter = shotDelay;
		}

		if (Input.GetButton("Fire1")) {
			shotDelayCounter -= Time.deltaTime;
			if (shotDelayCounter <= 0) {
				shotDelayCounter = shotDelay;
				Instantiate(ninjaStar,firePoint.position, firePoint.rotation);
			}
		}
		if (anim.GetBool("Sword")) {
			anim.SetBool ("Sword", false);
		}

		if (Input.GetButtonDown("Fire2")) {
			anim.SetBool ("Sword", true);
		}

		if (onLadder) {
			myRigidbody2D.gravityScale = 0f;

			climbVelocity = climbSpeed * Input.GetAxisRaw ("Vertical");

			myRigidbody2D.velocity = new Vector2 (myRigidbody2D.velocity.x, climbVelocity);
		}

		if (!onLadder) {
			myRigidbody2D.gravityScale = gravityStore;
		}
	}

	public void Jump(){
		myRigidbody2D.velocity = new Vector2 (myRigidbody2D.velocity.x, jumpHeight);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.transform.tag == "Moving Platform") {
			transform.parent = other.transform;
		}
	}

	void OnCollisionExit2D(Collision2D other)
	{
		if (other.transform.tag == "Moving Platform") {
			transform.parent = null;
		}
	}
}
