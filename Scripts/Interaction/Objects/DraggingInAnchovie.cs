using UnityEngine;
using System.Collections;

public class DraggingInAnchovie : MonoBehaviour {

	float randomZ = 0;
	bool possibleToDrag = true;
	GameObject emptyParentToDragIn = null;

	SoundCtrl_SFX_Spawning soundCtrl_SFX_Spawning;


	void Start () {

		soundCtrl_SFX_Spawning = GameObject.Find ("SFX_Spawning").GetComponent <SoundCtrl_SFX_Spawning> ();

		randomZ = Random.Range (10, 20);
		CommonScript cs_An = this.gameObject.GetComponent <CommonScript> ();
	}
	
	
	void FixedUpdate () {
		
		if (possibleToDrag == true)
		{
			Vector3 rawPosition = Input.mousePosition;
			rawPosition.z = randomZ;
			
			Vector3 targetPosition = Camera.main.ScreenToWorldPoint(rawPosition);
			//Debug.Log ("LALALA = " + targetPosition + ", Raw = " + targetPosition);
			emptyParentToDragIn.rigidbody.MovePosition (targetPosition);
		}
		
		if (Input.GetMouseButtonUp (0))
		{
			possibleToDrag = false;

			soundCtrl_SFX_Spawning.Anchovy_SpawnSound ();


			SeaCreature scBarr_scr = this.gameObject.GetComponent <SeaCreature> ();
			scBarr_scr.currentActionState_enum = SeaCreature.PossibleStatesOfActions.Moving;

			//  unparent anchovies
			//  kill the parent
			//  let them move
			//  probably PickANewDest?

			StartCoroutine (DisableThisScriptInOneSecond ());
		}
		
	}
	
	
	IEnumerator DisableThisScriptInOneSecond () {

		yield return new WaitForSeconds (1);
		this.enabled = false;
	}
}
