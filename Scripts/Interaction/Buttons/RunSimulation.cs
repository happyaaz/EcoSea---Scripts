using UnityEngine;
using System.Collections;

public class RunSimulation : MonoBehaviour {


	CountingSpecies cS;
	public GameObject buttonsLabel_go;
	
	void Start () {
		
		cS = Camera.main.GetComponent<CountingSpecies> ();
		buttonsLabel_go = GameObject.Find ("SpawningTab");
	}

	// Use this for initialization
	void OnClick () {


	}
}
