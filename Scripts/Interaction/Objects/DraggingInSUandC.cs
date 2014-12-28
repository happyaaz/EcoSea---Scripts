using UnityEngine;
using System.Collections;

public class DraggingInSUandC : MonoBehaviour {

	bool possibleToDrag = true;
	float offset = 0.2f;

	SoundCtrl_SFX_Spawning soundCtrl_SFX_Spawning;


	void Start () {

		soundCtrl_SFX_Spawning = GameObject.Find ("SFX_Spawning").GetComponent <SoundCtrl_SFX_Spawning> ();

		if (this.name.Contains ("Crab"))
		{
			offset = 1.5f;
		}
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
					                                       hit.point.y + offset, 
					                                       hit.point.z);
					break;
				}
				i++;
			}
		}
		
		if (Input.GetMouseButtonUp (0))
		{
			possibleToDrag = false;
			if (this.name.Contains ("Crab"))
			{
				Crab crab_scr = this.gameObject.GetComponent <Crab> ();
				crab_scr.EnableCharacterController ();
				soundCtrl_SFX_Spawning.Crab_SpawnSound ();

			}
			else
			{
				SeaUrchin su_scr = this.gameObject.GetComponent <SeaUrchin> ();
				su_scr.EnableCharacterController ();
				soundCtrl_SFX_Spawning.SeaUrchin_SpawnSound ();

			}

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
