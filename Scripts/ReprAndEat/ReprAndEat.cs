using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

[RequireComponent(typeof(HUDText))]


public class ReprAndEat : MonoBehaviour {

	public delegate void HuntDownPlant ();
	public static event HuntDownPlant HerbivoresAreGoingToEat;

	public delegate void HuntDownHerbi ();
	public static event HuntDownHerbi OmnivoresAreGoingToEat;

	public delegate void HuntDownOmni ();
	public static event HuntDownOmni CarnivoresAreGoingToEat;

	public delegate void Longevity ();
	public static event Longevity checkIfItsTimeToDie;
	
	public List <GameObject> spreadSheet_targetsToEat_list = new List <GameObject> ();
	public List <GameObject> spreadSheet_parent_list = new List <GameObject> ();

	public List <GameObject> spreadSheet_targetsToEat_list_Checker_list = new List <GameObject> ();
	public List <GameObject> spreadSheet_parent_Checker_list = new List <GameObject> ();

	public bool pleaseStopCallingMe = false;

	public static int pointsForFish = 0;
	public static int pointsForPlants = 0;
	public static int pointsForCarnis = 0;
	public static int totalPoints_int = 0;

	public delegate void ReproduceFish ();
	public static event ReproduceFish HappyReproducing;
	public static event ReproduceFish HappyReproducingPlants;
	public static bool allowedToCleck = true;

	public List <GameObject> eatingPlants = new List <GameObject> ();
	public List <GameObject> eatingHerbivores = new List <GameObject> ();
	public List <GameObject> eatingOmnivores = new List <GameObject> ();

	public List <SeaCreature> newBornFish = new List <SeaCreature> ();

	GameObject spawnFish_go;

	public GameObject buttonsLabel_go;
	public GameObject Playbutton_go;
	public GameObject SummaryWin_go;
	public GameObject LoadingButt_go;

	public Dictionary <string, List <int>> numberOfBornFishPerFish_dict = new Dictionary <string, List<int>> ();

	public int howManyPlantsWillBeReproduced;
	public int howManyChildrenCanBeBornForEachFish;


	TreasureChest trsChest;

	public bool somePlantsWillSurvive;

	public bool thereAreOneAndTwoSpeciesRespectively;
	Tutorial_Ctrl nMC;


	void Start () {

		buttonsLabel_go = GameObject.Find ("SpawningTab");
		trsChest = GameObject.Find ("PlayButton").GetComponent<TreasureChest> ();
		nMC = Camera.main.GetComponent<Tutorial_Ctrl> ();
	}


	void OnClick() {

		if  (CommonData.allowedToClick == true)
		{
			thereAreOneAndTwoSpeciesRespectively = false;
			SummaryCloseWindow.SummaryOkButtonClicked = false;
			CommonData.feat_scr.numberOfTurns_int ++;
			CommonData.cs_scr.CountBeforeTurn();

			if (CommonData.cs_scr.numberOfSpeciesBefore.ContainsValue (1))
			{
				thereAreOneAndTwoSpeciesRespectively = true;
			}

			pleaseStopCallingMe = false;
			RunSimulation ();
			CommonData.allowedToClick = false;
			NGUITools.SetActive (GameObject.Find ("SpawningTab"), false);
			NGUITools.SetActive (LoadingButt_go,true);
			StartCoroutine (RunSpreadSheet ());
		}
	}

	void Update () {
		//  click only when the simulation is running
		if (Input.GetKeyDown (KeyCode.Space))
		{
			SpreadSheet ();
		}
	}


