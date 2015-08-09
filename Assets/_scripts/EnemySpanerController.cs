using UnityEngine;
using System.Collections;
using InControl;

public class EnemySpanerController : MonoBehaviour {

	//public EnemyCarriageController enemyCarriagePrefab;

	public GameObject ravenEnemyPrefab;

	public GameObject enemyCarriagePrefab;

	public Transform leftSpawnPoint;

	public Transform leftTopSpawnPoint;

	public Transform rightSpawnPoint;

	public Transform carriageSpawnLeftPoint;

	public Transform carriageSpawnRightPoint;

	// Use this for initialization
	void Start () {

		this.ttAppendLoop (delegate(ttHandler handler){

			//if (InputManager.Devices[0].LeftBumper.WasPressed) {
			if(handler.timeSinceStart > 4000){
				
				SpawnRavenRoutine ();
				
			}

		});

	}

	void SpawnRavenRoutine(){

		GameObject ravenGO = (GameObject) GameObject.Instantiate (ravenEnemyPrefab.gameObject, rightSpawnPoint.position, Quaternion.identity);

		//ravenGO.transform.localScale = new Vector3 (-1f, 1f, 1f);

		ravenGO.GetComponent<GhostEnemyController> ().OnDie += delegate() {




			GameObject ravenAirGO = (GameObject) GameObject.Instantiate (ravenEnemyPrefab.gameObject, leftTopSpawnPoint.position, Quaternion.identity);
			
			ravenAirGO.GetComponent<GhostEnemyController>().side = 1;
			
			ravenAirGO.transform.localScale = new Vector3(-0.65f, 0.65f, 1f);




			//////

			GameObject raven2GO = (GameObject) GameObject.Instantiate (ravenEnemyPrefab.gameObject, leftSpawnPoint.position, Quaternion.identity);

			raven2GO.GetComponent<GhostEnemyController>().side = 1;

			raven2GO.transform.localScale = new Vector3(-0.65f, 0.65f, 1f);

			raven2GO.GetComponent<GhostEnemyController>().OnDie += delegate() {

				GameObject carriageGO = (GameObject) GameObject.Instantiate (enemyCarriagePrefab.gameObject, carriageSpawnRightPoint.position, Quaternion.identity);

				//carriageGO2.GetComponent<CarriageLeverController>().side = 1;

				carriageGO.GetComponent<CarriageLeverController>().OnDie += delegate(){

					// ultimo mae

					GameObject carriageGO2 = (GameObject) GameObject.Instantiate (enemyCarriagePrefab.gameObject, carriageSpawnLeftPoint.position, Quaternion.identity);

					carriageGO2.transform.localScale = new Vector3(-1f, 1f, 1f);

					carriageGO2.GetComponent<CarriageLeverController>().side = 1;

					carriageGO2.GetComponent<CarriageLeverController>().enemiesInCarriage[0].side = 1;

					carriageGO2.GetComponent<CarriageLeverController>().OnDie += delegate() {

						SpawnRavenRoutine();

					};

				};




			};

		};


	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
