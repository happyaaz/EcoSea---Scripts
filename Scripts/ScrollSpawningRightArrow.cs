using UnityEngine;
using System.Collections;

public class ScrollSpawningRightArrow : MonoBehaviour {
	
	public UIScrollView SpawnScrollView;

	void OnClick () {
		SpawnScrollView.MoveRelative (new Vector3( -75,0,0)) ;
		
	}
}