	IEnumerator RunSpreadSheet () {
		yield return new WaitForSeconds (7);
//		Debug.Log ("SPREADSHEET!!!!!");
		SpreadSheet ();

		if (spreadSheet_targetsToEat_list_Checker_list.Count > 0)
		{
			//  we destroy them and run the reproduction
			foreach (GameObject go in spreadSheet_targetsToEat_list)
			{
				//  since we remove everything from the checker list
				if (go != null && spreadSheet_targetsToEat_list_Checker_list.Contains (go))
				{
//					Debug.Log ("SPARTA!!!!! = " + go.name);
					Destroy (go);
				}
			}
			yield return new WaitForSeconds (0.1f);
			RunReproduction ();
			//  if the turn is not finished
			if (pleaseStopCallingMe == false && spreadSheet_parent_Checker_list.Count > 0)
			{
				foreach (GameObject go in spreadSheet_parent_list)
				{
					if (spreadSheet_parent_Checker_list.Contains (go))
					{
	//					Debug.Log (" =) go.name = " + go.name);
						GettingToMate gtm_ch = go.GetComponent <GettingToMate> ();
						SeaCreature sc_ch = go.GetComponent <SeaCreature> ();
						//  the states have to be correct - mate/getting to mate!
						gtm_ch.Reproduction_Checker (sc_ch.child_go);
					}
				}
			}
		}
		//  else we are in the middle of the reproduction cicle, so we just need to calculate stuff
		else
		{
			if (pleaseStopCallingMe == false)
			{
				foreach (GameObject go in spreadSheet_parent_list)
				{
					if (spreadSheet_parent_Checker_list.Contains (go))
					{
	//					Debug.Log (" =( go.name = " + go.name);
						GettingToMate gtm_ch = go.GetComponent <GettingToMate> ();
						SeaCreature sc_ch = go.GetComponent <SeaCreature> ();
						//  the states have to be correct - mate/getting to mate!
						gtm_ch.Reproduction_Checker (sc_ch.child_go);
					}
				}
			}
		}
		
		//  we should not call "turn is finished" when there's only ONE representative
		//  if it wasn't run!!!!!!!!
		
		
		//  plants will be reproduced
		if (pleaseStopCallingMe == false)
		{
			TurnIsFinished ();
		}

	}


	void SpreadSheet () {
		
		StopCoroutine ("WaitToLetObjectsWithNoFoodGetDestroyed");
		//  finish the eating circle
		
		//CommonData.cs_scr.CountBeforeTurn();

		//  if there are some objects to be destroyed

	}


	public void RunSimulation () {

		if (pleaseStopCallingMe == false)
		{
			eatingPlants.Clear ();
			eatingHerbivores.Clear ();
			eatingOmnivores.Clear ();
			newBornFish.Clear ();
			spreadSheet_targetsToEat_list.Clear ();
			spreadSheet_parent_list.Clear ();

			spreadSheet_targetsToEat_list_Checker_list.Clear ();
			spreadSheet_parent_Checker_list.Clear ();


			CommonData.SetNumbersOfFish ();

			//  if there's at least some fish other than Plants, we have to run this function, otherwise - Reproduce
			if (HerbivoresAreGoingToEat != null || OmnivoresAreGoingToEat != null || CarnivoresAreGoingToEat != null)
			{
				//  apparently, I just have to use Coroutines -___-
				StartCoroutine (WaitToLetObjectsWithNoFoodGetDestroyed ());
				//  but if everything is zero (3C and 1P - then we have to run the same if statement)
			}
			else
			{
				//  in case there are only plants, we can easily reproduce all the plants and then we would allow a player to click on the buttons
				//Debug.Log ("2!");
				TurnIsFinished ();
			}
		}
	}


