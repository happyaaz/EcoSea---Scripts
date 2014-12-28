using UnityEngine;
using System.Collections;

public class ScrollSpawningLeftArrow : MonoBehaviour {

	public UIScrollView SpawnScrollView;
	// Use this for initialization
	void OnClick () {
		SpawnScrollView.MoveRelative (new Vector3( 75,0,0)) ;
	
	}

}
