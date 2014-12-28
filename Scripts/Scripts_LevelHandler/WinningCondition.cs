using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;


public class WinningCondition : MonoBehaviour {

	public UIScrollView SpawnScrollView;
	string valuesFromTheList = string.Empty;
	public UISprite YoUwon;
	public GameObject YouWon;
	public GameObject youLostMan;
	public UILabel StageVictory;
	public UILabel FinalVictory;
	public UILabel gameOverText;
	public GameObject finalText;
	List <int> numbersOfFishBeforeSimulation = new List <int> ();
	List <int> numbersOfFishAfterSimulation = new List <int> ();


	public int numberOfSpeciesToStartWith = 2;
	public List <string> speciesInOrderOfUnlocking = new List <string> ();

	public GameObject mainGoalObj;

	private bool balanceIsFound = false;
	public UISlider BalanceSlider;

	public float lowestRatio;
	public float highestRatio;
	public float lowExtreme;
	public float highExtreme;

	public Dictionary <string, string> unlockingSpecies = new Dictionary<string, string> ();
	private bool youWonTheGame = false;

	void Awake () {

		unlockingSpecies.Add ("Plankton", "Unlocked");
		unlockingSpecies.Add ("Anchovie", "Unlocked");
		unlockingSpecies.Add ("Coral", "Locked");
		unlockingSpecies.Add ("SeaUrchin", "Locked");
		unlockingSpecies.Add ("SeaWeed", "Locked");
		unlockingSpecies.Add ("Crab", "Locked");
		unlockingSpecies.Add ("Barracouda", "Locked");
	}


	void Start () {

		//  REMEMBER
		speciesInOrderOfUnlocking.Add ("Plankton");
		speciesInOrderOfUnlocking.Add ("Anchovie");
		speciesInOrderOfUnlocking.Add ("Coral");
		speciesInOrderOfUnlocking.Add ("SeaUrchin");
		speciesInOrderOfUnlocking.Add ("SeaWeed");
		speciesInOrderOfUnlocking.Add ("Crab");
		speciesInOrderOfUnlocking.Add ("Barracouda");
//		YoUwon.text = string.Empty;
		NGUITools.SetActive (YouWon, false);
		NGUITools.SetActive (finalText, false);
		NGUITools.SetActive (youLostMan, false);
		BalanceSlider.value = 0f;
	}


	public string IfSpeciesUnlocked (string species) {

		return unlockingSpecies [species];
	}

	
	public void CheckIfLevelIsWon () {

		if (CommonData.tc_scr.existingSpeciesInTheScene.Count == numberOfSpeciesToStartWith)
		{
			//Debug.Log ("At least, I tried = " + numberOfSpeciesToStartWith);

			//  we want to check if it's needed species in the scene
			//  DONT NEED THIS FOR LOOP
			for (int i = 0; i < numberOfSpeciesToStartWith; i ++)
			{
				if (!CommonData.tc_scr.existingSpeciesInTheScene.Contains (speciesInOrderOfUnlocking [i]))
				{
					//  we still need to change the ratio
					ControllingKnob ();
					//Debug.Log ("Go away");
					return;
				}
			}

			CheckingRatioAfterSimulation ();

			balanceIsFound = false;
		}
		//  otherwise no changes in the knob will be registered
		else
		{
			ControllingKnob ();
		}

		if (CommonData.feat_scr.losingCondition_bool == true)
		{
			if (CommonData.feat_scr.numberOfTurns_int == CommonData.feat_scr.numberOfTurnWhenYouAreGoingToLose_int)
			{
				StartCoroutine (YouLost ());
			}
		}

		if (CommonData.countingExistingFish.numberofCarnivores + CommonData.countingExistingFish.numberOfOmnivores +
		    CommonData.countingExistingFish.numberOfHerbivores +
		    CommonData.countingExistingFish.numberOfPlants == 0 || GUI_Main.AvailablePoints < 5)
		{
			//  looooooooooooose
			NGUITools.SetActive (youLostMan, true);
			gameOverText.text = "You lost!";
			StartCoroutine (ResetLevel ());
		}
	}

