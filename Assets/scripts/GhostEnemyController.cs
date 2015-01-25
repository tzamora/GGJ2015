using UnityEngine;
using System.Collections;

public class GhostEnemyController : MonoBehaviour {

	public GunController weapon;

	// Use this for initialization
	void Start () 
	{
		SeekAndDestroyRoutine ();

		ShootRoutine ();

		FadeTrailRoutine ();

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
