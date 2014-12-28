using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class ControllingButtonsVisibility : MonoBehaviour {


	public List <GameObject> buttonsToThrowIn = new List <GameObject> ();
	List <GameObject> buttonsToDisable = new List <GameObject> ();


	void Start () {

		foreach (GameObject button in buttonsToThrowIn) 
		{
			int indexOfSymbol = button.name.IndexOf ("_");
			string typeOfRespectiveFish = button.name.Substring (0, indexOfSymbol);
			//Debug.Log ("typeOfRespectiveFish = " + typeOfRespectiveFish);
//			Debug.Log (CommonData.wc_scr.IfSpeciesUnlocked (typeOfRespectiveFish));
			if (CommonData.wc_scr.IfSpeciesUnlocked (typeOfRespectiveFish) == "Locked")
			{
				buttonsToDisable.Add (button);
			}
		}
		StartCoroutine (DisableAllTheShit (buttonsToDisable));
	}


	IEnumerator DisableAllTheShit (List <GameObject> toDisable) {
		while (true) 
		{
			yield return 0;
			foreach (GameObject test in toDisable) 
			{
				NGUITools.SetActive (test, false);
			}
		}
	}

	
	public void EnableButtons (List <string> speciesNames) {

//		Debug.Log ("ALALALA!");
		StopCoroutine ("DisableAllTheShit");

		foreach (string respectiveButton in speciesNames) 
		{
//			Debug.Log ("respectiveButton = " + respectiveButton);
			GameObject neededButton = buttonsToThrowIn.FirstOrDefault (go => go.name.Contains (respectiveButton)) as GameObject;
//			Debug.Log ("neededButton = " + neededButton.name);
			buttonsToDisable.Remove (neededButton);
			NGUITools.SetActive (neededButton, true);
		}
		StartCoroutine (DisableAllTheShit (buttonsToDisable));
	}
}
