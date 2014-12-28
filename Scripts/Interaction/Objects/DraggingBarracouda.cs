using UnityEngine;
using System.Collections;

public class DraggingBarracouda : MonoBehaviour {

	float randomZ = 0;
	bool possibleToDrag = true;

	SoundCtrl_SFX_Spawning soundCtrl_SFX_Spawning;

	
	void Start () {
		soundCtrl_SFX_Spawning = GameObject.Find ("SFX_Spawning").GetComponent <SoundCtrl_SFX_Spawning> ();

		randomZ = Random.Range (7, 20);
	}
	
	
	void FixedUpdate () {
		
		if (possibleToDrag == true)
		{
			Vector3 rawPosition = Input.mousePosition;
			rawPosition.z = randomZ;
			
			Vector3 targetPosition = Camera.main.ScreenToWorldPoint(rawPosition);
			//Debug.Log ("LALALA = " + targetPosition + ", Raw = " + targetPosition);
			rigidbody.MovePosition (targetPosition);
		}
		
		if (Input.GetMouseButtonUp (0))
		{
			possibleToDrag = false;

			soundCtrl_SFX_Spawning.Barracuda_SpawnSound ();

			SeaCreature scBarr_scr = this.gameObject.GetComponent <SeaCreature> ();
			scBarr_scr.currentActionState_enum = SeaCreature.PossibleStatesOfActions.Moving;

			StartCoroutine (DisableThisScriptInOneSecond ());
		}
		
	}


	IEnumerator DisableThisScriptInOneSecond () {

		yield return new WaitForSeconds (1);
		this.enabled = false;
	}
}
