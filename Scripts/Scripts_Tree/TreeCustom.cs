using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class TreeCustom : MonoBehaviour {


	public List <string> existingSpeciesInTheScene = new List <string> ();

	//  everywhere we get access by key!!!!

	//  we want to know the archetypes of fish to be able to position objects on different levels
	public Dictionary <string, string> archetypesOfFish_dict = new Dictionary <string, string> ();
	//  we want to know what archetypes a fish can eat (save resources)
	public Dictionary <string, List <string>> eatableArchetypes_dict = new Dictionary <string, List<string>> ();
	//  what fish a fish eats
	public Dictionary <string, List <string>> eatableFish_dict = new Dictionary <string, List<string>> (); 

	private List <string> emptyList_list = new List <string> ();

	public List <GameObject> minimapSpawnerCarni = new List <GameObject> ();
	public List <GameObject> minimapSpawnerHerbi = new List <GameObject> ();
	public List <GameObject> minimapSpawnerOmni = new List <GameObject> ();
	public List <GameObject> minimapSpawnerPlant = new List <GameObject> ();
	//public List <GameObject> minimapPrefabs = new List <GameObject> ();

	public List <GameObject> plantMinimapIcons = new List <GameObject> ();
	public List <GameObject> herbivoreMinimapIcons = new List <GameObject> ();
	public List <GameObject> omnivoreMinimapIcons = new List <GameObject> ();
	public List <GameObject> carnivoreMinimapIcons = new List <GameObject> ();


	public GameObject treeFishHolder;

	LineRendererController lrc_scr;

	void Awake (){

		//  REMEMBER
		string barracouda = "Barracouda";
		AddArchetypesOfFish (barracouda, "Carnivore");
		AddEatableArchetypes (barracouda, "Herbivore");
		AddEatableFish (barracouda, "Anchovie");

		string crab = "Crab";
		AddArchetypesOfFish (crab, "Omnivore");
		AddEatableArchetypes (crab, "Plant");
		AddEatableFish (crab, "SeaWeed");

		string anchovie = "Anchovie";
		AddArchetypesOfFish (anchovie, "Herbivore");
		AddEatableArchetypes (anchovie, "Plant");
		AddEatableFish (anchovie, "Plankton");

		string plankton = "Plankton";
		AddArchetypesOfFish (plankton, "Plant");

		string seaWeed = "SeaWeed";
		AddArchetypesOfFish (seaWeed, "Plant");

		string coral = "Coral";
		AddArchetypesOfFish (coral, "Plant");

		string seaUrchin = "SeaUrchin";
		AddArchetypesOfFish (seaUrchin, "Herbivore");
		AddEatableArchetypes (seaUrchin, "Plant");
		AddEatableFish (seaUrchin, "Coral");

		minimapSpawnerCarni = GameObject.FindGameObjectsWithTag ("CarnivoreSpawners").ToList ();
		minimapSpawnerOmni = GameObject.FindGameObjectsWithTag ("OmnivoreSpawners").ToList ();
		minimapSpawnerHerbi =  GameObject.FindGameObjectsWithTag ("HerbivoreSpawners").ToList ();
		minimapSpawnerPlant = GameObject.FindGameObjectsWithTag ("PlantSpawners").ToList ();
	}


#region WriteDownNeededInfo

	void AddArchetypesOfFish (string fishHunter, string itsArchetype) {

		archetypesOfFish_dict.Add (fishHunter, itsArchetype);
	}
	

	void AddEatableArchetypes (string fishHunter, params string [] archetypesToEat) {

		eatableArchetypes_dict.Add (fishHunter, archetypesToEat.ToList ());
	}

	void AddEatableFish (string fishHunter, params string [] fishToEat) {

		eatableFish_dict.Add (fishHunter, fishToEat.ToList ());
	}

#endregion


#region GetNeededInfo

	public List <string> GetEatableArchetypes (string fishHunter) {

		if (eatableArchetypes_dict.ContainsKey (fishHunter))
		{
			return eatableArchetypes_dict [fishHunter];
		}
		else
		{
			return emptyList_list;
		}
	}


	public List <string> GetEatableFish (string fishHunter) {

		if (eatableFish_dict.ContainsKey (fishHunter))
		{
			return eatableFish_dict [fishHunter];
		}
		else
		{
			return emptyList_list;
		}
	}


	public string GetArchetypeOfNeededFish (string fish) {

		return archetypesOfFish_dict [fish];
	}


	public List <string> GetHuntersFor (string victim) {

		List <string> listOfHunters = new List<string> ();

		foreach (KeyValuePair <string, List <string>> eatableFish in eatableFish_dict)
		{
			if (eatableFish.Value.Contains (victim))
			{
				listOfHunters.Add (eatableFish.Key);
			}
		}

		return listOfHunters;
	}

#endregion

	public void SpawnMiniMapObject (string name, string tag) {

		GameObject minimapObject_go = null;

		int randomValue = 1;

		//  existingSpeciesInTheTree_list contains unique objects!!!!
		if (tag == "Plant" && !CommonData.lrc_scr.existingSpeciesInTheTree_list.Any (go => go.name == name))
		{
//			Debug.Log ("Sadness");
			Transform pla = minimapSpawnerPlant [Random.Range (0, minimapSpawnerPlant.Count)].transform;

			GameObject plantSpawnerToRemove = pla.gameObject;
			minimapSpawnerPlant.Remove (plantSpawnerToRemove);

			List <GameObject> possiblePlants = plantMinimapIcons;
			GameObject goToSpawn = possiblePlants.FirstOrDefault (go => go.name.Contains (name)) as GameObject;
			minimapObject_go = NGUITools.AddChild (treeFishHolder, goToSpawn) as GameObject;
			minimapObject_go.transform.localPosition = new Vector3 (
				pla.transform.localPosition.x,
				-77,
				pla.transform.localPosition.z
				);

			minimapObject_go.name = name;
			SpeciesNames sn = minimapObject_go.GetComponent <SpeciesNames> ();
			sn.nameOfSpecies = name;
			CommonData.lrc_scr.existingSpeciesInTheTree_list.Add (minimapObject_go);
			
			RespectiveSpawner rs = minimapObject_go.GetComponent <RespectiveSpawner> ();
			rs.respectiveSpawner = plantSpawnerToRemove;
			//minimapObject_go.transform.parent = treeFishHolder.transform;
		}
		else if (tag == "Herbivore" && !CommonData.lrc_scr.existingSpeciesInTheTree_list.Any (go => go.name == name))
		{
			Transform her = minimapSpawnerHerbi [Random.Range (0, minimapSpawnerHerbi.Count)].transform;
			
			GameObject herSpawnerToRemove = her.gameObject;
			minimapSpawnerHerbi.Remove (herSpawnerToRemove);

			List <GameObject> possibleHerbivores = herbivoreMinimapIcons;
			GameObject goToSpawn = possibleHerbivores.FirstOrDefault (go => go.name.Contains (name)) as GameObject;

			minimapObject_go = NGUITools.AddChild (treeFishHolder, goToSpawn) as GameObject;
			minimapObject_go.transform.localPosition = new Vector3 (
				her.transform.localPosition.x,
				-30,
				her.transform.localPosition.z
				);

			minimapObject_go.name = name;
			SpeciesNames sn = minimapObject_go.GetComponent <SpeciesNames> ();
			sn.nameOfSpecies = name;
			CommonData.lrc_scr.existingSpeciesInTheTree_list.Add (minimapObject_go);
			
			RespectiveSpawner rs = minimapObject_go.GetComponent <RespectiveSpawner> ();
			rs.respectiveSpawner = herSpawnerToRemove;
			//minimapObject_go.transform.parent = treeFishHolder.transform;
		}
		else if (tag == "Omnivore" && !CommonData.lrc_scr.existingSpeciesInTheTree_list.Any (go => go.name == name))
		{
			Transform omn = minimapSpawnerOmni [Random.Range (0, minimapSpawnerOmni.Count)].transform;
			
			GameObject omnSpawnerToRemove = omn.gameObject;
			minimapSpawnerOmni.Remove (omnSpawnerToRemove);

			List <GameObject> possibleOmnivores = omnivoreMinimapIcons;
			GameObject goToSpawn = possibleOmnivores.FirstOrDefault (go => go.name.Contains (name)) as GameObject;

			minimapObject_go = NGUITools.AddChild (treeFishHolder, goToSpawn) as GameObject;
			minimapObject_go.transform.localPosition = new Vector3 (
				omn.transform.localPosition.x,
				17,
				omn.transform.localPosition.z
				);

			minimapObject_go.name = name;
			SpeciesNames sn = minimapObject_go.GetComponent <SpeciesNames> ();
			sn.nameOfSpecies = name;
			CommonData.lrc_scr.existingSpeciesInTheTree_list.Add (minimapObject_go);
			
			RespectiveSpawner rs = minimapObject_go.GetComponent <RespectiveSpawner> ();
			rs.respectiveSpawner = omnSpawnerToRemove;
			//minimapObject_go.transform.parent = treeFishHolder.transform;
		}
		else if (tag == "Carnivore" && !CommonData.lrc_scr.existingSpeciesInTheTree_list.Any (go => go.name == name))
		{
			Transform car = minimapSpawnerCarni [Random.Range (0, minimapSpawnerCarni.Count)].transform;
			
			GameObject carSpawnerToRemove = car.gameObject;
			minimapSpawnerCarni.Remove (carSpawnerToRemove);

			List <GameObject> possibleCarnivores = carnivoreMinimapIcons;
			GameObject goToSpawn = possibleCarnivores.FirstOrDefault (go => go.name.Contains (name)) as GameObject;

			minimapObject_go = NGUITools.AddChild (treeFishHolder, goToSpawn) as GameObject;
			minimapObject_go.transform.localPosition = new Vector3 (
				car.transform.localPosition.x,
				70,
				car.transform.localPosition.z
				);

			minimapObject_go.name = name;
			SpeciesNames sn = minimapObject_go.GetComponent <SpeciesNames> ();
			sn.nameOfSpecies = name;
			CommonData.lrc_scr.existingSpeciesInTheTree_list.Add (minimapObject_go);
			
			RespectiveSpawner rs = minimapObject_go.GetComponent <RespectiveSpawner> ();
			rs.respectiveSpawner = carSpawnerToRemove;
			//minimapObject_go.transform.parent = treeFishHolder.transform;
		}
		//
		//  check if we can draw smth
		//  it will be called every time we throw in species.
		// Why here? Because if it was in "Instantiate object", it didn't get added to the list with existing species right away.
		CommonData.lrc_scr.DrawingRendererRunner ();

	}

}