	IEnumerator WaitToLetObjectsWithNoFoodGetDestroyed () {

		ThereHaveToBeSomeSurvivorsAmongPlants ();
		yield return null;
		DecreaseAmountOfFoodToBeEaten ();
		yield return null;

		if (HerbivoresAreGoingToEat != null)
		{
			HerbivoresAreGoingToEat ();
		}
		yield return new WaitForSeconds (0.03f);
		if (OmnivoresAreGoingToEat != null)
		{
			OmnivoresAreGoingToEat ();
		}
		yield return new WaitForSeconds (0.03f);
		if (CarnivoresAreGoingToEat != null)
		{
			CarnivoresAreGoingToEat ();
		}



	//	CommonData.numberOfHerbivoresEatingPlants_int = eatingPlants.Count;
	//	CommonData.numberOfOmnivoresEatingHerbivores_int = eatingHerbivores.Count;
	//	CommonData.numberOfCarnivoresEatingOmnivores_int = eatingOmnivores.Count;
		
		//			Debug.Log (CommonData.numberOfHerbivoresEatingPlants_int + ", " +CommonData.numberOfOmnivoresEatingHerbivores_int + ", " +
		//			           CommonData.numberOfOmnivoresEatingHerbivores_int);
		
		if (eatingPlants.Count > 0)
		{
			foreach (GameObject go in eatingPlants)
			{
				LookingForFood lff = go.GetComponent <LookingForFood> ();
				lff.ActionToCall (0);
			}
		}
		else if (eatingHerbivores.Count > 0) 
		{
			LetOmnivoresEat ();
		}
		else if (eatingOmnivores.Count > 0)
		{
			LetCarnivoresEat ();
		}
		else
		{
			//  in case there are only plants, we can easily reproduce all the plants and then we would allow a player to click on the buttons
			//Debug.Log ("1!");
			TurnIsFinished ();
		}
	}


	private void DecreaseAmountOfFoodToBeEaten () {

		if (CommonData.feat_scr.eatLessWhenMany_bool == true)
		{
			foreach (string availableFish in CommonData.tc_scr.existingSpeciesInTheScene)
			{
				//Debug.Log ("availableFish = " + availableFish + ", type = " + CommonData.tc_scr.GetArchetypeOfNeededFish (availableFish));
				if (CommonData.tc_scr.GetArchetypeOfNeededFish (availableFish) == "Herbivore")
				{
					//Debug.Log ("H = " + CommonData.fswn_scr.NumberOfExistingFishOfThisType (availableFish, "Herbivore"));
					if (CommonData.fswn_scr.NumberOfExistingFishOfThisType (availableFish, "Herbivore") >= CommonData.feat_scr.herbivoresUpLimit)
					{
						List <GameObject> neededSpecies = CommonData.fswn_scr.ReturnSpeciesOfThisType (availableFish, "Herbivore");
						int numberOfFishToStopEating = System.Convert.ToInt32 (CommonData.feat_scr.herbivoresUpLimit * 0.3f);
//						Debug.Log ("How many H = " + numberOfFishToStopEating);
						
						for (int i = 0; i < numberOfFishToStopEating; i++)
						{
							SeaCreature sc_toStopEating = neededSpecies [i].GetComponent <SeaCreature> ();
							sc_toStopEating.currentActionState_enum = SeaCreature.PossibleStatesOfActions.SaveSomeFood;
						}
					}
				}
				else if (CommonData.tc_scr.GetArchetypeOfNeededFish (availableFish) == "Omnivore")
				{
					//Debug.Log ("O = " + CommonData.fswn_scr.NumberOfExistingFishOfThisType (availableFish, "Omnivore"));
					if (CommonData.fswn_scr.NumberOfExistingFishOfThisType (availableFish, "Omnivore") >= CommonData.feat_scr.omnivoresUpLimit)
					{
						List <GameObject> neededSpecies = CommonData.fswn_scr.ReturnSpeciesOfThisType (availableFish, "Omnivore");
						int numberOfFishToStopEating = System.Convert.ToInt32 (CommonData.feat_scr.omnivoresUpLimit * 0.3f);
//						Debug.Log ("How many O = " + numberOfFishToStopEating);
						
						for (int i = 0; i < numberOfFishToStopEating; i++)
						{
							SeaCreature sc_toStopEating = neededSpecies [i].GetComponent <SeaCreature> ();
							sc_toStopEating.currentActionState_enum = SeaCreature.PossibleStatesOfActions.SaveSomeFood;
						}
					}
				}
				else if (CommonData.tc_scr.GetArchetypeOfNeededFish (availableFish) == "Carnivore")
				{
					//Debug.Log ("C = " + CommonData.fswn_scr.NumberOfExistingFishOfThisType (availableFish, "Carnivore"));
					if (CommonData.fswn_scr.NumberOfExistingFishOfThisType (availableFish, "Carnivore") >= CommonData.feat_scr.carnivoresUpLimit)
					{
						List <GameObject> neededSpecies = CommonData.fswn_scr.ReturnSpeciesOfThisType (availableFish, "Carnivore");
						int numberOfFishToStopEating = System.Convert.ToInt32 (CommonData.feat_scr.carnivoresUpLimit * 0.3f);
//						Debug.Log ("How many C = " + numberOfFishToStopEating);
						
						for (int i = 0; i < numberOfFishToStopEating; i++)
						{
							SeaCreature sc_toStopEating = neededSpecies [i].GetComponent <SeaCreature> ();
							sc_toStopEating.currentActionState_enum = SeaCreature.PossibleStatesOfActions.SaveSomeFood;
						}
					}
				}
			}
		}
	}


