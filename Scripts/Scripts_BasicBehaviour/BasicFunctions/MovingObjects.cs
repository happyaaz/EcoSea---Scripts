using UnityEngine;
using System.Collections;
using System;

public class MovingObjects : MonoBehaviour {

	CommonScript comScr;
	Vector3 movingObject;
	
	void Start (){

		comScr = gameObject.GetComponent <CommonScript> ();
	}

	public void MovingToMovingObjects (GameObject movingTargetToMoveTo, Action DoSmthAfterReachingTarget, Action DoStuffIfTargetIsDestroyed, float speed) {
		
		if (movingTargetToMoveTo != null)
		{
			if (comScr == null)
			{
				movingObject = this.transform.position;
			}
			else 
			{
				movingObject = comScr.fishes[0].transform.position;
			}
			
			if (Vector3.Distance (movingObject, movingTargetToMoveTo.transform.position) > 2f)
			{
				transform.LookAt (movingTargetToMoveTo.transform);
				rigidbody.MovePosition (rigidbody.position + transform.forward * speed * Time.deltaTime);
			}
			else
			{
				if (DoSmthAfterReachingTarget != null)
				{
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
		
		//  for some STUPID (I don't know) reasons an object may not achieve the target (f.e. 0.00001 meters)
		//  a workaround - give a little bit of freedom
		if (Vector3.Distance (this.transform.position, targetPos) > 0.1f)
		{
			rigidbody.MovePosition (rigidbody.position + transform.forward * speed * Time.deltaTime);
		}
		else
		{
			if (DoSmthAfterReachingTarget != null)
			{
				DoSmthAfterReachingTarget ();
			}
		}
	}

}
