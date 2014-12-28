using UnityEngine;
using System.Collections;

public class DraggingIn : MonoBehaviour {


	bool possibleToDrag = true;
	public float timeOfSpawning;

	SoundCtrl_SFX_Spawning soundCtrl_SFX_Spawning;

	void Start ()
	{
		soundCtrl_SFX_Spawning = GameObject.Find ("SFX_Spawning").GetComponent <SoundCtrl_SFX_Spawning> ();

	}

	// Update is called once per frame
	void FixedUpdate () {
	

		if (possibleToDrag == true)
		{
			RaycastHit[] hits;
			
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			
			hits = Physics.RaycastAll(ray, Mathf.Infinity);
			int i = 0;
			while (i < hits.Length) 
			{
				RaycastHit hit = hits[i];
				if (hit.transform.tag == "OceanFloor") 
				{
					this.transform.position = new Vector3 (hit.point.x, 
					                                       hit.point.y, 
					                                       hit.point.z);
					break;
				}
				i++;
			}
		}

		if (Input.GetMouseButtonUp (0))
		{
			if (Time.time - timeOfSpawning < 0.5f)
			{
				return;
				//Debug.Log ("UEUEUEUE");
				//this.transform.position = CommonFunctions.RandomPositionWhenThrowingIn ();

				//Coral coral = this.gameObject.GetComponent <Coral> ();
				//coral.Placement ();
			}
			else
			{
				possibleToDrag = false;
				CheckForSpecie ();
				StartCoroutine (DisableThisScriptInOneSecond ());
			}
		}

	}


	IEnumerator DisableThisScriptInOneSecond () {

		yield return new WaitForSeconds (1);
		this.enabled = false;
	}
	//we run this to check which spawning SFX to play
	void CheckForSpecie ()
	{
		if(this.transform.name.Contains ("Coral"))
		{
			soundCtrl_SFX_Spawning.Coral_SpawnSound ();
		}else if (this.transform.name.Contains ("SeaWeed"))
		{
			soundCtrl_SFX_Spawning.Seaweed_SpawnSound ();

		}
	}
}
