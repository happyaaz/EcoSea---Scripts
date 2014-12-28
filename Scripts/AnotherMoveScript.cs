using UnityEngine;
using System.Collections;

public class AnotherMoveScript : MonoBehaviour {

	Vector3 DestPos = Vector3.zero;
	Transform CurrPos;
	Transform DestRot;

	public float speed = 0.2f;
	public float proximity = 10;
	public float offset = 1;
	
	bool isSlerping = false;
	public float timeTakenDuringSlerp = 5;
	float timeStartedSlerping;
	CommonScript cS;
	public GameObject reference;

	void StartSlerping (){
		isSlerping = true;
		timeStartedSlerping = Time.time;
		
	}


	void Start () {

		PickANewDest ();
	}

	public void SetReference (){
		cS = reference.GetComponent<CommonScript> ();

	}
	void PickANewDest () {
		DestPos = new Vector3 (Random.Range (-offset, offset) + cS.currLocation.x, Random.Range (-offset, offset) + cS.currLocation.y, Random.Range (-offset, offset)+ cS.currLocation.z);
		StartSlerping ();
	}

	void Update () {
		rigidbody.transform.Translate(Vector3.forward * speed*Time.deltaTime);
		if (isSlerping) {
			float timeSinceStarted = Time.time - timeStartedSlerping;
			float percentageCompleted = timeSinceStarted / timeTakenDuringSlerp;
			Vector3 relativePos = DestPos - transform.position;
			Quaternion rotation = Quaternion.LookRotation(relativePos);
			transform.rotation = Quaternion.Slerp (transform.rotation, rotation, 0.1f);

			try
			{
				if (Vector3.Distance (DestPos, transform.position) <= proximity) //  && reference != null && reference.GetComponent <CommonScript> () != null
				{
						isSlerping = false;
						cS.PickASchoolingLocation();
						PickANewDest();
						StartSlerping();
				}
			}
			catch
			{

			}
		}
	}
}
