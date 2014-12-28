using UnityEngine;
using System.Collections;
using System;


public static class ControlNumberOfClicks {



	public static void IncreaseNumberOfClicks (Action NeededActionForInstantiating) {


		if (NeededActionForInstantiating != null)
		{
			NeededActionForInstantiating ();
			//  display the button
			if (CommonData.numberOfClicks % 2 != 0)
			{
//				Debug.Log ("CommonData.numberOfClicks = " + CommonData.numberOfClicks);
				CommonData.allowedToRunSimulation = true;
				CommonData.allowedToClick = false;
				//  Hide the labels
				NGUITools.SetActive (GameObject.Find ("SpawningTab"), false);
			}
		}

	}
}
