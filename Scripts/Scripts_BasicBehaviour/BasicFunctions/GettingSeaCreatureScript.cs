using UnityEngine;
using System.Collections;

public class GettingSeaCreatureScript : MonoBehaviour {

	public SeaCreature GetSeaCreatureScript (GameObject getScript) {
		
		SeaCreature sc_Ac = getScript.GetComponent <SeaCreature> ();
		return sc_Ac;
	}
}
