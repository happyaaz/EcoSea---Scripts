using UnityEngine;
using System.Collections.Generic;

public class MiniGoals : MonoBehaviour {
	
	public int levelWeAreIn_int = 1;
	public Dictionary <int, List <string>> levelGoals_dict = new Dictionary <int, List<string>> ();
	public Dictionary <string, int> madeActions_dict = new Dictionary <string, int> ();
	public List <string> miniGoalsGuiText_list = new List <string> ();

	public List <string> goalsForFirstLevel = new List <string> ();


	private UIToggle uit_scr;
	
	void Start () {
		
		
		List <string> miniGoalsLevelFirst_list = new List <string> ();
		
		miniGoalsLevelFirst_list.Add ("Plankton_WasSpawned_1");
		miniGoalsLevelFirst_list.Add ("Anchovie_WasSpawned_1");
		miniGoalsLevelFirst_list.Add ("Anchovie_Eats_Plankton_1");
		miniGoalsLevelFirst_list.Add ("Anchovie_GaveBirth_1");
		
		miniGoalsLevelFirst_list.Add ("Coral_WasSpawned_1");
		miniGoalsLevelFirst_list.Add ("SeaUrchin_WasSpawned_1");
		miniGoalsLevelFirst_list.Add ("SeaUrchin_Eats_Coral_1");
		miniGoalsLevelFirst_list.Add ("SeaUrchin_GaveBirth_1");
		
		miniGoalsLevelFirst_list.Add ("SeaWeed_WasSpawned_1");
		miniGoalsLevelFirst_list.Add ("Crab_WasSpawned_1");
		miniGoalsLevelFirst_list.Add ("Crab_Eats_SeaWeed_1");
		miniGoalsLevelFirst_list.Add ("Crab_GaveBirth_1");
		
		miniGoalsLevelFirst_list.Add ("Barracouda_WasSpawned_1");
		miniGoalsLevelFirst_list.Add ("Barracouda_Eats_Anchovie_1");
		miniGoalsLevelFirst_list.Add ("Barrcouda_GaveBirth_1");

		goalsForFirstLevel = miniGoalsLevelFirst_list;
		
		miniGoalsGuiText_list.Add ("Spawn a Plankton");
		miniGoalsGuiText_list.Add ("Spawn an Anchovie");
		miniGoalsGuiText_list.Add ("Anchovie eats Plankton");
		miniGoalsGuiText_list.Add ("Anchovie reproduces");

		miniGoalsGuiText_list.Add ("Spawn a Coral");
		miniGoalsGuiText_list.Add ("Spawn a SeaUrchin");
		miniGoalsGuiText_list.Add ("SeaUrchin eats Coral");
		miniGoalsGuiText_list.Add ("SeaUrchin reproduces");
		
		miniGoalsGuiText_list.Add ("Spawn a SeaWeed");
		miniGoalsGuiText_list.Add ("Spawn a Crab");
		miniGoalsGuiText_list.Add ("Crab eats SeaWeed");
		miniGoalsGuiText_list.Add ("Crab reproduces");

		miniGoalsGuiText_list.Add ("Spawn a Barracouda");
		miniGoalsGuiText_list.Add ("Barracouda eats Anchovie");
		miniGoalsGuiText_list.Add ("Barracouda reproduces");
		
		levelGoals_dict [1] = miniGoalsLevelFirst_list;
		
	}
	




	public void WriteDownEvent (string eventStr) {
		
		if (!madeActions_dict.ContainsKey (eventStr))
		{
			madeActions_dict.Add (eventStr, 1);
//			Debug.Log ("FirstTime = " + eventStr + madeActions_dict [eventStr]);
		}
		else
		{
			int numberOfOccurancesOfThisEvent = madeActions_dict [eventStr];
			numberOfOccurancesOfThisEvent ++;
			madeActions_dict [eventStr] = numberOfOccurancesOfThisEvent;
		}
	}

	
	public void CheckMiniGoals () {

//		foreach (string str in madeActions_list)
//		{
//			Debug.Log ("Event = " + str);
//		}
//		
		
		List <string> miniActions_list = new List <string> ();

		foreach (KeyValuePair <string, int> pair in madeActions_dict)
		{
			string action = pair.Key + "" + pair.Value.ToString ();
		//	Debug.Log ("And that's what was made = " + action);
			miniActions_list.Add (action);
		}


		
		foreach (string goalStr in miniActions_list)
		{
			if (goalsForFirstLevel.Contains (goalStr))
			{
//				Debug.Log ("goalStr = " + goalStr + ", " + goalsForFirstLevel.IndexOf (goalStr));
				GameObject ach_go = GameObject.Find ("GoalCheckBox" + goalsForFirstLevel.IndexOf (goalStr).ToString ());
				uit_scr = ach_go.GetComponent <UIToggle>();
				uit_scr.value = true;

			}
			
		}
	}
}
