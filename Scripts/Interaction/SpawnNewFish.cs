using UnityEngine;
using System.Collections.Generic;

public class SpawnNewFish : MonoBehaviour {

	public List <GameObject> species_list = new List <GameObject> ();
	private Vector3 randomPos;


	void Start () {
	}

	void Update () {

		/*

		 */
		if (Input.GetKeyDown (KeyCode.A))
		{
			GameObject inst = Instantiate (species_list [2], CommonFunctions.RandomPositionWhenThrowingIn (), species_list [2].transform.rotation) as GameObject;
			NeededActions (inst);
		}
		else if (Input.GetKeyDown (KeyCode.P))
		{
			GameObject inst = Instantiate (species_list [3], CommonFunctions.RandomPositionWhenThrowingIn (), species_list [3].transform.rotation) as GameObject;
			NeededActions (inst);
		}
		else if (Input.GetKeyDown (KeyCode.S))
		{
			GameObject inst = Instantiate (species_list [4], CommonFunctions.RandomPositionWhenThrowingIn (), species_list [4].transform.rotation) as GameObject;
			NeededActions (inst);
		}
		else if (Input.GetKeyDown (KeyCode.C))
		{
			GameObject inst = Instantiate (species_list [1], CommonFunctions.RandomPositionWhenThrowingIn (), species_list [1].transform.rotation) as GameObject;
			NeededActions (inst);
		}
		else if (Input.GetKeyDown (KeyCode.B))
		{
			GameObject inst = Instantiate (species_list [0], CommonFunctions.RandomPositionWhenThrowingIn (), species_list [0].transform.rotation) as GameObject;
			NeededActions (inst);
		}
		else if (Input.GetKeyDown (KeyCode.U))
		{
			GameObject child = CommonData.listOfPrefabs_scr.SendInstantiatedObject ("Coral");
			GameObject inst = Instantiate (child, CommonFunctions.RandomPositionWhenThrowingIn (), species_list [0].transform.rotation) as GameObject;
			NeededActions (inst);
		}
		else if (Input.GetKeyDown (KeyCode.K))
		{
			GameObject child = CommonData.listOfPrefabs_scr.SendInstantiatedObject ("SeaUrchin");
			GameObject inst = Instantiate (child, CommonFunctions.RandomPositionWhenThrowingIn (), species_list [0].transform.rotation) as GameObject;
			NeededActions (inst);
		}
	}

	void NeededActions (GameObject go) {

		SeaCreature sC = go.GetComponent<SeaCreature> ();
		if (!CommonData.tc_scr.existingSpeciesInTheScene.Contains (sC.nameOfSpecies)){
			CommonData.tc_scr.existingSpeciesInTheScene.Add (sC.nameOfSpecies);
		}
		//  BECAUSE
		CommonData.cs_scr.CountBeforeTurn ();
	}

}
