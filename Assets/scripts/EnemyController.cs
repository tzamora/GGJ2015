using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	// movement config
	public float gravity = -25f;
	public float runSpeed = 8f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	public float inAirDamping = 5f;
	public float jumpHeight = 3f;
	public int side = 1; // -1 left, 1 right

	public float health = 4f;

	[HideInInspector]
	private float normalizedHorizontalSpeed = 0;

	private CharacterController2D _controller;
	private Vector3 _velocity;

	public GunController weapon;

	public Material damageMaterial;

	public Material normalMaterial;

	
	void Awake()
	{
		_controller = GetComponent<CharacterController2D>();
	}

	// Use this for initialization
	void Start () 
	{


		//MoveRoutine ();

		//JumpRoutine ();

	}

	void MoveRoutine(){
		
		this.ttAppendLoop ("MoveRoutine", delegate(ttHandler rootLoop) {

			// get the player position
			Vector3 targetPosition = GameContext.Get.player.transform.position;
			
			Vector3 relativePoint = (transform.position - targetPosition).normalized;

			if (relativePoint.x > 0.0f)
			{
				// left
				Move (true, false, false);
			}
			else if (relativePoint.x < 0.0f)
			{
				// right
				Move (false, true, false);
			}
			
		});
		
	}

	void JumpRoutine()
	{
		this.ttAppendLoop ("JumpRoutine", delegate(ttHandler loop) {
		
			Move (false, false, true);

			loop.WaitFor(3.0f);
		
		});
	}

	void OnTriggerEnter2D(Collider2D other) {

		BulletController bullet = other.GetComponent<BulletController> ();

		if (bullet != null) {
		
			Damage();

		}
		
	}

	void Damage(){
	
		DamageBlinkRoutine();

		health--;

		if (health <= 0) {
		
			Die();

		}
	
	}

	void DamageBlinkRoutine()
	{
		SpriteRenderer[] spriteRenders = gameObject.GetComponentsInChildren<SpriteRenderer> ();

		this
		.ttAppend (delegate() {

			for (int i = 0; i < spriteRenders.Length; i++) {
				
				spriteRenders[i].material = damageMaterial;
				
			}
		
		})
		.ttAppend(0.01f)
		.ttAppend(delegate(){

			for (int i = 0; i < spriteRenders.Length; i++) {
				
				spriteRenders[i].material = normalMaterial;
				
			}	

		});
	}
	
	void Move( bool left, bool right, bool jump )
	{
		// grab our current _velocity to use as a base for all calculations
		_velocity = _controller.velocity;
		
		if( _controller.isGrounded )
			_velocity.y = 0;

		if(right)
		{
			normalizedHorizontalSpeed = 1;
			side = 1;
			if( transform.localScale.x < 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );
		}
		else if(left)
		{
			side = -1;
			normalizedHorizontalSpeed = -1;
			if( transform.localScale.x > 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );
		}
		else
		{
			normalizedHorizontalSpeed = 0;
		}
		
		
		// we can only jump whilst grounded
		if( _controller.isGrounded && ( jump ) )
		{
			_velocity.y = Mathf.Sqrt( 2f * jumpHeight * -gravity );
		}
		
		
		// apply horizontal speed smoothing it
		var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
		
		_velocity.x = Mathf.Lerp( _velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor );
		
		// apply gravity before moving
		_velocity.y += gravity * Time.deltaTime;
		
		_controller.move( _velocity * Time.deltaTime );
	}

	void Die(){

		// play die animation

		// wait for half a second

		// disappear

		//tt

	}

}
