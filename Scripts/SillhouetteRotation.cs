using UnityEngine;
using System.Collections;

public class SillhouetteRotation : MonoBehaviour {

	public float orbitSpeed = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up * orbitSpeed * Time.deltaTime);
	}
}
