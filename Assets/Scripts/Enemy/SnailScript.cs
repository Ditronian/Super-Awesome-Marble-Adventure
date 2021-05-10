using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailScript : MonoBehaviour
{

	public float moveSpeed = 1f;
	public float knockBackForce = 500.0f;
	public Rigidbody2D myBody;
	public Animator anim;
	public bool isMovingLeft;
	public bool canMove;
	public bool isStunned;
	public Transform down_Collision, up_Collision, left_Collision, right_Collision;
	public LayerMask playerLayer;
	public AudioClip stunSound;
	private AudioSource soundEffectsPlayer;
	private AudioController audioController;

	void Awake()
	{
		myBody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		soundEffectsPlayer = FindObjectOfType<ReferenceManager>().soundEffectPlayer;
		audioController = soundEffectsPlayer.GetComponent<AudioController>();
	}

	// Use this for initialization
	void Start()
	{
		canMove = true;
		isMovingLeft = true;
	}

	// Update is called once per frame
	void Update()
	{
		if (canMove)
		{
			if (isMovingLeft) myBody.velocity = new Vector2(-moveSpeed, myBody.velocity.y);
			else myBody.velocity = new Vector2(moveSpeed, myBody.velocity.y);
		}
		
		checkCollision();
	}

	//Flips the Snails movement direction
	private void changeDirection()
	{
		isMovingLeft = !isMovingLeft;
		Vector3 tempScale = transform.localScale;

		//Flip Ray Colliders to compensate for flipping of the sprite
		Vector3 oldLeftPos = left_Collision.position;
		left_Collision.position = right_Collision.position;
		right_Collision.position = oldLeftPos;

		//Flip the Sprite
		if (isMovingLeft) tempScale.x = Mathf.Abs(tempScale.x);
		else tempScale.x = -Mathf.Abs(tempScale.x);
		transform.localScale = tempScale;
	}

	//Handles all of the Snail's collisions
	private void checkCollision()
	{
		Collider2D leftHit = Physics2D.OverlapCircle(left_Collision.position, 0.25f);
		Collider2D rightHit = Physics2D.OverlapCircle(right_Collision.position, 0.25f);
		RaycastHit2D bottomHit = Physics2D.Raycast(down_Collision.position, Vector2.down, 1f);
		Collider2D topHit = Physics2D.OverlapCircle(up_Collision.position, 0.35f);      //Uses circle for bigger detection area

		//Handles all collisions
		if (!isStunned)
		{
			//Top Hit and by the player
			if (topHit != null && topHit.gameObject.tag == "Player")
			{
				//Knockback
				Vector2 playerPos = new Vector2(topHit.transform.position.x, topHit.transform.position.y);
				Vector2 dir = new Vector2(transform.position.x, transform.position.y) - playerPos;
				topHit.GetComponent<Rigidbody2D>().AddForce(-dir.normalized * knockBackForce);

				//Effects
				canMove = false;
				isStunned = true;
				myBody.velocity = new Vector2(0, 0);
				anim.Play("SnailStunned");
				audioController.playOnce(stunSound);
				Invoke("unStun", 5f);
			}
			//Left Hit and by the player
			else if (leftHit != null && leftHit.gameObject.tag == "Player" && !leftHit.gameObject.GetComponent<Player>().isImmune)
			{
				//Knockback
				Vector2 playerPos = new Vector2(leftHit.transform.position.x, leftHit.transform.position.y);
				Vector2 dir = new Vector2(transform.position.x, transform.position.y) - playerPos;
				leftHit.GetComponent<Rigidbody2D>().AddForce(-dir.normalized * knockBackForce);
				//Effects
				leftHit.gameObject.GetComponent<Player>().takeDamage();
			}
			//Right Hit and by the player
			else if (rightHit != null && rightHit.gameObject.tag == "Player" && !rightHit.gameObject.GetComponent<Player>().isImmune)
			{
				//Knockback
				Vector2 playerPos = new Vector2(rightHit.transform.position.x, rightHit.transform.position.y);
				Vector2 dir = new Vector2(transform.position.x, transform.position.y) - playerPos;
				rightHit.GetComponent<Rigidbody2D>().AddForce(-dir.normalized * knockBackForce);
				//Effects
				rightHit.gameObject.GetComponent<Player>().takeDamage();
			}

			//Direction Changing
			if (!bottomHit) changeDirection();
			else if (!isMovingLeft && Physics2D.Raycast(right_Collision.position, Vector2.right, 0.15f)) changeDirection();
			else if (isMovingLeft && Physics2D.Raycast(left_Collision.position, Vector2.left, 0.15f)) changeDirection();
		}
	}

	//Un-stuns the snail after the Invoked time period
	private void unStun()
	{
		isStunned = false;
		canMove = true;
		anim.Play("SnailWalk");
	}
}
