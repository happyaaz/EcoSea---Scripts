using UnityEngine;
using System.Collections.Generic;

public class GettingToMate : MonoBehaviour {


	//  REMEMBER
	public GameObject matingParticles;
	public GameObject matingParticleInstance;

	SeaCreature sc_this;
	private int gotToMate_int = 0;

	void Start () {

		sc_this = this.GetComponent <SeaCreature> ();
	}


	//  father
	public void GettingToMate_TargetIsReached (GameObject child_go) {

		if (sc_this.currentActionState_enum == SeaCreature.PossibleStatesOfActions.GettingToMate)
		{
			//  Spread sheet
			CommonData.rae_scr.spreadSheet_parent_Checker_list.Remove (this.gameObject);


			Vector3 actionHappensHere = this.transform.position;

			//create and destroy particles
			matingParticleInstance = Instantiate (matingParticles, actionHappensHere, matingParticles.transform.rotation) as GameObject;
			Destroy (matingParticleInstance, 2.0f);

//			Debug.Log ("Mate = " + sc_this.nameOfSpecies);



			List <int> numberOfFishToBeBorn_list = CommonData.rae_scr.numberOfBornFishPerFish_dict [sc_this.nameOfSpecies];
			//  number of THAT fish that GOT to Each other
			if (numberOfFishToBeBorn_list.Count == 0)
			{
				return;
			}
			int numberOfNewBornFishPerFather_int = numberOfFishToBeBorn_list [0];
			numberOfFishToBeBorn_list.RemoveAt (0);
			CommonData.rae_scr.numberOfBornFishPerFish_dict [sc_this.nameOfSpecies] = numberOfFishToBeBorn_list;
			for (int i = 0; i < numberOfNewBornFishPerFather_int; i++)
			{
				//  events
				gotToMate_int ++;
				CommonData.mg_scr.WriteDownEvent (sc_this.nameOfSpecies + "_GaveBirth_");
				CommonData.mg_scr.CheckMiniGoals ();

				GameObject go = Instantiate (child_go, this.transform.position, child_go.transform.rotation) as GameObject;
				SeaCreature sc = go.GetComponent <SeaCreature> ();
				sc.currentActionState_enum = SeaCreature.PossibleStatesOfActions.WasBorn;

				//  to prevent the situation where newborn fish can be chosen as a mate
				CommonData.rae_scr.newBornFish.Add (sc);
			}
			//  to let a player spawn more species
			CommonData.numberOfFishGivenBirth ++;

			if (CommonData.numberOfFishGettingToMate != 0 && CommonData.numberOfFishGivenBirth != 0 &&
			    CommonData.numberOfFishGettingToMate == CommonData.numberOfFishGivenBirth)
			{
				//Debug.Log ("Done");
				CommonData.rae_scr.TurnIsFinished ();
			}
		}

		CommonStuff ();
	}


	public void Reproduction_Checker (GameObject childToReproduce) {
		
		//  Spread sheet

		CommonData.rae_scr.spreadSheet_parent_Checker_list.Remove (this.gameObject);
		List <int> numberOfFishToBeBorn_list = CommonData.rae_scr.numberOfBornFishPerFish_dict [sc_this.nameOfSpecies];
		//  number of THAT fish that GOT to Each other
		if (numberOfFishToBeBorn_list.Count > 0)
		{
			int numberOfNewBornFishPerFather_int = numberOfFishToBeBorn_list [0];
			numberOfFishToBeBorn_list.RemoveAt (0);
			CommonData.rae_scr.numberOfBornFishPerFish_dict [sc_this.nameOfSpecies] = numberOfFishToBeBorn_list;
			for (int i = 0; i < numberOfNewBornFishPerFather_int; i++)
			{
				//  events
				gotToMate_int ++;
				
				GameObject go = Instantiate (childToReproduce, this.transform.position, childToReproduce.transform.rotation) as GameObject;
				SeaCreature sc = go.GetComponent <SeaCreature> ();
				sc.currentActionState_enum = SeaCreature.PossibleStatesOfActions.WasBorn;
				
				//  to prevent the situation where newborn fish can be chosen as a mate
				CommonData.rae_scr.newBornFish.Add (sc);
			}
			//  to let a player spawn more species
			CommonData.numberOfFishGivenBirth ++;
		}
	}


	private void CommonStuff () {
	
		//  no mate
		sc_this.mate_go = null;
		//  and go about your business
		sc_this.PickRandomSpot ();
		sc_this.currentActionState_enum = SeaCreature.PossibleStatesOfActions.Moving;
	}

	
	public void GettingToMate_TargetIsDestroyed () {
		
		sc_this.PickRandomSpot ();
		sc_this.currentActionState_enum = SeaCreature.PossibleStatesOfActions.Moving;
	}
}
