using UnityEngine;
using System.Collections;

public class SkipTutorial : MonoBehaviour {
	Tutorial_Ctrl nMC;
	// Use this for initialization
	void Start () {
		nMC = Camera.main.GetComponent<Tutorial_Ctrl> ();
	}
	
	// Update is called once per frame
	void OnClick () {
		nMC.SkipTutorial ();
		gameObject.SetActive (false);
	}
	void OnTooltip (bool show)
	{
		UITooltip.ShowText ("Skip Tutorial");
	}
}
