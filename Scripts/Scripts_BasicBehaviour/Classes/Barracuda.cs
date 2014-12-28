using UnityEngine;
using System.Collections;

public class Barracuda : Carnivore_AC {
	
	public float speed;


	public override void Start () {

		Speed_prop = speed;
		SpeedInit_prop = speed;
		base.Start ();

		
		ReprAndEat.CarnivoresAreGoingToEat += lff_scr.LookingForSpeciesToEat;
		ReprAndEat.HappyReproducing += repr_scr.ChoosingMate;
		ReprAndEat.checkIfItsTimeToDie += CheckIfItsTimeToDie;
		InvokeRepeating ("CheckIfOutside", 5, 7);
	}

	
	void OnDestroy () {
		
		ReprAndEat.checkIfItsTimeToDie -= CheckIfItsTimeToDie;
		ReprAndEat.HappyReproducing -= repr_scr.ChoosingMate;
		ReprAndEat.CarnivoresAreGoingToEat -= lff_scr.LookingForSpeciesToEat;
	}
}