	void ThereHaveToBeSomeSurvivorsAmongPlants () {

		if (somePlantsWillSurvive == true) 
		{

			List <string> plantsThatWillSurvive = new List <string> ();

			foreach (string availableFish in CommonData.tc_scr.existingSpeciesInTheScene)
			{
				if (CommonData.tc_scr.GetArchetypeOfNeededFish (availableFish) == "Plant")
				{
					plantsThatWillSurvive.Add (availableFish);
				}

				if (plantsThatWillSurvive.Count > 0)
				{
					foreach (string survivingPlant in plantsThatWillSurvive)
					{
						if (CommonData.fswn_scr.NumberOfExistingFishOfThisType (survivingPlant, "Plant") >= 1)
						{
							List <GameObject> survivingPlantsOfThisType = CommonData.fswn_scr.ReturnSpeciesOfThisType (survivingPlant, "Plant");

							for (int i = 0; i < 1; i++)
							{
								GameObject go = survivingPlantsOfThisType [i];
								SeaCreature sc_plant_survive = go.GetComponent <SeaCreature> ();
								sc_plant_survive.currentActionState_enum = SeaCreature.PossibleStatesOfActions.WillBeSaved;
							}
						}
					}
				}
			}
		}
	}


	public void RunReproduction () {
//		Debug.Log ("What produced?");
		numberOfBornFishPerFish_dict.Clear ();

		List <string> fishNeededMatesToReproduce_list = new List <string> ();

		foreach (string fish in CommonData.tc_scr.existingSpeciesInTheScene)
		{
			if (CommonData.tc_scr.GetArchetypeOfNeededFish (fish) != "Plant")
			{
				fishNeededMatesToReproduce_list.Add (fish);
			}
		}

		//  either only plants or nothing is here
		if (fishNeededMatesToReproduce_list.Count == 0)
		{
			TurnIsFinished ();
		}
		else
		{
			//  since it's static, don't forget to set it to zero
			Reproduction.numberOfMatingPairs_dict.Clear ();
			//  Repr dictionary has to know what species are in the scene!
			foreach (KeyValuePair <string, int> pair in CommonData.cs_scr.numberOfSpeciesBefore)
			{
				//  and then we populate the dict with names of existing fish in the scene
				//  if there're already 2 mating pairs, we cannon reproduce
				Reproduction.numberOfMatingPairs_dict.Add (pair.Key, 0);
			}

			//  for each fish that needs a mate to be able to reproduce
			foreach (string fish in fishNeededMatesToReproduce_list)
			{
				//  we need to see how much prey exists in the scene
				List <string> eatableFishForThisOne_list = CommonData.tc_scr.GetEatableFish (fish);
				//  we need to see if eatable food IS in the scene
				List <string> eatableFishForThisOneInTheScene_list = new List <string> ();

				foreach (string eatableFish in eatableFishForThisOne_list)
				{
					if (CommonData.tc_scr.existingSpeciesInTheScene.Contains (eatableFish))
					{
						eatableFishForThisOneInTheScene_list.Add (eatableFish);
					}
				}

				//  if it has fish to hunt down, then work!
				if (eatableFishForThisOneInTheScene_list.Count > 0)
				{
					int numberOfFood = 0;
					foreach (string eatableFish in eatableFishForThisOneInTheScene_list)
					{
						numberOfFood += CommonData.fswn_scr.NumberOfExistingFishOfThisType (eatableFish, CommonData.tc_scr.GetArchetypeOfNeededFish (eatableFish));
					}
//					Debug.Log (fish + ", number of food = " + numberOfFood);
					if (numberOfFood > 0)
					{
						int numberOfThisFish = CommonData.fswn_scr.NumberOfExistingFishOfThisType (fish, CommonData.tc_scr.GetArchetypeOfNeededFish (fish));
//						Debug.Log ("numberOfThisFish = " + numberOfThisFish);
						//  what if there's only 1A and 1B? Then we need to somehow figure out the best way to call "TIF". Some variable? if it's zero
						//  at the end of the loop - run "TIF"
						if (numberOfThisFish > 1 && numberOfFood > numberOfThisFish)
						{
							List <int> numberOfNewBornFishPerFather_list = new List <int> ();
							//  if there's enough food
							if ( Mathf.FloorToInt ((numberOfFood - numberOfThisFish) / 2) >= 1)
							{
								//  if there're at least 4 parents, we need only two of them to reproduce
								//  crabs reproduce less
								if (fish != "Crab")
								{
									if (numberOfThisFish >= 4)
									{
	//									Debug.Log ("4 par");
										numberOfNewBornFishPerFather_list.Add (howManyChildrenCanBeBornForEachFish / 2);
										numberOfNewBornFishPerFather_list.Add (howManyChildrenCanBeBornForEachFish - howManyChildrenCanBeBornForEachFish / 2);
									}
									//  if there're only 2-3 parents
									else if (numberOfFood >= 2)
									{
//										Debug.Log ("2 par");
										numberOfNewBornFishPerFather_list.Add (howManyChildrenCanBeBornForEachFish);
									}
								}
								else
								{
									numberOfNewBornFishPerFather_list.Add (1);
								}
							}
	//							foreach (int i in numberOfNewBornFishPerFather_list)
	//							{
	//								Debug.Log ("i = " + i);
	//							}
//								Debug.Log ("fish = " + fish);
							numberOfBornFishPerFish_dict.Add (fish, numberOfNewBornFishPerFather_list);
						}
					}
				}
			}
		}

		//  the problem here is that the dictionary is almost never empty and some keys can contain ZEROED lists
		//  remove those keys so that therer would not be any errors!
		List <string> keysToRemove = new List <string> ();

		if (numberOfBornFishPerFish_dict.Count > 0)
		{
			foreach (KeyValuePair <string, List <int>> pair in numberOfBornFishPerFish_dict)
			{
				if (pair.Value.Count == 0)
				{
					keysToRemove.Add (pair.Key);
					//numberOfBornFishPerFish_dict.Remove (pair.Key);
				}
			}
		}

		if (keysToRemove.Count > 0)
		{
			foreach (string removeable in keysToRemove)
			{
//				Debug.Log ("removeable = " + removeable);
				numberOfBornFishPerFish_dict.Remove (removeable);
			}
		}

		//  if there's at least one type of fish that needs to reproduce
		if (numberOfBornFishPerFish_dict.Count > 0)
		{
			if (HappyReproducing != null && HappyReproducing.GetInvocationList ().Count () > 1)
			{
//				Debug.Log ("SMTH happens");
				HappyReproducing ();
			}
		}
		//  otherwise TIF
		else
		{
//			Debug.Log ("Enough");
			TurnIsFinished ();
		}
	
	}


