using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;


public abstract class SeaCreature : MonoBehaviour {
	
	//  choose random place to go to
	public Vector3 targetSpot_vector3 = Vector3.zero;
	protected float randomSpot_x_fl = 0;
	protected float randomSpot_y_fl = 0;
	protected float randomSpot_z_fl = 0;

	private float speedInit_fl;
	public float SpeedInit_prop 
	{
		get 
		{
			return speedInit_fl;
		}
		set 
		{
			if (value > 0 && value < 10)
			{
				speedInit_fl = value;
			}
		}
	}
	//  change it to private later
	public float speed_fl;
	public float Speed_prop 
	{
		get 
		{
			return speed_fl;
		}
		set 
		{
			if (value > 0 && value < 15)
			{
				speed_fl = value;
			}
		}
	}

	public enum PossibleStatesOfActions
	{
		Moving,
		Fleeing,
		Chasing,
		Attacking,
		GettingToMate,
		BeingChosenAsMateAndHeadingToItsMate,
		WasBorn,
		Dead,
		WillBeSaved,
		SaveSomeFood,
		WillDieNextRound,
		BeingDragged
	}
	public bool beingHuntDown = false;
	public PossibleStatesOfActions currentActionState_enum;

	public GameObject huntItDown_go;

	protected List <GameObject> allTheHideouts = new List <GameObject> ();

	public GameObject speciesHunter_go;

	//  Reproduction
	public GameObject child_go;
	public List <string> possibleFood = new List <string> (); 
	public List <string> possibleFoodArchetypes = new List <string> (); 

	public GameObject mate_go;

	//  references
	protected GettingSeaCreatureScript getSCScript;
	protected HuntingDown hd_scr;
	protected MovingObjects ms_scr;
	protected GettingToMate gtm_scr;
	protected Reproduction repr_scr;
	public LookingForFood lff_scr;

	public string nameOfSpecies = string.Empty;
	
	public int maxAmountOfThisSpecies;

	public bool willBeDestroyedBecauseTheresNoFood = false;

	public int longevityInRounds_int;
	public int numberOfRoundIwasBorn_int;
	public int numberOfRoundWhenIwillDie_int;

	public virtual void Start () {

		//  initializing the tree
		TreeCustom tree = GameObject.Find ("Tree").GetComponent<TreeCustom> ();
		possibleFoodArchetypes = tree.GetEatableArchetypes(nameOfSpecies);
		possibleFood = tree.GetEatableFish (nameOfSpecies);
		tree.SpawnMiniMapObject (this.nameOfSpecies, this.tag);


		AssignPrefabs ();

		//  to make the idea with keeping the number of fish within some limits
		CommonData.countingExistingFish.CountNumberOfExistingFish ();


		//  just be consistent - keep everything in one place
		transform.parent = GameObject.FindGameObjectWithTag (this.tag + "_parent").transform;
		PickRandomSpot ();
		//  assign all the needed variables
		AssignVariables ();
		//this.GetComponent <ChoosingLayers> ().SetLayers (ref layerMask);

		getSCScript = this.GetComponent <GettingSeaCreatureScript> ();

		hd_scr = this.GetComponent <HuntingDown> ();
		

		if (this.GetComponent <MovingObjects> () != null)
		{
			ms_scr = this.GetComponent <MovingObjects> ();
		}

		gtm_scr = this.GetComponent <GettingToMate> ();
		lff_scr = this.GetComponent <LookingForFood> ();
		repr_scr = this.GetComponent <Reproduction> ();


	}


	public void CheckIfOutside () {

		if (this.transform.position.x < CommonFunctions.minX || this.transform.position.x > CommonFunctions.maxX || 
		    this.transform.position.y < CommonFunctions.minY || this.transform.position.y > CommonFunctions.maxY || 
		    this.transform.position.z < CommonFunctions.minZ || this.transform.position.z > CommonFunctions.maxZ &&
		    currentActionState_enum == PossibleStatesOfActions.Moving)
		{
			PickRandomSpot ();
		}
	}


	public void LongevityData () {

//		Debug.Log ("HERE");

		longevityInRounds_int = CommonData.feat_scr.longevity_dict [nameOfSpecies];
		numberOfRoundIwasBorn_int = CommonData.feat_scr.numberOfTurns_int;
		numberOfRoundWhenIwillDie_int = numberOfRoundIwasBorn_int + longevityInRounds_int;
	}


