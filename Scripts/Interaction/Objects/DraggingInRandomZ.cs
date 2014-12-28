using UnityEngine;
using System.Collections;

public class DraggingInRandomZ : MonoBehaviour {

	float randomZ = 0;
	bool possibleToDrag = true;
	public float timeOfSpawning;

	SoundCtrl_SFX_Spawning soundCtrl_SFX_Spawning;

	void Start () {
		soundCtrl_SFX_Spawning = GameObject.Find ("SFX_Spawning").GetComponent <SoundCtrl_SFX_Spawning> ();


		randomZ = Random.Range (5, 18);
	}


	void FixedUpdate () {
	
		if (possibleToDrag == true)
		{
			Vector3 rawPosition = Input.mousePosition;
			rawPosition.z = randomZ;

			Vector3 targetPosition = Camera.main.ScreenToWorldPoint(rawPosition);
//			Debug.Log ("LALALA = " + targetPosition + ", Raw = " + targetPosition);
			rigidbody.MovePosition (targetPosition);
		}

		if (Input.GetMouseButtonUp (0))
		{
			rigidbody.MovePosition (new Vector3 (rigidbody.position.x, rigidbody.position.y, randomZ));
			if (Time.time - timeOfSpawning < 0.5f)
			{
				return;
				//rigidbody.MovePosition (CommonFunctions.RandomPositionWhenThrowingIn ());
			}
			else
			{
				possibleToDrag = false;
				soundCtrl_SFX_Spawning.Plankton_SpawnSound ();

				StartCoroutine (DisableThisScriptInOneSecond ());
			}
		}

	}


	IEnumerator DisableThisScriptInOneSecond () {

		yield return new WaitForSeconds (1);
		this.enabled = false;
	}


			
}
