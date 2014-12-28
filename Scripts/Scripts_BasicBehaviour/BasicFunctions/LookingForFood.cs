using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class LookingForFood : MonoBehaviour {


	SeaCreature sc_this;
	GettingSeaCreatureScript getSCScript;
	public List <GameObject> fishThatIsPossibleToHuntDown = new List <GameObject> ();
	GameObject targetToReach_go;

	public int numberOfFishThisOneNeedsToEat_int;
	List <GameObject> targetsToEat_list = new List<GameObject> ();

	public bool didntEatLastTime_bool = false;

	void Start () {
		
		sc_this = this.GetComponent <SeaCreature> ();
		getSCScript = this.GetComponent <GettingSeaCreatureScript> ();
	}


	//  Find a target to eat
	public void LookingForSpeciesToEat () {

		//  each time we start the turn, we have to clear the list
		targetsToEat_list.Clear ();

		if (sc_this.currentActionState_enum == SeaCreature.PossibleStatesOfActions.SaveSomeFood)
		{
			if (didntEatLastTime_bool == false)
			{
				didntEatLastTime_bool = true;
				return;
			}
			else if (CommonData.feat_scr.dieIfDidntEatTwoRoundsWheneatLessWhenManyIsEnabled_bool == true)
			{
//				Debug.Log ("Just to check...");
				StartCoroutine (sc_this.DieFish (0.01f));
				return;
			}
		}

		if (numberOfFishThisOneNeedsToEat_int > 0)
		{
			//  we find all the needed fish within those archetypes
			for (int i = 0; i < numberOfFishThisOneNeedsToEat_int; i ++)
			{
				List <GameObject> findEatableFish = CommonData.fswn_scr.CountExistingFood (sc_this.possibleFoodArchetypes, sc_this.possibleFood);

				//  there have to be some survivors!!!!


				//Debug.Log ("1_");
				//  nothing? then stop executing
				if (findEatableFish.Count == 0)
				{
					//  no food? sorry, you have to die
					//RemoveObjectFromRespectiveList ();
					sc_this.targetSpot_vector3 = this.transform.position + Vector3.up * 2;
					this.gameObject.tag = "DeadFish";
					sc_this.currentActionState_enum = SeaCreature.PossibleStatesOfActions.Dead;
					StartCoroutine (sc_this.DieFish (0.5f));
					return;
				}
				//Debug.Log ("2_");
				//  we've found all the species we can eat
				//  now we want to see of some of them are being hunt down already.
				
				fishThatIsPossibleToHuntDown.Clear ();
				//  and that's the point - plants now is not a SeaCreature script, 
				//  which means it has to inherit from SeaCreture class anyway, but doesn't implement its functions
				foreach (GameObject go in findEatableFish) 
				{
					SeaCreature sc_possible = getSCScript.GetSeaCreatureScript (go);
					if (sc_possible.beingHuntDown == false && sc_possible.currentActionState_enum != SeaCreature.PossibleStatesOfActions.WillBeSaved
					    && sc_possible.currentActionState_enum != SeaCreature.PossibleStatesOfActions.WillDieNextRound)
					{
						fishThatIsPossibleToHuntDown.Add (go);
					}
				}

		//		Debug.Log (name + ", fishThatIsPossibleToHuntDown = " + fishThatIsPossibleToHuntDown.Count);

				//  nothing? then stop executing
				if (fishThatIsPossibleToHuntDown.Count == 0)
				{
					//  no food? sorry, you have to die
					//RemoveObjectFromRespectiveList ();
					sc_this.targetSpot_vector3 = this.transform.position + Vector3.up * 2;
					this.gameObject.tag = "DeadFish";
					sc_this.currentActionState_enum = SeaCreature.PossibleStatesOfActions.Dead;
					StartCoroutine (sc_this.DieFish (0.5f));
					return;
				}
				//Debug.Log ("3_");
				//  sort fish to choose the closest one
				fishThatIsPossibleToHuntDown = CommonData.sf_scr.SortFish (fishThatIsPossibleToHuntDown);

				if (fishThatIsPossibleToHuntDown.Count > 0)
				{

					targetToReach_go = fishThatIsPossibleToHuntDown [0].transform.gameObject;
					fishThatIsPossibleToHuntDown.Clear ();
					
					//  Spread sheet
					CommonData.rae_scr.spreadSheet_targetsToEat_list.Add (targetToReach_go);
					CommonData.rae_scr.spreadSheet_targetsToEat_list_Checker_list.Add (targetToReach_go);
					//  only in this case we will increase this number!
					if (this.tag == "Herbivore" && i == numberOfFishThisOneNeedsToEat_int - 1)
					{
						CommonData.numberOfHerbivoresEatingPlants_int += numberOfFishThisOneNeedsToEat_int;
						//Debug.Log ("CommonData.numberOfHerbivoresEatingPlants_int = " + CommonData.numberOfHerbivoresEatingPlants_int);
						if (!CommonData.rae_scr.eatingPlants.Contains (this.gameObject))
						{
							CommonData.rae_scr.eatingPlants.Add (this.gameObject);
						}
					}
					else if (this.tag == "Omnivore" && i == numberOfFishThisOneNeedsToEat_int - 1)
					{
						CommonData.numberOfOmnivoresEatingHerbivores_int += numberOfFishThisOneNeedsToEat_int;
						if (!CommonData.rae_scr.eatingHerbivores.Contains (this.gameObject))
						{
							CommonData.rae_scr.eatingHerbivores.Add (this.gameObject);
						}
					}
					else if (this.tag == "Carnivore" && i == numberOfFishThisOneNeedsToEat_int - 1)
					{
						CommonData.numberOfCarnivoresEatingOmnivores_int += numberOfFishThisOneNeedsToEat_int;
						if (!CommonData.rae_scr.eatingOmnivores.Contains (this.gameObject))
						{
							CommonData.rae_scr.eatingOmnivores.Add (this.gameObject);
						}
					}

					if (targetToReach_go.tag == "Omnivore" || 
					    targetToReach_go.tag == "Herbivore") 
					{
						//  later we will have to figure out the exact species, not the archetype
						LookingForFood_MainAction (targetToReach_go.gameObject);
					}
					else if (targetToReach_go.tag == "Plant") 
					{
						SeaCreature sc_TargetToEat = getSCScript.GetSeaCreatureScript (targetToReach_go);
						sc_TargetToEat.beingHuntDown = true;
						sc_this.Speed_prop =  sc_this.SpeedInit_prop + sc_this.SpeedInit_prop / 3;  //  + 30%
						//sc_this.huntItDown_go = targetToReach_go;
						targetsToEat_list.Add (targetToReach_go);
					}
				//	Debug.Log ("4_");
				}
			}
		}
	}


	private void RemoveObjectFromRespectiveList () {

		if (this.tag == "Herbivore" && CommonData.rae_scr.eatingPlants.Contains (this.gameObject))
		{
//			Debug.Log ("I removed");
			CommonData.rae_scr.eatingPlants.Remove (this.gameObject);
		}
		else if (this.tag == "Omnivore" && CommonData.rae_scr.eatingHerbivores.Contains (this.gameObject))
		{
			CommonData.rae_scr.eatingHerbivores.Remove (this.gameObject);
		}
		else if (this.tag == "Carnivore" && CommonData.rae_scr.eatingOmnivores.Contains (this.gameObject))
		{
			CommonData.rae_scr.eatingOmnivores.Remove (this.gameObject);
		}
	}


	public void ActionToCall (int i) {

		sc_this.huntItDown_go = targetsToEat_list [i];
		SeaCreature sc_victim = sc_this.huntItDown_go.GetComponent <SeaCreature> ();
//		Debug.Log ("sc_this.nameOfSpecies = " + sc_this.nameOfSpecies);
		if (sc_this.currentActionState_enum != SeaCreature.PossibleStatesOfActions.Chasing)
		{
//			Debug.Log ("Get recked!!!!");
			sc_this.currentActionState_enum = SeaCreature.PossibleStatesOfActions.Chasing;
		}

		string keywordEvent = sc_this.nameOfSpecies + "_Eats_" + sc_victim.nameOfSpecies + "_";
		CommonData.mg_scr.WriteDownEvent (keywordEvent);
		CommonData.mg_scr.CheckMiniGoals ();
	}


	void LookingForFood_MainAction (GameObject targetToEat) {

		targetsToEat_list.Add (targetToEat);
		//sc_this.huntItDown_go = targetToEat;
		//Debug.Log (targetToEat.name);
		SeaCreature sc_TargetToEat = getSCScript.GetSeaCreatureScript (targetToEat);
		sc_TargetToEat.beingHuntDown = true;
		//  just in case, no point in getting to a mate in case you are hunt down
		sc_TargetToEat.mate_go = null;
		//  since we changed a reference, we are talking to ANOTHER script
		//  if chasing - change state and increase the speed, ONLY IN CASE we've found a fish to hunt down

		sc_this.Speed_prop =  sc_this.SpeedInit_prop + sc_this.SpeedInit_prop / 3;  //  + 30%

	}
}
