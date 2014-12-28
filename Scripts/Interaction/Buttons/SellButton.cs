using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SellButton : MonoBehaviour {

	public GameObject infoWindow;
	MouseClickOnBigCollider mcobc;
	SeaCreature sc;
	GUI_Main guiMain;
	int sellPoints = 5;
	// Use this for initialization
	void Start () {
		mcobc = Camera.main.GetComponent<MouseClickOnBigCollider> ();
		guiMain = GameObject.Find ("NGuiLabelManager").GetComponent<GUI_Main> ();
	}
	
	// Update is called once per frame
	void OnClick () {

		sc = mcobc.sC;
//		if (mcobc.ObjectHovered.name.Contains ("Big")) {
//			sc = mcobc.aMS.reference.GetComponent<SeaCreature>();
//		} else {
//			sc = mcobc.ObjectHovered.GetComponent<SeaCreature>();
//		}
		if (mcobc.ObjName.Contains ("Anchovie")) {
			sellPoints = SpawnSpeciesButtons.valueAnchovie/2;
		} else if (mcobc.ObjName.Contains ("Barracouda")) {
			sellPoints = SpawnSpeciesButtons.valueBarracouda/2;
		} else if (mcobc.ObjName.Contains ("Plankton")) {
			sellPoints = SpawnSpeciesButtons.valuePlankton/2;
		} else if (mcobc.ObjName.Contains ("Coral")) {
			sellPoints = SpawnSpeciesButtons.valueCoral/2;
		} else if (mcobc.ObjName.Contains ("Crab")) {
			sellPoints = SpawnSpeciesButtons.valueCrab/2;
		} else if (mcobc.ObjName.Contains ("SeaWeed")) {
			sellPoints = SpawnSpeciesButtons.valueSeaWeed/2;
		} else if (mcobc.ObjName.Contains ("SeaUrchin")) {
			sellPoints = SpawnSpeciesButtons.valueSeaUrchin/2;
		} 
//		StartCoroutine (sc.DieFish ());
//		Debug.Log ("Shit should die");
		MouseClickOnBigCollider.guiElementIsOpen = false;
		GUI_Main.AvailablePoints += sellPoints;

		// if the object is destroyed, we no linger can execyte the code associated with it. That's why we cheat
		CommonData.countingExistingFish.CountNumberOfExistingFish (sc.tag);

		//  refresh the bars
		CommonData.wc_scr.ControllingKnob ();
		CommonData.ib_scr.ControlBars ();

		SeaCreature sc_toDestroy = sc.GetComponent <SeaCreature> ();

		sc_toDestroy.DestroyThisFuckingFish ();
		
		NGUITools.SetActive (infoWindow,false);
//		infoWindow.SetActive (false);
	}
}
