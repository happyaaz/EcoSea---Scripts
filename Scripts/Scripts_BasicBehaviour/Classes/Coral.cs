using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class Coral : Plant_AC {

	public float speed;
	public LayerMask layerMask;
	public static int howManyPlantsReproduced = 0;


	public override void Start () {
		base.Start ();
		ReprAndEat.HappyReproducingPlants += Reproducing;

		float randomSizeForCorals = Random.Range (0.4f, 1.0f);

		this.transform.localScale = new Vector3 (randomSizeForCorals, randomSizeForCorals, randomSizeForCorals);

		Placement ();

		
		ReprAndEat.checkIfItsTimeToDie += CheckIfItsTimeToDie;
	}


	public void Placement () {
	
		RaycastHit[] hits;
		hits = Physics.RaycastAll(transform.position, Vector3.down, 100.0F);
		int i = 0;
		while (i < hits.Length) 
		{
			RaycastHit hit = hits[i];
			if (hit.transform.tag == "OceanFloor") 
			{
				this.transform.position = new Vector3 (this.transform.position.x, 
				                                       hit.point.y, 
				                                       this.transform.position.z);
//				Debug.Log ("hit.name = " + hit.transform.name + ", point = " + hit.point);
				break;
			}
			i++;
		}
	}


	private void Reproducing () {
		
//		Debug.Log ("I really tried " + name);
		//		Debug.Log ("howManyPlantsReproduced = " + howManyPlantsReproduced + 
		//		           ", CommonData.rae_scr.howManyPlantsWillBeReproduced = " + CommonData.rae_scr.howManyPlantsWillBeReproduced);
		if (howManyPlantsReproduced < CommonData.rae_scr.howManyPlantsWillBeReproduced)
		{
			if (GameObject.FindGameObjectsWithTag (this.tag).ToList ().Count < maxAmountOfThisSpecies)
			{
				Instantiate (child_go, CommonFunctions.RandomPositionWhenThrowingIn (), Quaternion.identity);
				howManyPlantsReproduced ++;
			}
			else
			{
//				Debug.Log ("No1");
			}
		}
		else
		{
//			Debug.Log ("No2 = " + howManyPlantsReproduced + ", " + CommonData.rae_scr.howManyPlantsWillBeReproduced);
		}
	}


	void OnDestroy () {
		
		ReprAndEat.HappyReproducingPlants -= Reproducing;
		
		ReprAndEat.checkIfItsTimeToDie -= CheckIfItsTimeToDie;
	}
}
