using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class Plankton : Plant_AC {

	public float speed;
	
	public static int howManyPlantsReproduced = 0;


	public override void  Start () {
		base.Start ();
		ReprAndEat.HappyReproducingPlants += Reproducing;
		
		ReprAndEat.checkIfItsTimeToDie += CheckIfItsTimeToDie;
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
				//Debug.Log ("No1 + " + GameObject.FindGameObjectsWithTag (this.tag).ToList ().Count + ", " + maxAmountOfThisSpecies);
			}
		}
		else
		{
			//Debug.Log ("No2 = " + howManyPlantsReproduced + ", " + CommonData.rae_scr.howManyPlantsWillBeReproduced);
		}
	}


	void OnDestroy () {
		
		ReprAndEat.HappyReproducingPlants -= Reproducing;
		
//		
		ReprAndEat.checkIfItsTimeToDie -= CheckIfItsTimeToDie;
	}
}
