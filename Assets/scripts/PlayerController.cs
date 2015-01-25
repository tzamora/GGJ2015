using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	public GunController gun;

	public AudioClip damageSound;

	public Animator animator;

	public Material damageMaterial;
	
	public Material normalMaterial;

	// movement config
	public float gravity = -25f;
	public float runSpeed = 8f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	public float inAirDamping = 5f;
	public float jumpHeight = 3f;
	public int side = 1;

	public int health;
	
	[HideInInspector]
	private float normalizedHorizontalSpeed = 0;
	
	private CharacterController2D _controller;
	private RaycastHit2D _lastControllerColliderHit;
	public Vector3 _velocity;

	public AudioClip jumpSound;

	public AudioClip shootSound;

	void Awake()
	{
		_controller = GetComponent<CharacterController2D>();
	}

	void Start(){

		health = 6;

		this.ttAppendLoop ("MoveRoutine",delegate(ttHandler handler){

			move ();

		});

		HandleInput ();

	}


	// the Update loop contains a very simple example of moving the character around and controlling the animation
	void move()
	{
		// grab our current _velocity to use as a base for all calculations
			_velocity = _controller.velocity;
		
		if( _controller.isGrounded )
			_velocity.y = 0;

		if( Input.GetKey( KeyCode.RightArrow ) || InputManager.Devices[0].LeftStick.Right)
		{
			side = 1;

			normalizedHorizontalSpeed = 1;
			if( transform.localScale.x < 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );
			
			if( _controller.isGrounded ){
				animator.Play( Animator.StringToHash( "walk" ) );
			}
				
		}
		else if( Input.GetKey( KeyCode.LeftArrow ) || InputManager.Devices[0].LeftStick.Left)
		{
			side = -1;

			normalizedHorizontalSpeed = -1;
			if( transform.localScale.x > 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );
			
			if( _controller.isGrounded ){
				animator.Play( Animator.StringToHash( "walk" ) );
			}
				
		}
		else
		{
			normalizedHorizontalSpeed = 0;
			
			if( _controller.isGrounded ){
				animator.Play( Animator.StringToHash( "animation_idle" ) );
			}
				
		}
		
		
		// we can only jump whilst grounded
		if( _controller.isGrounded && (Input.GetKeyDown( KeyCode.X ) || InputManager.Devices[0].Action1) )
		{
			_velocity.y = Mathf.Sqrt( 2f * jumpHeight * -gravity );
			animator.Play( Animator.StringToHash( "animation_jump" ) );

			SoundManager.Get.PlayClip(jumpSound, false);
		}
		
		// apply horizontal speed smoothing it
		var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
		_velocity.x = Mathf.Lerp( _velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor );
		
		// apply gravity before moving
		_velocity.y += gravity * Time.deltaTime;
		
		_controller.move( _velocity * Time.deltaTime );
	}

	void HandleInput(){

		this.ttAppendLoop ("ShootRoutine",delegate(ttHandler loop){

			if(InputManager.Devices[0].Action2.WasPressed){

				animator.Play( Animator.StringToHash( "animation_lever" ) );

			}

			// listen the shoot action
			if( InputManager.Devices[0].Action3 || Input.GetKey(KeyCode.Z))
			{
//				int vDirection = 0;
//				
//				if(InputManager.ActiveDevice.Direction.Up || Input.GetKey(KeyCode.UpArrow)){
//					
//					vDirection = 1;
//					
//				}else if(InputManager.ActiveDevice.Direction.Down || Input.GetKey(KeyCode.DownArrow)){
//					
//					vDirection = -1;
//					
//				}
//				
//				int hDirection = 1;
//				
//				if(InputManager.ActiveDevice.Direction.Left || Input.GetKey(KeyCode.LeftArrow)){
//					
//					hDirection = -1;
//					
//				}else if(InputManager.ActiveDevice.Direction.Right || Input.GetKey(KeyCode.RightArrow)){
//					
//					hDirection = 1;
//					
//				}
//				
//				if(hDirection == 0 && vDirection == 0)
//				{
//					hDirection = side;
//				}
				
				gun.Shoot(new Vector3(side, 0f));

				SoundManager.Get.PlayClip(shootSound, false);
				
				loop.WaitFor(0.3f);
			}
			
		});

	}

	void OnTriggerEnter2D(Collider2D theOther) {

		if (theOther.transform.parent != null) {
		
			RailController rail = theOther.transform.parent.GetComponent<RailController> ();
			
			if (rail!=null) {
				
				transform.parent = rail.transform;
				
			}

		}


		DieTriggerController dieTrigger = theOther.GetComponent<DieTriggerController>();

		if (dieTrigger!=null) {

			Die();

		}

		EnemyBulletController enemyBullet = theOther.GetComponent<EnemyBulletController>();

		GhostEnemyController raven = theOther.GetComponent<GhostEnemyController>();

		if (enemyBullet != null || raven != null) {

			health--;

			SoundManager.Get.PlayClip(damageSound, false);

			DamageBlinkRoutine();

			List<Image> player1Health = GameContext.Get.GUI.player1Health;

			if(player1Health.Count > 0){

				Destroy(player1Health[player1Health.Count-1].gameObject);

				player1Health.RemoveAt(player1Health.Count-1);

			}

			Destroy (enemyBullet.gameObject);

			if(health <= 0){

				Die ();

			}
		
		}


	}

	void DamageBlinkRoutine()
	{
		SpriteRenderer[] spriteRenders = gameObject.GetComponentsInChildren<SpriteRenderer> ();
		
		this.ttAppend ("damageBlinkRoutine", delegate() {
				
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

	void OnTriggerExit2D(Collider2D theOther) {

		if (theOther.transform.parent != null) {
		
			RailController rail = theOther.transform.parent.GetComponent<RailController> ();
			
			if (rail!=null) {
				
				transform.parent = null;
				
			}
		
		}
		
	}

	void Die(){

		SoundManager.Get.PlayClip (damageSound, false);

		// first show a die animation
		// then disapear
		// wait a few seconds
		// show gameover screen

		this.ttAppend ("DieRoutine", delegate(ttHandler handler){

			/*SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

			if (spriteRenderer != null)
			{
				spriteRenderer.enabled = false;
			}*/

			GameContext.Get.GUI.ShowGameOver();

			gameObject.SetActive(false);


		//}).ttAppend(1f).ttAppend(delegate(ttHandler handler){

		//	GameContext.Get.GUI.ShowGameOver();	

		});



	}
	
}
