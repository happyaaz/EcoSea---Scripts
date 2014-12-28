using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class FindSpeciesWeNeed : MonoBehaviour {


	public List <GameObject> FindMates (string tagOfFishsArchetype, string nameOfSpecies, GameObject male) {

		List <GameObject> mates = new List<GameObject> ();

		//  we should not be able to find ourselves
		mates = GameObject.FindGameObjectsWithTag (tagOfFishsArchetype).
			Select (go => go.gameObject).
			Where (go => go.name.Contains (nameOfSpecies) && male != go).
			ToList ();

		return mates;
	}


	public List <GameObject> CountExistingFood (List <string> eatableArchetypes, List <string> eatableSpecies) {

		List <GameObject> existingFood = new List<GameObject> ();
		List <GameObject> eatableArchetypesFound = new List <GameObject> ();

		foreach (string archetype in eatableArchetypes)
		{
			List <GameObject> foundArch = GameObject.FindGameObjectsWithTag (archetype).ToList ();
			eatableArchetypesFound.AddRange (foundArch);
		}

		foreach (string species in eatableSpecies)
		{
			List <GameObject> foundSpec = eatableArchetypesFound.
				Select (go => go.gameObject).
				Where (go => go.name.Contains (species)).
				ToList ();
			existingFood.AddRange (foundSpec);
		}
//		Debug.Log ("existingFood.Count = " + existingFood.Count);
		return existingFood;
	}


	public int NumberOfExistingFishOfThisType (string nameOfFish, string archetypeOfFish) {

		//  can't change this function due to having the reproduction system
		List <GameObject> foundSpecies = GameObject.FindGameObjectsWithTag (archetypeOfFish).
			Select (go => go.gameObject).
				Where (go => go.name.Contains (nameOfFish)).
				ToList ();

		return foundSpecies.Count;
	}


	public List <GameObject> ReturnSpeciesOfThisType (string nameOfFish, string archetypeOfFish) {
		
		//  can't change this function due to having the reproduction system
		List <GameObject> foundSpecies = GameObject.FindGameObjectsWithTag (archetypeOfFish).
			Select (go => go.gameObject).
				Where (go => go.name.Contains (nameOfFish)).
				ToList ();
		
		return foundSpecies;
	}
}
