using UnityEngine;
using System.Collections;

public class DraggingAnchovie : MonoBehaviour {

	float randomZ = 0;
	bool possibleToDrag = true;

	public GameObject actualAnchovie;

	// Use this for initialization
	void Start () {
		actualAnchovie = CommonData.listOfPrefabs_scr.SendInstantiatedObject ("Anchovie");
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
			
			DisableThisScriptInOneSecond ();
		}
		
	}
	
	
	void DisableThisScriptInOneSecond () {
		
		GameObject draggedInObject = Instantiate (actualAnchovie, this.gameObject.transform.position, actualAnchovie.transform.rotation) as GameObject;

		SeaCreature sc = draggedInObject.GetComponent <SeaCreature> ();
		
		string eventStr = sc.nameOfSpecies + "_WasSpawned_";
		CommonData.mg_scr.WriteDownEvent (eventStr);

		
		if (!CommonData.tc_scr.existingSpeciesInTheScene.Contains (sc.nameOfSpecies))
		{
			CommonData.tc_scr.existingSpeciesInTheScene.Add (sc.nameOfSpecies);
		}
		//  BECAUSE
		CommonData.cs_scr.CountBeforeTurn ();
		
		CommonData.ib_scr.ControlBars ();
		
		CommonData.mg_scr.CheckMiniGoals ();
		Destroy (GameObject.Find (gameObject.name));
//		Debug.Log ("Hello");

	}
}