	IEnumerator ResetLevel () {
		yield return new WaitForSeconds (1);
		Application.LoadLevel (Application.loadedLevel);
	}

	public void ControllingKnob () {

		BalanceSlider.value = 0f;

		List <float> ratioValues_list = new List <float> ();
		foreach (string hunter in CommonData.tc_scr.existingSpeciesInTheScene)
		{
			//  find what this fish eats.
			//  figure out the numbers
			//  add the ratio
			int numberOfHunters = CommonData.fswn_scr.NumberOfExistingFishOfThisType (hunter, CommonData.tc_scr.GetArchetypeOfNeededFish (hunter));
			//  not for 
			List <string> prey_list = CommonData.tc_scr.GetEatableFish (hunter);
			
			if (prey_list.Count > 0)
			{
				foreach (string victim in prey_list)
				{
					//if (CommonData.cs_scr.changeableNumberOfFishToCalcRatio.ContainsKey (victim))
					//{
					int numberOfVictims = CommonData.fswn_scr.NumberOfExistingFishOfThisType (victim, CommonData.tc_scr.GetArchetypeOfNeededFish (victim));
					//Debug.Log ("numberOfVictims = " + numberOfVictims);
					if (numberOfVictims > 0)
					{
						float ratio = (float) numberOfHunters / (float) numberOfVictims;
						//Debug.Log ("localRatio = " + ratio);
						ratio = Mathf.Clamp (ratio, 0f, 1f);
						ratioValues_list.Add (ratio);
					}
					//}
				}
			}
		}
		float finalRatio = 0;
		float sumOfRatios = 0f;


		if (ratioValues_list.Count > 0)
		{
			foreach (float ratio in ratioValues_list) 
			{
				sumOfRatios += ratio;
			}
			finalRatio = sumOfRatios / (float) ratioValues_list.Count;
		///	Debug.Log ("tempRatio = " + finalRatio + ", sumOfRatios = " + sumOfRatios + ", ratioValues_list.Count = " + ratioValues_list.Count);
			if (finalRatio > 1)
			{
				finalRatio = 1;
			}
		//	Debug.Log ("finalRatio = " + finalRatio);
			BalanceSlider.value = finalRatio;
		}
		else
		{
			BalanceSlider.value = 1f;
		}
		//Debug.Log ("Done = " + finalRatio);
	}


	public void CheckingRatioAfterSimulation () {

//		Debug.Log ("I run it, hey");

		List <float> ratioValues_list = new List <float> ();
		foreach (string hunter in CommonData.tc_scr.existingSpeciesInTheScene)
		{
			//  find what this fish eats.
			//  figure out the numbers
			//  add the ratio
			int numberOfHunters = CommonData.fswn_scr.NumberOfExistingFishOfThisType (hunter, CommonData.tc_scr.GetArchetypeOfNeededFish (hunter));
			List <string> prey_list = CommonData.tc_scr.GetEatableFish (hunter);

			if (prey_list.Count > 0)
			{
				foreach (string victim in prey_list)
				{
					if (CommonData.cs_scr.numberOfSpeciesAfter.ContainsKey (victim))
					{
						int numberOfVictims = CommonData.cs_scr.numberOfSpeciesAfter [victim];
						float ratio = (float) numberOfHunters / (float) numberOfVictims;
						ratio = Mathf.Clamp (ratio, 0f, 1f);
//						Debug.Log (hunter + " / " + victim + ": " + ratio);
						ratioValues_list.Add (ratio);
					}
				}
			}
		}

		float sumOfRatios = 0f;

		foreach (float ratio in ratioValues_list) 
		{
			sumOfRatios += ratio;
		}

		float finalRatio = 0;
		finalRatio = sumOfRatios / (float) ratioValues_list.Count;
//		Debug.Log ("WC finalRatio = " + finalRatio);
		if (finalRatio > 1)
		{
			finalRatio = 1;
		}
		//  new position of the knob
		//knob_go.transform.localPosition = new Vector3 (-100 + finalRatio * 200, 0, 0);
		BalanceSlider.value = finalRatio;


		//  if there's at least one ratio that doesn't hit the limits - don't run the victory text

		foreach (float ratio in ratioValues_list) 
		{
			if (ratio < lowExtreme || ratio > highExtreme)
			{
//				Debug.Log ("Smth is wrong with the extremes");
				return;
			}
		}

		if (finalRatio > lowestRatio && finalRatio < highestRatio && CommonData.rae_scr.thereAreOneAndTwoSpeciesRespectively == false)
		{
//			Debug.Log ("GOTCHA!");
			balanceIsFound = true;
			VictoryTextFunction ();
		}
		else
		{
//			Debug.Log ("Smth is wrong with the ratio");
		}


	}