	public void LetOmnivoresEat () {

		foreach (GameObject go in eatingHerbivores)
		{
			LookingForFood lff = go.GetComponent <LookingForFood> ();
			lff.ActionToCall (0);
		}
	}


	public void LetCarnivoresEat () {

		foreach (GameObject go in eatingOmnivores)
		{
			LookingForFood lff = go.GetComponent <LookingForFood> ();
			lff.ActionToCall (0);
		}
	}

	
	public void TurnIsFinished () {
		pleaseStopCallingMe = true;
		StopCoroutine ("RunSpreadSheet");
		//  have to wait. Otherwise some species won't be counted
		StartCoroutine (WaitALittleBit ());
	}





	IEnumerator WaitALittleBit () {

		yield return new WaitForSeconds (0.1f);
		//  always set it to zero
		Coral.howManyPlantsReproduced = 0;
		SeeWeed.howManyPlantsReproduced = 0;
		Plankton.howManyPlantsReproduced = 0;
//		Debug.Log ("Cryyy, cryyyyyyy!");
		if (HappyReproducingPlants != null)
		{
			HappyReproducingPlants ();
		}
		yield return new WaitForSeconds (0.1f);
//		Debug.Log ("Once");
		if (newBornFish.Count > 0)
		{
			foreach (SeaCreature sc in newBornFish)
			{
				sc.currentActionState_enum = SeaCreature.PossibleStatesOfActions.Moving;
			}
		}


		


		yield return null;

		if (CommonData.feat_scr.enableLifespan_bool == true)
		{
			if (checkIfItsTimeToDie != null)
			{
				checkIfItsTimeToDie ();
			}
		}
		yield return new WaitForSeconds (0.3f);
		CommonData.countingExistingFish.CountNumberOfExistingFish ();
		CommonData.ib_scr.RefreshBars ();



		yield return new WaitForSeconds (0.05f);

		List <GameObject> seaCreatures_list = new List<GameObject> ();
		seaCreatures_list.AddRange (FindAllSeaCreatures ("Plant"));
		seaCreatures_list.AddRange (FindAllSeaCreatures ("Herbivore"));
		seaCreatures_list.AddRange (FindAllSeaCreatures ("Omnivore"));
		seaCreatures_list.AddRange (FindAllSeaCreatures ("Carnivore"));

		foreach (GameObject go in seaCreatures_list)
		{
			if (go.GetComponent <SeaCreature> () != null && go != null)
			{
				SeaCreature sc = go.GetComponent <SeaCreature> ();
				sc.BackToNormal ();
			}
		}

		GivePoints ();
	}

