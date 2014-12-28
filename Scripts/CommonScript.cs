using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommonScript : MonoBehaviour {

	float TimerToSchool = 0; 
	public int NumberInSchool = 5;
	public Vector3 currLocation = Vector3.zero;
	public GameObject prefab, bigCollider;
	public List<GameObject> fishes = new List<GameObject>();
//	GameObject Parent;


	int i = 0;
//	int i = 0;
	// Use this for initialization
	void Start () {
//		InvokeRepeating ("Spawn", 0.1f, NumberInSchool);
//		PickASchoolingLocation ();
		for (int i = 0; i < NumberInSchool; i++) {
			GameObject anchovie = Instantiate(prefab, transform.position, Quaternion.identity) as GameObject;
			anchovie.GetComponent<AnotherMoveScript>().reference = gameObject;
			anchovie.GetComponent<AnotherMoveScript>().SetReference();
			anchovie.name = "anchovie" + i.ToString ();
			fishes.Add (anchovie);
		}
		GameObject BigCollider = Instantiate (bigCollider, transform.position, Quaternion.identity) as GameObject;
		BigCollider.GetComponent<AnotherMoveScript>().reference = gameObject;
		BigCollider.GetComponent<AnotherMoveScript>().SetReference();
		fishes.Add (BigCollider);
//		foreach (GameObject fish in fishes) {
//
//			//			fish.GetComponent<AnotherMoveScript>().SendMessage("MeetUrMaker", this);
//		}
	}
	public void PickASchoolingLocation (){
		currLocation = transform.position;
//		Debug.Log (currLocation);
	}
	// Update is called once per frame
	void Update () {
//		currLocation = transform.position;
		TimerToSchool += Time.deltaTime;
//		if (TimerToSchool >= 10) {
//			TimerToSchool = 0;
//		}
	}
}
