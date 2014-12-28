using UnityEngine;
using System.Collections;

public class ChoosingLayers : MonoBehaviour {

	//  saving resources
	public void SetLayers (ref int layerMask) {
		
		switch (this.tag)
		{
			case ("Carnivore"):
				//	Debug.Log ("Carnivore");
				layerMask = ((1 << 9) | (1 << 10));
				break;
			case ("Omnivore"):
				//	Debug.Log ("Omnivore");
				layerMask = ((1 << 10) | (1 << 11));
				break;
			case ("Herbivore"):
				//	Debug.Log ("Herbivore");
				layerMask = ((1 << 11));
				break;
			default:
				break;
		}
	}
}
