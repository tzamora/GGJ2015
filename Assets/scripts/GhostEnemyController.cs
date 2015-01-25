using UnityEngine;
using System.Collections;
using System;

public class GhostEnemyController : MonoBehaviour {

	public GunController weapon;

	public float speed = 5f;

	public Material damageMaterial;
	
	public Material normalMaterial;
	
	public bool autoShoot = false;

	public float health = 4f;
	
	public GameObject dieExplosion;

	public int side = -1;

	public Action OnDie;

	// Use this for initialization
	void Start () 
	{
		//SeekAndDestroyRoutine ();

		//ShootRoutine ();

		//FadeTrailRoutine ();

		MoveRoutine ();

	}

	void MoveRoutine (){

		this.ttAppendLoop (delegate(ttHandler handler){

			transform.Translate (new Vector3(side * speed, 0f, 0f) * Time.deltaTime);

		});

	}

	void OnTriggerEnter2D(Collider2D other) {
		
		BulletController bullet = other.GetComponent<BulletController> ();
		
		if (bullet != null) {
			
			Destroy(bullet.gameObject);
			
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
			.ttAppend ("damageBlinkRoutine", delegate() {
				
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

	void FadeTrailRoutine()
	{
		// number of copies
		int counter = 0;

		this.ttAppendLoop ("FadeRoutine", delegate(ttHandler rootLoop){

			GameObject copy = Instantiate(this.gameObject, transform.position, Quaternion.identity) as GameObject;

			copy.GetComponent<GhostEnemyController>().enabled = false;

			SpriteRenderer faderSprite = copy.GetComponent<SpriteRenderer>();

			this.ttAppendLoop("fade"+counter,0.3f, delegate(ttHandler loop){

				faderSprite.color = new Color(1f, 1f, 1f, Mathf.Lerp(0.5f, 0f, loop.t));

			}).ttAppend(delegate(){

				Destroy(faderSprite.gameObject);

			});

			rootLoop.WaitFor(0.1f);

			counter++;
			
		});
	}

	void Die(){
		
		//GameObject explosion = (GameObject)GameObject.Instantiate (dieExplosion, transform.position, Quaternion.identity);
		
		//float duration = explosion.GetComponent<ParticleSystem>().duration;
		
		//SoundManager.Get.PlayClip (damageSound, false);
		
		SpriteRenderer[] spriteRenderer = gameObject.GetComponentsInChildren<SpriteRenderer>();
		
		for (int i = 0; i < spriteRenderer.Length; i++) {
			spriteRenderer[i].enabled = false;
		}

		if (OnDie!=null) {
		
			OnDie ();
		
		}

		
		//Destroy (explosion, duration);
		
		Destroy (gameObject);
		
		//weapon.enabled = false;
		
		//Destroy (weapon);
		
		// play die animation
		
		// wait for half a second
		
		// disappear
		
		//tt
		
	}

	void SeekAndDestroyRoutine()
	{
		this.ttAppendLoop ("SeekAndDestroyRoutine", delegate(ttHandler loop){

			var player = GameContext.Get.player;

			Vector2 targetPosition = player.transform.position;

			transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 1);

		});
	}

	void ShootRoutine()
	{
		this.ttAppendLoop ("ShootRoutine", delegate(ttHandler loop){

			Vector3 targetPosition = GameContext.Get.player.transform.position;

			weapon.Shoot( (targetPosition - transform.position).normalized );

			loop.WaitFor(1f);

		});
	}
}
