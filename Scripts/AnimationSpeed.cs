using UnityEngine;
using System.Collections;

public class AnimationSpeed : MonoBehaviour {

	public float someNumber;

	// Use this for initialization
	void Start () {
		animation.Play ("Take 001");
		animation ["Take 001"].speed = someNumber;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
