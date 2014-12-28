using UnityEngine;
using System.Collections;

public class TreasureChest_GUI : MonoBehaviour {

	TreasureChest trsChest;
	WinningCondition winCond;

	// Use this for initialization
	void Start () {
		trsChest = GameObject.Find ("PlayButton").GetComponent<TreasureChest> ();
		winCond = GameObject.Find ("WinningCondition").GetComponent<WinningCondition> ();

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick()
	{
		StartCoroutine(trsChest.CollectGUITreasureChest ());
		//		Debug.Log ("GUI chest clicked");

		StartCoroutine (winCond.CloseBalancedWindow ());
	}
}
