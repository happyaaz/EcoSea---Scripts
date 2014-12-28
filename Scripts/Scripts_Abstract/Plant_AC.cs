using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public abstract class Plant_AC : SeaCreature {
	
	public List <string> speciesICanEat_list = new List <string> ();

	public override void Start () {

		transform.parent = GameObject.FindGameObjectWithTag (this.tag + "_parent").transform;
		//  initializing the tree
		TreeCustom tree = GameObject.Find ("Tree").GetComponent<TreeCustom> ();
		tree.SpawnMiniMapObject (this.nameOfSpecies, "Plant");

		
//		Debug.Log ("Hello =)");

		AssignPrefabs ();

		CommonData.countingExistingFish.CountNumberOfExistingFish ();
		
		maxAmountOfThisSpecies = CommonData.controllingNumberOfSpecies [this.tag];
		LongevityData ();
	}


	public override void Update ()
	{
		//  don't want the original Update to be executed
	}


}