	public void VictoryTextFunction () {

		if (balanceIsFound == true) //&& SummaryCloseWindow.SummaryOkButtonClicked == true)
		{
			SpawnScrollView.MoveRelative (new Vector3( 75,0,0)) ;
			SpawnScrollView.MoveRelative (new Vector3( 75,0,0)) ;

//			Debug.Log ("You won");
			//  you won!
			StageVictory.text = /*"balanced the system for " +*/ numberOfSpeciesToStartWith + " species!";
			if (numberOfSpeciesToStartWith != 7)
			{
				NGUITools.SetActive (YouWon, true);
			}
			else
			{
				NGUITools.SetActive (finalText, true);
				FinalVictory.text = "You balanced\nall species!";
			}
			StartCoroutine (ZeroTextOfWinningCondition ());

			List <string> unlockedSpecies = new List <string> ();

			for (int i = numberOfSpeciesToStartWith; i < numberOfSpeciesToStartWith + 2; i ++)
			{
				// -1
				if (numberOfSpeciesToStartWith <= speciesInOrderOfUnlocking.Count && i < speciesInOrderOfUnlocking.Count)
				{
					string unlockedOne = speciesInOrderOfUnlocking [i];
//					Debug.Log ("unlockedOne = " + unlockedOne + ", i = " + i);
					unlockedSpecies.Add (unlockedOne);
				}
			}
			CommonData.cbv_scr.EnableButtons (unlockedSpecies);
			numberOfSpeciesToStartWith += 2;

			if (numberOfSpeciesToStartWith == 6)
			{
				EasierToHitBalance ();
			}

			//  change later when we have more species
			if (numberOfSpeciesToStartWith > 7)
			{
				numberOfSpeciesToStartWith = 7;
			}
			balanceIsFound = false;

			if (CommonData.feat_scr.losingCondition_bool == true)
			{
				CommonData.feat_scr.numberOfTurnWhenYouAreGoingToLose_int = 10 + CommonData.feat_scr.numberOfTurns_int;
			}
		}
	}

	public IEnumerator CloseBalancedWindow()
	{
//		Debug.Log ("Closing balanced window");
		yield return new WaitForSeconds (4f);
		NGUITools.SetActive (YouWon, false);

	}

	public void EasierToHitBalance () {

		if (CommonData.feat_scr.easierToHitRatio_bool == true)
		{
			lowExtreme -= CommonData.feat_scr.lowerExtremes;
			highExtreme += CommonData.feat_scr.lowerExtremes;
			lowestRatio -= CommonData.feat_scr.lowerRatioBorders;
			highestRatio += CommonData.feat_scr.lowerRatioBorders;
		}
	}


	IEnumerator ZeroTextOfWinningCondition () {

		yield return new WaitForSeconds (4);
//		YoUwon.text = string.Empty;
	}


	void YouWonTheGame () {

		if (numberOfSpeciesToStartWith >= 7)
		{
			//FinalVictory.text = "You balanced all species!";
			youWonTheGame = true;
		}
	}

	IEnumerator YouLost () {

	//	YoUwon.text = "U lost =(";
		yield return new WaitForSeconds (4);
		Application.LoadLevel (Application.loadedLevel);
	}

}
