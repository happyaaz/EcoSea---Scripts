using UnityEngine;
using System.Collections;
using System;

public class CrabMovement : MonoBehaviour {

	CharacterController cc_ctrl;
	SeaCreature sc_this;

	// Use this for initialization
	void Start () {

		sc_this = this.GetComponent <SeaCreature> ();
		cc_ctrl = this.GetComponent <CharacterController> ();
	}



	// Update is called once per frame
	void FixedUpdate () {
	
		if (!cc_ctrl.isGrounded)
		{
			cc_ctrl.SimpleMove (Vector3.down * Time.deltaTime * 1);
		}
		else
		{
			RaycastHit hit;
			
			if (Physics.Raycast (transform.position, Vector3.down, out hit))
			{
				if (hit.transform.tag == "OceanFloor")
				{
					//  all of a sudden the rotation is ok O____O
					var hitRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
					transform.rotation = hitRotation;
				}
			}
		}
	}


	public void MovingToMovingObjects (GameObject movingTargetToMoveTo, Action DoSmthAfterReachingTarget, Action DoStuffIfTargetIsDestroyed, float speed) {

		float distance = 0;
		if (movingTargetToMoveTo != null)
		{
//			Debug.Log ("movingTargetToMoveTo = " + movingTargetToMoveTo.transform.position);
			if (sc_this.currentActionState_enum == SeaCreature.PossibleStatesOfActions.BeingChosenAsMateAndHeadingToItsMate ||
			    sc_this.currentActionState_enum == SeaCreature.PossibleStatesOfActions.GettingToMate)
			{
				distance = 3.6f;
			}
			else
			{
				distance = 2.6f;
			}
		
			if (Vector3.Distance (this.transform.position, movingTargetToMoveTo.transform.position) > distance)
			{
				Vector3 offset = movingTargetToMoveTo.transform.position - this.transform.position;
//				Debug.Log ("Vector3.Distance (this.transform.position, movingTargetToMoveTo.transform.position) = " +
//				           Vector3.Distance (this.transform.position, movingTargetToMoveTo.transform.position) +
//				           "offset = " + offset);
				cc_ctrl.SimpleMove  (offset * Time.deltaTime * 6);
			}
			else
			{
			//	Debug.Log ("GOOOO!");
				if (DoSmthAfterReachingTarget != null)
				{
			//		Debug.Log ("DONE!!!!");
					DoSmthAfterReachingTarget ();
				}
			}
		}
		else
		{
			if (DoStuffIfTargetIsDestroyed != null)
			{
				DoStuffIfTargetIsDestroyed ();
			}
		}
	}
	
	
	public void MovingToStaticObject (Action DoSmthAfterReachingTarget, Vector3 targetPos, float speed) {


		//Debug.Log (offset);
		if (Vector3.Distance (this.transform.position, targetPos) > 2.5f)
		{
			Vector3 offset = targetPos - this.transform.position;
//			Debug.Log (Vector3.Distance (this.transform.position, targetPos) + ", offset = " + offset);
			cc_ctrl.SimpleMove  (offset * Time.fixedDeltaTime * 6);
			//Debug.Log ("Vector3.Distance (this.transform.position, targetPos) = " + Vector3.Distance (this.transform.position, targetPos));
		}
		else
		{
			if (DoSmthAfterReachingTarget != null)
			{
//				Debug.Log ("Crab rocks");
				DoSmthAfterReachingTarget ();
			}
		}
		/*
		if (Vector3.Distance (this.transform.position, targetPos) > 0.1f)
		{
			cc_ctrl.SimpleMove  (transform.forward * speed * Time.deltaTime);
		}
		else
		{
			if (DoSmthAfterReachingTarget != null)
			{
				DoSmthAfterReachingTarget ();
			}
		}
		*/
	}
	
}
