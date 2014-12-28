using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ListOfPrefabs : MonoBehaviour {

	public List <GameObject> species_list = new List <GameObject> ();
	public Dictionary <string, List <GameObject>> keeperOfPrefabs_dict = new Dictionary <string, List<GameObject>> ();

	void Awake () {

		foreach (GameObject go in species_list)
		{
			SeaCreature sc_scr = go.GetComponent <SeaCreature> ();

			if (!keeperOfPrefabs_dict.ContainsKey (sc_scr.nameOfSpecies))
			{
				List <GameObject> creature = new List<GameObject> ();
				creature.Add (go);
				keeperOfPrefabs_dict.Add (sc_scr.nameOfSpecies, creature);
			}
			else
			{
				List <GameObject> listOfCreatures = keeperOfPrefabs_dict [sc_scr.nameOfSpecies];
				listOfCreatures.Add (go);
				keeperOfPrefabs_dict [sc_scr.nameOfSpecies] = listOfCreatures;
			}
		}

//		Debug.Log ("Count = " + keeperOfPrefabs_dict.Count);
	}


	public GameObject SendInstantiatedObject (string nameOfSpecies) {

		List <GameObject> seaCretures = keeperOfPrefabs_dict [nameOfSpecies];

		if (seaCretures.Count == 1)
		{
			return seaCretures [0];
		}
		else
		{
			return seaCretures [Random.Range (0, seaCretures.Count)];
		}
	}


	public GameObject SendTheChild (GameObject parent) {

		GameObject go = (GameObject) species_list.Find (t => parent.name.Contains (t.gameObject.name)) as GameObject;

		return go;
	}
}
