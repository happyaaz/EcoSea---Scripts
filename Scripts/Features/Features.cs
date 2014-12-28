using UnityEngine;
using System.Collections.Generic;

public class Features : MonoBehaviour {

	

	
	public int numberOfTurns_int = 1;

	public bool eatLessWhenMany_bool;
	public int herbivoresUpLimit;
	public int omnivoresUpLimit;
	public int carnivoresUpLimit;

	public bool easierToHitRatio_bool;
	public float lowerExtremes;
	public float lowerRatioBorders;

	public bool enableLifespan_bool;
	public int planktonLongevity;
	public int coralLongevity;
	public int seaWeedLongevity;
	public int anchovieLongevity;
	public int seaUrchinLongevity;
	public int crabLongevity;
	public int barracoudaLongevity;
	public Dictionary <string, int> longevity_dict = new Dictionary <string, int> ();

	public bool dieIfDidntEatTwoRoundsWheneatLessWhenManyIsEnabled_bool;

	public bool losingCondition_bool;
	public int numberOfTurnWhenYouAreGoingToLose_int;

	// Use this for initialization
	void Awake () {
	
		longevity_dict.Add ("Barracouda", barracoudaLongevity);
		longevity_dict.Add ("Crab", crabLongevity);
		longevity_dict.Add ("Anchovie", anchovieLongevity);
		longevity_dict.Add ("Plankton", planktonLongevity);
		longevity_dict.Add ("SeaWeed", seaWeedLongevity);
		longevity_dict.Add ("Coral", coralLongevity);
		longevity_dict.Add ("SeaUrchin", seaUrchinLongevity);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
