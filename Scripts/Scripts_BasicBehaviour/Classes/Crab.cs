using UnityEngine;
using System.Collections;

public class Crab : Omnivore_AC {

	public float speed;
	CrabMovement cm_scr;
	CharacterController cc_ctrl;
	RotateObjectToward rot_scr;
	bool groundedAndRotate = false;
	
	public override void Start () {
		
		Speed_prop = speed;
		SpeedInit_prop = speed;
		cm_scr = this.GetComponent <CrabMovement> ();
		GetCharacterController ();

		foreach (Transform t in this.transform)
		{
			rot_scr = t.gameObject.GetComponent <RotateObjectToward> ();
		}

		base.Start ();

		
		ReprAndEat.checkIfItsTimeToDie += CheckIfItsTimeToDie;
		ReprAndEat.OmnivoresAreGoingToEat += lff_scr.LookingForSpeciesToEat;
		ReprAndEat.HappyReproducing += repr_scr.ChoosingMate;
		InvokeRepeating ("CheckIfOutside", 5, 7);
	}


	public void GetCharacterController () {

		cc_ctrl = this.GetComponent <CharacterController> ();
	}


	public void DisableChararacterController () {

		cc_ctrl.enabled = false;
	}


	public void EnableCharacterController () {

		cc_ctrl.enabled = true;
	}


	public override void Update ()
	{

	}


	void FixedUpdate () {

		if (cc_ctrl != null)
		{
			if (cc_ctrl.isGrounded == true)
			{
				switch (currentActionState_enum)
				{
					case PossibleStatesOfActions.Moving:
						//  JUST MOVE
						//this.transform.LookAt (targetSpot_vector3);
						targetSpot_vector3 = new Vector3 (randomSpot_x_fl, this.transform.position.y, randomSpot_z_fl);
						cm_scr.MovingToStaticObject (() => PickRandomSpot (), targetSpot_vector3, speed_fl);
						break;
						
					case PossibleStatesOfActions.WasBorn:
						//this.transform.LookAt (targetSpot_vector3);
						cm_scr.MovingToStaticObject (() => PickRandomSpot (), targetSpot_vector3, speed_fl);
						break;
					
					case PossibleStatesOfActions.Chasing:
					//	Debug.Log ("moving");
						//cm_scr.MovingToStaticObject (() => hd_scr.HuntingDown_TargetIsReached (), huntItDown_go.transform.position, speed_fl);
						cm_scr.MovingToMovingObjects (huntItDown_go, () => hd_scr.HuntingDown_TargetIsReached (), () => hd_scr.HuntingDown_TargetIsDestroyed (), speed_fl);
						break;
						
					case PossibleStatesOfActions.GettingToMate:
						//  Move to your mate
						cm_scr.MovingToMovingObjects (mate_go, () => gtm_scr.GettingToMate_TargetIsReached (child_go), () => gtm_scr.GettingToMate_TargetIsDestroyed (), speed_fl);
						break;
						
					case PossibleStatesOfActions.BeingChosenAsMateAndHeadingToItsMate:
						//  Move to your mate
						cm_scr.MovingToMovingObjects (mate_go, () => gtm_scr.GettingToMate_TargetIsReached (child_go), () => gtm_scr.GettingToMate_TargetIsDestroyed (), speed_fl);
						break;
						
					case PossibleStatesOfActions.Dead:
						Dead ();
						break;
						
					default: 
						break;
				}
			}
		}
	}


	public override void Dead () {

		cc_ctrl.SimpleMove  (this.transform.position + Vector3.up * 1 * Time.deltaTime);
	}


	public override void PickRandomSpot () {

		randomSpot_x_fl = UnityEngine.Random.Range (-18, 18);
		randomSpot_z_fl = UnityEngine.Random.Range (3, 25);

		targetSpot_vector3 = new Vector3 (randomSpot_x_fl, this.transform.position.y, randomSpot_z_fl);


		this.transform.LookAt (new Vector3 (targetSpot_vector3.x, this.transform.position.y, this.transform.position.z));

		rot_scr.RotateOnce (targetSpot_vector3);
	}

	
	void OnDestroy () {
		
		ReprAndEat.checkIfItsTimeToDie -= CheckIfItsTimeToDie;
		ReprAndEat.HappyReproducing += repr_scr.ChoosingMate;
		ReprAndEat.OmnivoresAreGoingToEat -= lff_scr.LookingForSpeciesToEat;
	}
}
