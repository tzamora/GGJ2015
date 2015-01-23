using UnityEngine;
using System.Collections;

public class AutoDestroyParticle : MonoBehaviour {

	private ParticleSystem particles;

	// Use this for initialization
	void Start () 
	{
		particles = GetComponent<ParticleSystem>();

		StartCoroutine( AutoDestroy() );
	}

	IEnumerator AutoDestroy()
	{
		while(true)
		{
			if(particles != null && !particles.IsAlive())
			{
				Destroy(this.gameObject);
			}

			yield return new WaitForSeconds(0.2f);
		}
	}
}