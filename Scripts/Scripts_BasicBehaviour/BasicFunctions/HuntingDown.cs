using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class HuntingDown : MonoBehaviour {

	//eating particles
	//  REMEMBER
	public GameObject eatingParticles;
	public GameObject eatingParticleInstance;

	SeaCreature sc_this;
	LookingForFood lff_this;

	int numberOfFishEaten = 0;

	void Start () {
	
		sc_this = this.GetComponent <SeaCreature> ();
		lff_this = this.GetComponent <LookingForFood> ();
	}


	public void HuntingDown_TargetIsReached () {



		numberOfFishEaten ++;


		
		CommonData.rae_scr.spreadSheet_targetsToEat_list_Checker_list.Remove (sc_this.huntItDown_go);

		//
		Vector3 actionHappensHere = this.transform.position;

		//create and destroy particles
		eatingParticleInstance = Instantiate (eatingParticles, actionHappensHere, eatingParticles.transform.rotation) as GameObject;
		Destroy (eatingParticleInstance, 2.0f);

		SeaCreature sc_toDestroy = sc_this.huntItDown_go.GetComponent <SeaCreature> ();

		//  destroy an object we've eaten
		StartCoroutine (sc_toDestroy.DieFish (0.01f));

		//  choose a new target spot
		sc_this.PickRandomSpot ();
		//  change states - we are full
		sc_this.currentActionState_enum = SeaCreature.PossibleStatesOfActions.Moving;
		//  go back to the initial speed
		sc_this.Speed_prop = sc_this.SpeedInit_prop;


		//rae.totalNumberOfFishThatAteSomething ++;

		if (this.tag == "Herbivore" && CommonData.numberOfHerbivoresEatingPlants_int > 0)
		{
			CommonData.numberOfHerbivoresThatAtePlants_int ++;
		}
		else if (this.tag == "Omnivore" && CommonData.numberOfOmnivoresEatingHerbivores_int > 0)
		{
			CommonData.numberOfOmnivoresThatAteHerbivores_int ++;
		}
		else if (this.tag == "Carnivore" && CommonData.numberOfCarnivoresEatingOmnivores_int > 0)
		{
			CommonData.numberOfCarnivoresThatAteOmnivores_int ++;
		}


		//  until we don't eat the exact number of fish, continue to run this function and stop executing this one
		if (numberOfFishEaten < lff_this.numberOfFishThisOneNeedsToEat_int)
		{
			lff_this.ActionToCall (numberOfFishEaten);
			return;
		}
		else if (numberOfFishEaten == lff_this.numberOfFishThisOneNeedsToEat_int)
		{
			//  we have to zero eat each time we reach that number
//			Debug.Log ("I finally finished");
			numberOfFishEaten = 0;
		}


		if (this.tag == "Herbivore") 
		{
			if (CommonData.numberOfHerbivoresThatAtePlants_int != 0 && CommonData.numberOfHerbivoresEatingPlants_int != 0 &&
			    CommonData.numberOfHerbivoresThatAtePlants_int == CommonData.numberOfHerbivoresEatingPlants_int)
			{
				if (CommonData.numberOfOmnivoresEatingHerbivores_int > 0)
				{
					CommonData.rae_scr.LetOmnivoresEat ();
				}
				else if (CommonData.numberOfCarnivoresEatingOmnivores_int > 0)
				{
					CommonData.rae_scr.LetCarnivoresEat ();
				}
				else
				{
					//Debug.Log ("Lal, I am running reproduction, Herbivore");
					StartCoroutine (WaitToLetEventsRun (CommonData.rae_scr.RunReproduction ));
				}
			}
		}
		else if (this.tag == "Omnivore") 
		{
			if (CommonData.numberOfOmnivoresThatAteHerbivores_int != 0 && CommonData.numberOfOmnivoresEatingHerbivores_int != 0 &&
			    CommonData.numberOfOmnivoresThatAteHerbivores_int == CommonData.numberOfOmnivoresEatingHerbivores_int)
			{
				if (CommonData.numberOfCarnivoresEatingOmnivores_int > 0)
				{
					if (CommonData.numberOfHerbivoresThatAtePlants_int == CommonData.numberOfHerbivoresEatingPlants_int)
					{
						CommonData.rae_scr.LetCarnivoresEat ();
					}
					else
					{
//						Debug.Log ("Wow, you are in trouble");
						StartCoroutine (AllHerbivoresMustFinishEatingBeforeLettingCarnivoresDoIt ());
					}
				}
				else
				{
					//Debug.Log ("Lal, I am running reproduction, Omnivore");
					StartCoroutine (WaitToLetEventsRun (CommonData.rae_scr.RunReproduction ));
				}
			}
		}
		else if (this.tag == "Carnivore")
		{
			if (CommonData.numberOfCarnivoresEatingOmnivores_int != 0 && CommonData.numberOfCarnivoresThatAteOmnivores_int != 0 &&
			    CommonData.numberOfCarnivoresEatingOmnivores_int == CommonData.numberOfCarnivoresThatAteOmnivores_int)
			{
				//Debug.Log ("Lal, I am running reproduction, Carnivore");
				StartCoroutine (WaitToLetEventsRun (CommonData.rae_scr.RunReproduction ));
			}
		}

			StartCoroutine (WaitAlittleBit ());
	}


	public IEnumerator AllHerbivoresMustFinishEatingBeforeLettingCarnivoresDoIt () {

		while (true)
		{
			yield return new WaitForSeconds (1);
			if (CommonData.numberOfHerbivoresThatAtePlants_int == CommonData.numberOfHerbivoresEatingPlants_int)
			{
				CommonData.rae_scr.LetCarnivoresEat ();
				StopCoroutine ("AllHerbivoresMustFinishEatingBeforeLettingCarnivoresDoIt");
				Debug.Log ("Wow, you are NOT in trouble");
			}
		}
	}


	IEnumerator WaitToLetEventsRun (Action runAction) {

		yield return new WaitForSeconds (0.5f);
		//  run next event. Since we can't run an event from outside the owing class, we invoke the method =)
		if (runAction != null)
		{
			runAction ();
		}
		//rae.RunReproduction ();
	}

	IEnumerator WaitAlittleBit () {
		yield return new WaitForSeconds (0.1f);
		//  to make the idea with keeping the number of fish within some limits
		CommonData.countingExistingFish.CountNumberOfExistingFish ();
	}
	
	
	public void HuntingDown_TargetIsDestroyed () {
		
		sc_this.Speed_prop = sc_this.SpeedInit_prop + sc_this.SpeedInit_prop / 10; //  (+ 10%)
		sc_this.PickRandomSpot ();
		//  what if someone hunted down fish we were planning to eat? just think about it.
	}
}
