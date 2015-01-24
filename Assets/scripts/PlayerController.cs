using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GunController gun;

	// movement config
	public float gravity = -25f;
	public float runSpeed = 8f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	public float inAirDamping = 5f;
	public float jumpHeight = 3f;
	public int side = 0;
	
	[HideInInspector]
	private float normalizedHorizontalSpeed = 0;
	
	private CharacterController2D _controller;
	private RaycastHit2D _lastControllerColliderHit;
	public Vector3 _velocity;

	void Awake()
	{
		_controller = GetComponent<CharacterController2D>();
	}

	void Start(){

		this.ttAppendLoop ("MoveRoutine",delegate(ttHandler handler){

			move ();

		});

		this.ttAppendLoop ("HandleInputRoutine", delegate(ttHandler handler){
			
			HandleInput ();
			
		});

	}


	// the Update loop contains a very simple example of moving the character around and controlling the animation
	void move()
	{
		// grab our current _velocity to use as a base for all calculations
			_velocity = _controller.velocity;
		
		if( _controller.isGrounded )
			_velocity.y = 0;
		
		if( Input.GetKey( KeyCode.RightArrow ) )
		{
			side = 1;

			normalizedHorizontalSpeed = 1;
			if( transform.localScale.x < 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );
			
			if( _controller.isGrounded ){
				//_animator.Play( Animator.StringToHash( "Run" ) );
			}
				
		}
		else if( Input.GetKey( KeyCode.LeftArrow ) )
		{
			side = -1;

			normalizedHorizontalSpeed = -1;
			if( transform.localScale.x > 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );
			
			if( _controller.isGrounded ){
				//_animator.Play( Animator.StringToHash( "Run" ) );
			}
				
		}
		else
		{
			normalizedHorizontalSpeed = 0;
			
			if( _controller.isGrounded ){
				//_animator.Play( Animator.StringToHash( "Idle" ) );
			}
				
		}
		
		
		// we can only jump whilst grounded
		if( _controller.isGrounded && Input.GetKeyDown( KeyCode.UpArrow ) )
		{
			_velocity.y = Mathf.Sqrt( 2f * jumpHeight * -gravity );
			//_animator.Play( Animator.StringToHash( "Jump" ) );s
		}
		
		// apply horizontal speed smoothing it
		var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
		_velocity.x = Mathf.Lerp( _velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor );
		
		// apply gravity before moving
		_velocity.y += gravity * Time.deltaTime;
		
		_controller.move( _velocity * Time.deltaTime );
	}

	void HandleInput(){

		if (Input.GetKey (KeyCode.Space)) {
		
			gun.Shoot( new Vector3(side, 0f, 0f ) );
		
		}
	}

	void OnTriggerEnter2D(Collider2D theOther) {

		RailController rail = theOther.transform.parent.GetComponent<RailController> ();

		if (rail!=null) {
		
			transform.parent = rail.transform;
		
		}

		DieTriggerController dieTrigger = theOther.GetComponent<DieTriggerController>();

		if (dieTrigger) {
		
			Die();

		}


	}

	void OnTriggerExit2D(Collider2D theOther) {
		
		RailController rail = theOther.transform.parent.GetComponent<RailController> ();
		
		if (rail!=null) {
			
			transform.parent = null;
			
		}
		
	}

	void Die(){

		// first show a die animation
		// then disapear
		// wait a few seconds
		// show gameover screen



		this.ttAppend (delegate(ttHandler handler){

			gameObject.GetComponent<SpriteRenderer>().enabled = false;;

		}).ttAppend(1f).ttAppend(delegate(ttHandler handler){

			GameContext.Get.GUI.ShowGameOver();

		});



	}
	
}
