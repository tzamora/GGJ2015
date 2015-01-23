using UnityEngine;
using System.Collections;

public class BackgroundScrollerController : MonoBehaviour {

	public GameObject background1;

	public GameObject background2;

	public float ScrollingSpeed = 1f;

	private GameObject currentBackground;

	public Camera cameraLooking;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine( StartMovingBackground());

		currentBackground = background1;
	}

	IEnumerator StartMovingBackground()
	{
		while(true)
		{
			background1.transform.Translate(ScrollingSpeed * Time.deltaTime, 0f, 0f);
			
			background2.transform.Translate(ScrollingSpeed * Time.deltaTime, 0f, 0f);

			yield return null;

			//
			// check if the background is not inside the camera
			//

			//if(!currentBackground.transform.GetBounds().IsVisibleFrom(GameContext.Get.GameCamera))
			if(!currentBackground.transform.GetBounds().IsVisibleFrom(Camera.main))
			//if(!currentBackground.renderer.IsVisibleFrom(GameContext.Get.GameCamera))
			{
				Vector3 backgroundWidth = new Vector3(background2.transform.GetBounds().size.x, 0f, 0f);

				currentBackground.transform.position = 
					background2.transform.position + backgroundWidth;

				//
				// swap the backgrounds
				//


				background1 = background2;

				background2 = currentBackground;

				currentBackground = background1;
			}
		}
	}

	void OnDrawGizmos() 
	{
		Bounds background1Bounds = background1.transform.GetBounds();

		Bounds background2Bounds = background2.transform.GetBounds();

		Gizmos.DrawWireCube(background1Bounds.center, background1Bounds.size);

		Gizmos.DrawWireCube(background2Bounds.center, background2Bounds.size);
	}
}