	public virtual void Update () {
	

		switch (currentActionState_enum)
		{
			case PossibleStatesOfActions.Moving:
				//  JUST MOVE
			    //this.transform.LookAt (targetSpot_vector3);
				ms_scr.MovingToStaticObject (() => PickRandomSpot (), targetSpot_vector3, speed_fl);
				break;

			case PossibleStatesOfActions.WillDieNextRound:
				//  JUST MOVE
				//this.transform.LookAt (targetSpot_vector3);
				ms_scr.MovingToStaticObject (() => PickRandomSpot (), targetSpot_vector3, speed_fl);
				break;

			case PossibleStatesOfActions.WasBorn:
				//this.transform.LookAt (targetSpot_vector3);
				ms_scr.MovingToStaticObject (() => PickRandomSpot (), targetSpot_vector3, speed_fl);
				break;
		
			case PossibleStatesOfActions.Chasing:
				ms_scr.MovingToMovingObjects (huntItDown_go, () => hd_scr.HuntingDown_TargetIsReached (), () => hd_scr.HuntingDown_TargetIsDestroyed (), speed_fl);
				break;

			case PossibleStatesOfActions.GettingToMate:
				//  Move to your mate
				ms_scr.MovingToMovingObjects (mate_go, () => gtm_scr.GettingToMate_TargetIsReached (child_go), () => gtm_scr.GettingToMate_TargetIsDestroyed (), speed_fl);
				break;

			case PossibleStatesOfActions.BeingChosenAsMateAndHeadingToItsMate:
				//  Move to your mate
				ms_scr.MovingToMovingObjects (mate_go, () => gtm_scr.GettingToMate_TargetIsReached (child_go), () => gtm_scr.GettingToMate_TargetIsDestroyed (), speed_fl);
				break;

			case PossibleStatesOfActions.Dead:
				Dead ();
				break;

			default: 
				break;
		}
	}

	#region HELPER_FUNCTIONS


	public virtual void Dead () {

		rigidbody.MovePosition (rigidbody.position + Vector3.up * 1 * Time.deltaTime);
	}


	public IEnumerator DieFish (float timeToWait) {

		this.tag = "DeadFish";

		yield return new WaitForSeconds (timeToWait);

		if (this.GetComponent <CommonScript> () != null)
		{
			List<GameObject> toDestroy = this.GetComponent <CommonScript> ().fishes;
			foreach (GameObject go in toDestroy) 
			{
				Destroy (go);	
			}
		}

	
		// if the object is destroyed, we no linger can execyte the code associated with it. That's why we cheat
		CommonData.countingExistingFish.CountNumberOfExistingFish (this.tag);

//		Debug.Log ("LOOOORDE");
		CommonData.ib_scr.RefreshBars ();

		Destroy (this.gameObject);
	}


	public void DestroyThisFuckingFish () {

		this.tag = "DeadFish";

		if (this.GetComponent <CommonScript> () != null)
		{
			List<GameObject> toDestroy = this.GetComponent <CommonScript> ().fishes;
			foreach (GameObject go in toDestroy) 
			{
				Destroy (go);	
			}
		}
		
		
		// if the object is destroyed, we no linger can execyte the code associated with it. That's why we cheat
		CommonData.countingExistingFish.CountNumberOfExistingFish (this.tag);

		
		
		//CheckIfThereAreSomeSpeciesLeft (this.nameOfSpecies, this.tag);
		
		//Debug.Log ("LOOOORDE");

		CommonData.ib_scr.RefreshBars ();

		Destroy (this.gameObject);
	}


	public virtual void PickRandomSpot () {
		
		randomSpot_x_fl = UnityEngine.Random.Range (-18, 18);
		randomSpot_z_fl = UnityEngine.Random.Range (3, 25);
		
		if (this.transform.position.y > 6)
		{
			randomSpot_y_fl = UnityEngine.Random.Range (this.transform.position.y - 5, this.transform.position.y + 1);
		}
		else if (this.transform.position.y < 3)
		{
			randomSpot_y_fl = UnityEngine.Random.Range (this.transform.position.y - 1, this.transform.position.y + 5);
		}
		else
		{
			randomSpot_y_fl = UnityEngine.Random.Range (this.transform.position.y - 3, this.transform.position.y + 3);
		}
		
		targetSpot_vector3 = new Vector3 (randomSpot_x_fl, randomSpot_y_fl, randomSpot_z_fl);
		this.transform.LookAt (targetSpot_vector3);
		//  make forward axis face the spot
	}


	void AssignVariables () {

		currentActionState_enum = PossibleStatesOfActions.Moving;
		mate_go = null;
	}


	protected void AssignPrefabs () {
		//  it takes the object itself if we don't find it


		child_go = CommonData.listOfPrefabs_scr.SendTheChild (this.gameObject);
	}


	public void BackToNormal () {
		//  ideally, that function should be overridden respectively.
		if (this.gameObject != null)
		{
			huntItDown_go = null;
			mate_go = null;
			PickRandomSpot ();
			currentActionState_enum = PossibleStatesOfActions.Moving;
			beingHuntDown = false;
		}
	}


	public void CheckIfItsTimeToDie () {

		if (CommonData.feat_scr.numberOfTurns_int == numberOfRoundWhenIwillDie_int) 
		{
//			Debug.Log ("Im fucking dead");
			DestroyThisFuckingFish ();
		}
		
	}

	#endregion
}