	List <GameObject> FindAllSeaCreatures (string tagToUse) {

		return GameObject.FindGameObjectsWithTag (tagToUse).ToList ();
	}



	void GivePoints () {

		totalPoints_int = 0;

		foreach (string fish in CommonData.tc_scr.existingSpeciesInTheScene)
		{
			string tagOfFish = CommonData.tc_scr.GetArchetypeOfNeededFish (fish);
			int gottenValue = 0;

			if (tagOfFish == "Plant")
			{
				totalPoints_int += 1 * CommonData.fswn_scr.NumberOfExistingFishOfThisType (fish, tagOfFish);
			}
			else if (tagOfFish == "Herbivore")
			{
				totalPoints_int += 3 * CommonData.fswn_scr.NumberOfExistingFishOfThisType (fish, tagOfFish);
			}
			else if (tagOfFish == "Omnivore")
			{
				totalPoints_int += 5 * CommonData.fswn_scr.NumberOfExistingFishOfThisType (fish, tagOfFish);
			}
			else if (tagOfFish == "Carnivore")
			{
				totalPoints_int += 7 * CommonData.fswn_scr.NumberOfExistingFishOfThisType (fish, tagOfFish);
			}
		}

		//		Debug.Log ("Total points = " + totalPoints_int );
//		GUI_Main.AvailablePoints += totalPoints_int;
//		HUDText RecievedPoints= GetComponent<HUDText> ();
//		RecievedPoints.Add ("You Received " + totalPoints_int + " for all the living Creatures",new Color32 (22,214,86,0), 2.0f);
//		NGUITools.SetActive (SummaryWin_go,true);

		//instantiate the GUI treasure chest
		trsChest.Chest_GUI_Pref.SetActive (true);
		
		CommonData.cs_scr.CountAfterTurn ();
		//  show the labels and let the player click
		CommonData.allowedToClick = true;
		NGUITools.SetActive (buttonsLabel_go, true);
		NGUITools.SetActive (LoadingButt_go,false);
		
		CommonData.mg_scr.CheckMiniGoals ();
		if (Tutorial_Ctrl.move6 && !Tutorial_Ctrl.move7){
			nMC.FindNextStep();
		}
	}
	void OnTooltip (bool show)
	{
		UITooltip.ShowText ("Play");
	}

}
