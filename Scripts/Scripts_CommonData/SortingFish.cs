using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SortingFish : MonoBehaviour {

	public List <GameObject> SortFish (List <GameObject> fishToSort) {

		for (int i = 0; i < fishToSort.Count - 1; i ++)
		{
			float sqrMag1 = Vector3.Distance (fishToSort [i].transform.position, transform.position);
			float sqrMag2 = Vector3.Distance (fishToSort [i + 1].transform.position, transform.position);
			
			if (sqrMag2 < sqrMag1)
			{
				GameObject temp = fishToSort [i];
				fishToSort [i] = fishToSort [i + 1];
				fishToSort [i + 1] = temp;
				i = 0;
			}
		}
		
		return fishToSort;
	}
}
