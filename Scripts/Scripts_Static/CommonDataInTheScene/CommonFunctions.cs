using UnityEngine;
using System.Collections;

public class CommonFunctions {

	public static int minX = -20;
	public static int maxX = 20;
	public static int minY = -2;
	public static int maxY = 12;
	public static int minZ = 0;
	public static int maxZ = 27;



	public static Vector3 RandomPositionWhenThrowingIn () {

		float randomSpot_x_fl = Random.Range (-18, 18);
		float randomSpot_y_fl = Random.Range (0, 10);
		float randomSpot_z_fl = Random.Range (3, 25);
		
		Vector3 randomPos = new Vector3 (randomSpot_x_fl, randomSpot_y_fl, randomSpot_z_fl);
		return randomPos;
	}
}
