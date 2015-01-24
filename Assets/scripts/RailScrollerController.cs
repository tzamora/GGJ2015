using UnityEngine;
using System.Collections;

public class RailScrollerController : MonoBehaviour {

	public GameObject background1;

	public GameObject background2;

	public GameObject[] rails;

	public float ScrollingSpeed = 1f;

	private GameObject currentRail;

	public Camera cameraLooking;

	public Vector3 railWidth = Vector3.zero;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine( StartMovingBackground());

		currentRail = rails[0];

		//rails[0].transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));

		railWidth = new Vector3(currentRail.transform.GetBounds().size.x, 0f, 0f);
	}

	IEnumerator StartMovingBackground()
	{

		while(true)
		{
			for (int i = 0; i < rails.Length; i++) {

				rails[i].transform.Translate(ScrollingSpeed * Time.deltaTime, 0f, 0f);

			}

//			background1.transform.Translate(ScrollingSpeed * Time.deltaTime, 0f, 0f);
//			
//			background2.transform.Translate(ScrollingSpeed * Time.deltaTime, 0f, 0f);

			yield return null;

			//
			// check if the background is not inside the camera
			//

			if(!currentRail.transform.GetBounds().IsVisibleFrom(Camera.main))
			{
				print("nos salimos de esta vara");

				currentRail.transform.position =
					rails[rails.Length-1].transform.position + (railWidth);

				//
				// swap the backgrounds
				//


				//background1 = background2;
				rails[0] = rails[1];
				rails[1] = rails[2];
				rails[2] = rails[3];
				rails[3] = rails[4];
				rails[4] = currentRail;

				//currentRail = background1;
				currentRail = rails[0];

			}
		}
	}
}
