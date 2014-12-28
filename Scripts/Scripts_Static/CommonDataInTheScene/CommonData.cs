using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public static class CommonData {

	public static List <string> allTheSpecies_list = new List <string> {
		"Carnivore", "Omnivore", "Herbivore", "Plant"
	};
	public static ListOfPrefabs listOfPrefabs_scr;
	public static List <GameObject> listOfSpeciesPrefabs_list;
	public static GameObject spawnFishLabel_go;

	public static CountingExistingFish countingExistingFish;
	public static FindSpeciesWeNeed fswn_scr;
	public static SortingFish sf_scr;
	public static ReprAndEat rae_scr;
	public static WinningCondition wc_scr;
	public static MiniGoals mg_scr;
	public static TreeCustom tc_scr;
	public static LineRendererController lrc_scr;
	public static CountingSpecies cs_scr;
	public static IndividualBars ib_scr;
	public static ControllingButtonsVisibility cbv_scr;
	public static Features feat_scr;


	public static int numberOfHerbivoresEatingPlants_int = 0;
	public static int numberOfHerbivoresThatAtePlants_int = 0;

	public static int numberOfOmnivoresEatingHerbivores_int = 0;
	public static int numberOfOmnivoresThatAteHerbivores_int = 0;

	public static int numberOfCarnivoresEatingOmnivores_int = 0;
	public static int numberOfCarnivoresThatAteOmnivores_int = 0;

	public static int numberOfFishGettingToMate = 0;
	public static int numberOfFishGivenBirth = 0;

	public static bool allowedToClick = true;
	public static int numberOfClicks = 0;
	public static bool allowedToRunSimulation = false;

	public static int numberOfTurn_int = 0;

	public static Dictionary <string, int> controllingNumberOfSpecies = new Dictionary <string, int> ();

	public static void FindNeededGameObjects () {

		spawnFishLabel_go = GameObject.Find ("SpawningTabTab");
	}


	public static void FindListOfPrefabs () {

		listOfPrefabs_scr = GameObject.Find ("ListOfGameObjectsWhichAreChildren").GetComponent <ListOfPrefabs> ();
		listOfSpeciesPrefabs_list = listOfPrefabs_scr.species_list;
	}


	public static void FindCountingExistingFish () {

		GameObject countingExistingFish_go = GameObject.FindGameObjectWithTag ("countingExistingFish");
		countingExistingFish = countingExistingFish_go.GetComponent <CountingExistingFish> ();
	}


	public static void GettingNeededScripts () {

		fswn_scr = GameObject.Find ("FindingNeededSpecies").GetComponent <FindSpeciesWeNeed> ();
		sf_scr = GameObject.Find ("SortingFish").GetComponent <SortingFish> ();
		rae_scr = GameObject.Find ("PlayButton").GetComponent <ReprAndEat> ();
		wc_scr = GameObject.Find ("WinningCondition").GetComponent <WinningCondition> ();
		mg_scr = GameObject.Find ("MiniGoals").GetComponent <MiniGoals> ();
		cs_scr = Camera.main.GetComponent <CountingSpecies> ();
		tc_scr = GameObject.Find ("Tree").GetComponent <TreeCustom> ();
		lrc_scr = GameObject.Find ("LineRendererController").GetComponent <LineRendererController> ();
		ib_scr = GameObject.Find ("ControllingRatios").GetComponent <IndividualBars> ();
		cbv_scr = GameObject.Find ("ControllingButtonsVisibility").GetComponent <ControllingButtonsVisibility> ();
		feat_scr = GameObject.Find ("Features").GetComponent <Features> ();
	}


	public static void SetToDefault () {

		numberOfTurn_int = 0;
		allowedToClick = true;
		allowedToRunSimulation = false;
		numberOfClicks = 0;
		SetNumbersOfFish ();
	}


	public static void SetNumbersOfFish () {

		numberOfCarnivoresEatingOmnivores_int = 0;
		numberOfCarnivoresThatAteOmnivores_int = 0;

		numberOfHerbivoresEatingPlants_int = 0;
		numberOfHerbivoresThatAtePlants_int = 0;

		numberOfOmnivoresEatingHerbivores_int = 0;
		numberOfOmnivoresThatAteHerbivores_int = 0;

		numberOfFishGettingToMate = 0;
		numberOfFishGivenBirth = 0;

		CommonData.numberOfFishGettingToMate = 0;

	}

	public static void ControlNumberOfFish () {

		controllingNumberOfSpecies.Clear ();
		controllingNumberOfSpecies.Add ("Plant", 50);
		controllingNumberOfSpecies.Add ("Herbivore", 30);
		controllingNumberOfSpecies.Add ("Omnivore", 20);
		controllingNumberOfSpecies.Add ("Carnivore", 10);
	}
}
