using UnityEngine;
using System.Collections;

public class RotateObjectToward : MonoBehaviour {


	public void RotateOnce (Vector3 pointToLookAt) {

		Vector3 targetDir = pointToLookAt - transform.position;
		Vector3 forward = transform.forward;
		float angle = Vector3.Angle(targetDir, forward);
		//transform.eulerAngles = new Vector3 (0, pointToLookAt.y, 0);
		if (angle > 45 && angle < 135)
		{
			angle -= 180;
			transform.eulerAngles = new Vector3 (0, angle, 0);
		}
		else if (angle > -45 && angle < 45)
		{
			angle -= 90;
			transform.eulerAngles = new Vector3 (0, angle, 0);
		}
		else
		{
			transform.eulerAngles = new Vector3 (0, angle, 0);
		}
//		Debug.Log (angle + ", " + transform.eulerAngles); 
	}
}
