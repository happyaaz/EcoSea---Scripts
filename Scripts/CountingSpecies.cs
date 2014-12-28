using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class CountingSpecies : MonoBehaviour {
	
	public Dictionary <string, int> numberOfSpeciesBefore = new Dictionary <string, int> ();
	public Dictionary <string, int> numberOfSpeciesAfter = new Dictionary <string, int> ();
	public Dictionary <string, int> changeableNumberOfFishToCalcRatio = new Dictionary <string, int> ();

	GUI_Main gM;

	void Start () {

		gM = GameObject.Find ("NGuiLabelManager").GetComponent <GUI_Main>();
	}

	//  it is often called when we throw in a species
	public void CountBeforeTurn () {

		numberOfSpeciesAfter.Clear();
		numberOfSpeciesBefore.Clear();
		changeableNumberOfFishToCalcRatio.Clear ();

		foreach (string specie in CommonData.tc_scr.existingSpeciesInTheScene) {

			string tagOfSpecies = CommonData.tc_scr.GetArchetypeOfNeededFish (specie);


			numberOfSpeciesBefore [specie] = CommonData.fswn_scr.NumberOfExistingFishOfThisType (specie, tagOfSpecies);

		}

		changeableNumberOfFishToCalcRatio = numberOfSpeciesBefore;
		CommonData.wc_scr.ControllingKnob ();
	}
	

	public void CountAfterTurn () {
	
//		Debug.Log ("Was called by smth");

		//  workaround. sometimes we get an error that a key wasn't found in a dictionary, which means that this function is called twice (probably)


		System.Text.StringBuilder summary = new System.Text.StringBuilder ();

		foreach (string specie in CommonData.tc_scr.existingSpeciesInTheScene) 
		{
//			Debug.Log (specie);
			string tagOfSpecies = CommonData.tc_scr.GetArchetypeOfNeededFish (specie);
			numberOfSpeciesAfter [specie] = CommonData.fswn_scr.NumberOfExistingFishOfThisType (specie, tagOfSpecies);;
		}

		gM.displayAfterTurn = false;
		/*
		foreach (string thisShit in CommonData.tc_scr.existingSpeciesInTheScene) 
		{
//			Debug.Log ("Before = " + thisShit + ", " + numberOfSpeciesBefore[thisShit] + " : After = " + numberOfSpeciesAfter[thisShit]);
			if (numberOfSpeciesBefore[thisShit] < numberOfSpeciesAfter[thisShit])
			{
				summary.Append ("Number of " + thisShit + ", they increased by " + Mathf.Abs(numberOfSpeciesAfter[thisShit] - numberOfSpeciesBefore [thisShit]).ToString());
			} 
			else if (numberOfSpeciesBefore[thisShit] > numberOfSpeciesAfter[thisShit])
			{
				summary.Append ("Number of " + thisShit + ", they decreased by " + Mathf.Abs(numberOfSpeciesAfter[thisShit] - numberOfSpeciesBefore [thisShit]).ToString());
			} 
			else 
			{
				summary.Append("Number of " + thisShit + " is the same".ToString());
			}
			summary.Append (System.Environment.NewLine);	
		}
		*/
//		Debug.Log ("summary.ToString() = " + summary.ToString());
		gM.AfterTurnReport = summary.ToString();
		gM.displayAfterTurn = true;

		foreach (KeyValuePair <string, int> pair in numberOfSpeciesAfter) 
		{
//			Debug.Log (pair.Value);
			if (pair.Value == 0)
			{
			//	Debug.Log ("HERE. all of a sudden");
				CommonData.tc_scr.existingSpeciesInTheScene.Remove (pair.Key);
				//  zero elemets - remove the minimap object
				CommonData.lrc_scr.RemovingObjectFromTree (pair.Key);
				CommonData.lrc_scr.RemovingConnections (pair.Key);
			}
		}

//		Debug.Log ("Yeah, I count them here");

		CommonData.lrc_scr.DrawingRendererRunner ();
		changeableNumberOfFishToCalcRatio.Clear ();
		changeableNumberOfFishToCalcRatio = numberOfSpeciesAfter;
		CommonData.wc_scr.CheckIfLevelIsWon ();
		//CommonData.ib_scr.ControlBars ();
	}
}
