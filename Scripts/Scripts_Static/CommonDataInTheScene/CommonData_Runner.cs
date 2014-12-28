using UnityEngine;
using System.Collections;

public class CommonData_Runner : MonoBehaviour {

	// Use this for initialization
	void Awake () {
	
		CommonData.SetToDefault ();
		CommonData.FindListOfPrefabs ();
		CommonData.FindCountingExistingFish ();
		CommonData.GettingNeededScripts ();
		CommonData.FindNeededGameObjects ();
		CommonData.ControlNumberOfFish ();
	}
}
